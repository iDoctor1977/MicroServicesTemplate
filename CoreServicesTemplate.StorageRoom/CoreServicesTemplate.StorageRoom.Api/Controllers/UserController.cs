using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("StorageRoom/[controller]/[action]", Name = "[controller]_[action]")]
    public class UserController : ControllerBase
    {
        private readonly IAddUserFeature _addUserFeature;
        private readonly IGetUserFeature _getUserFeature;
        private readonly IGetUsersFeature _getUsersFeature;

        private readonly IConsolidatorToData<UsersApiModel, UsersModel> _usersModelCustomPresenter;
        private readonly IConsolidatorToData<UserApiModel, UserModel> _userModelConsolidator;

        public UserController(
            IAddUserFeature addUserFeature,
            IGetUserFeature getUserFeature,
            IGetUsersFeature getUsersFeature,
            IConsolidatorToData<UsersApiModel, UsersModel> usersModelCustomPresenter,
            IConsolidatorToData<UserApiModel, UserModel> userModelConsolidator)
        {
            _addUserFeature = addUserFeature;
            _getUserFeature = getUserFeature;
            _getUsersFeature = getUsersFeature;
            _usersModelCustomPresenter = usersModelCustomPresenter;
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

            //var model = _userModelReceiver.ToData(apiModel);
            var model = _userModelConsolidator.ToData(apiModel).Resolve();

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

            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            var resultModel = await _getUserFeature.HandleAsync(model);

            var resultApiModel = _userModelConsolidator.ToDataReverse(resultModel).Resolve();

            return resultApiModel is null ? NoContent() : resultApiModel;
        }

        // GET: StorageRoom/User/GetAll
        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _usersModelCustomPresenter.ToDataReverse(model).Resolve();

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

            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // var result = await _updateUserFeature.HandleAsync(model);

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

            var model = _userModelConsolidator.ToData(apiModel).Resolve();

            // var result = await _deleteUserFeature.HandleAsync(model);

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}

            return await Task.FromResult<IActionResult>(NoContent());
        }
    }
}
