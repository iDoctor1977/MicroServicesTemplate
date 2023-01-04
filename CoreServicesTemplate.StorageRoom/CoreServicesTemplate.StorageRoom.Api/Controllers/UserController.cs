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

        private readonly IConsolidator<UserApiModel, UserAppModel> _userCustomConsolidator;
        private readonly IConsolidator<UsersApiModel, UsersAppModel> _usersCustomConsolidator;

        public UserController(
            IFeatureCommand<UserAppModel> addUserFeature,
            IFeatureQuery<UserAppModel, UserAppModel> getUserFeature,
            IFeatureQuery<UsersAppModel> getUsersFeature,
            IConsolidator<UsersApiModel, UsersAppModel> usersCustomConsolidator,
            IConsolidator<UserApiModel, UserAppModel> userCustomConsolidator)
        {
            _addUserFeature = addUserFeature;
            _getUserFeature = getUserFeature;
            _getUsersFeature = getUsersFeature;
            _usersCustomConsolidator = usersCustomConsolidator;
            _userCustomConsolidator = userCustomConsolidator;
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
            var model = _userCustomConsolidator.ToData(apiModel).Resolve();

            await _addUserFeature.HandleAsync(model);
            
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
            var model = _userCustomConsolidator.ToData(apiModel).Resolve();

            var resultModel = await _getUserFeature.HandleAsync(model);

            // decoupling AppModel and map it in to ApiModel to return value.
            var resultApiModel = _userCustomConsolidator.ToDataReverse(resultModel).Resolve();

            return resultApiModel is null ? NoContent() : resultApiModel;
        }

        // GET: StorageRoom/User/GetAll
        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            // decoupling AppModel and map it in to ApiModel to return value.
            var apiModel = _usersCustomConsolidator.ToDataReverse(model).Resolve();

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
            var model = _userCustomConsolidator.ToData(apiModel).Resolve();

            // var result = await _updateUserFeature.AddHandleAsync(model);

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
            var model = _userCustomConsolidator.ToData(apiModel).Resolve();

            // var result = await _deleteUserFeature.AddHandleAsync(model);

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}

            return await Task.FromResult<IActionResult>(NoContent());
        }
    }
}
