using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("StorageRoom/[controller]/[action]", Name = "[controller]_[action]")]
    public class UserController : ControllerBase
    {
        private readonly IAddUserFeature _addUserFeature;
        private readonly IGetUserFeature _getUserFeature;
        private readonly IGetUsersFeature _getUsersFeature;

        private readonly IConsolidators<UserApiModel, UserModel> _consolidatorsReceiver;
        private readonly IConsolidators<UserModel, UserApiModel> _consolidatorsPresenter;
        private readonly IConsolidators<UsersModel, UsersApiModel> _consolidatorsCustomPresenter;

        public UserController(
            IAddUserFeature addUserFeature,
            IGetUserFeature getUserFeature,
            IGetUsersFeature getUsersFeature,
            IConsolidators<UserApiModel, UserModel> consolidatorsReceiver,
            IConsolidators<UserModel, UserApiModel> consolidatorsPresenter,
            IConsolidators<UsersModel, UsersApiModel> consolidatorsCustomPresenter)
        {
            _addUserFeature = addUserFeature;
            _getUserFeature = getUserFeature;
            _getUsersFeature = getUsersFeature;
            _consolidatorsReceiver = consolidatorsReceiver;
            _consolidatorsPresenter = consolidatorsPresenter;
            _consolidatorsCustomPresenter = consolidatorsCustomPresenter;
        }

        // POST: StorageRoom/User/AddUser/{apiModel}
        [HttpPost("{apiModel}")]
        public async Task<IActionResult> AddUser(UserApiModel apiModel)
        {
            if (apiModel is null)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            var model = _consolidatorsReceiver.ToData(apiModel);

            await _addUserFeature.HandleAsync(model);
            
            return CreatedAtAction(nameof(AddUser), apiModel);
        }

        // GET: StorageRoom/User/GetUser
        [HttpGet]
        public async Task<ActionResult<UserApiModel>> GetUser([FromBody] UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            var model = _consolidatorsReceiver.ToData(apiModel);

            var resultModel = await _getUserFeature.HandleAsync(model);

            var resultApiModel = _consolidatorsPresenter.ToData(resultModel);

            return resultApiModel is null ? NoContent() : resultApiModel;
        }

        // GET: StorageRoom/User/GetUsers
        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetUsers()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _consolidatorsCustomPresenter.ToData(model);

            return apiModel is null ? NoContent() : apiModel;
        }

        // PUT: StorageRoom/User/UpdateUser/{apiModel}
        [HttpPut("{apiModel}")]
        public async Task<IActionResult> UpdateUser(UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            var model = _consolidatorsReceiver.ToData(apiModel);

            // var result = await _updateUserFeature.HandleAsync(model);

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}
            
            return await Task.FromResult<IActionResult>(NoContent());
        }

        // DELETE: StorageRoom/User/DeleteUser/{apiModel}
        [HttpDelete("{apiModel}")]
        public async Task<IActionResult> DeleteUser(UserApiModel apiModel)
        {
            if (apiModel is null || apiModel.Guid == Guid.Empty)
            {
                return await Task.FromResult<ActionResult>(BadRequest());
            }

            var model = _consolidatorsReceiver.ToData(apiModel);

            // var result = await _deleteUserFeature.HandleAsync(model);

            //if (result is null)
            //{
            //    return await Task.FromResult<IActionResult>(NotFound());
            //}

            return NoContent();
        }
    }
}
