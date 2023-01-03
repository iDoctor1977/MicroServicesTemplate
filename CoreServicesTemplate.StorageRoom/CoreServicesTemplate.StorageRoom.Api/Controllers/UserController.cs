using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("StorageRoom/[controller]/[action]", Name = "[controller]_[action]")]
    public class UserController : ControllerBase
    {
        private readonly IFeatureCommand<UserAppModel> _addUserFeature;
        private readonly IFeatureQuery<UserAppModel, UserAppModel> _getUserFeature;
        private readonly IFeatureQuery<UsersAppModel> _getUsersFeature;

        private readonly IConsolidator<UsersApiModel, UsersAppModel> _usersModelCustomConsolidator;
        private readonly IConsolidator<UserApiModel, UserAppModel> _userModelConsolidator;

        public UserController(
            IFeatureCommand<UserAppModel> addUserFeature,
            IFeatureQuery<UserAppModel, UserAppModel> getUserFeature,
            IFeatureQuery<UsersAppModel> getUsersFeature,
            IConsolidator<UsersApiModel, UsersAppModel> usersModelCustomConsolidator,
            IConsolidator<UserApiModel, UserAppModel> userModelConsolidator)
        {
            _addUserFeature = addUserFeature;
            _getUserFeature = getUserFeature;
            _getUsersFeature = getUsersFeature;
            _usersModelCustomConsolidator = usersModelCustomConsolidator;
            _userModelConsolidator = userModelConsolidator;
        }

        // POST: StorageRoom/User/Add/{apiModel}
        [HttpPost("{apiModel}")]
        public async Task<IActionResult> Add(UserApiModel apiModel)
        {
            if (apiModel is null)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            // decoupling ApiModel and map it in to AppModel.
            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // set model domain in to feature ad call handle method
            await _addUserFeature.SetModel(model).HandleAsync();
            
            return CreatedAtAction(nameof(Add), apiModel);
        }

        // GET: StorageRoom/User/Get
        [HttpGet]
        public async Task<ActionResult<UserApiModel>> Get([FromBody] UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            // decoupling ApiModel and map it in to AppModel.
            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // set model domain in to feature ad call handle method
            var resultModel = await _getUserFeature.SetModel(model).HandleAsync();

            // decoupling AppModel and map it in to ApiModel to return value.
            var resultApiModel = _userModelConsolidator.ToDataReverse(resultModel).Resolve();

            return resultApiModel is null ? NoContent() : resultApiModel;
        }

        // GET: StorageRoom/User/GetAll
        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            // call feature handle method without domain model input.
            var model = await _getUsersFeature.HandleAsync();

            // decoupling AppModel and map it in to ApiModel to return value.
            var apiModel = _usersModelCustomConsolidator.ToDataReverse(model).Resolve();

            return apiModel is null ? NoContent() : apiModel;
        }

        // PUT: StorageRoom/User/Update/{apiModel}
        [HttpPut("{apiModel}")]
        public async Task<IActionResult> Update(UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            // decoupling ApiModel and map it in to AppModel.
            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // set model domain in to feature ad call handle method.
            // var result = await _updateUserFeature.SetModel(model).HandleAddAsync();

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}

            return await Task.FromResult<IActionResult>(NoContent());
        }

        // DELETE: StorageRoom/User/Delete/{apiModel}
        [HttpDelete("{apiModel}")]
        public async Task<IActionResult> Delete(UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            // decoupling ApiModel and map it in to AppModel.
            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // set model domain in to feature ad call handle method.
            // var result = await _deleteUserFeature.SetModel(model).HandleAddAsync();

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}

            return await Task.FromResult<IActionResult>(NoContent());
        }
    }
}
