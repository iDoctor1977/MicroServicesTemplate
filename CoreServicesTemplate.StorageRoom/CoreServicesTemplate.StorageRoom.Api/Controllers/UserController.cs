using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;
using Microsoft.AspNetCore.Http;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers;

[ApiController]
[Route("storageroom/api/[controller]/[action]", Name = "[controller]_[action]")]
public class UserController : ControllerBase
{
    private readonly IQueryHandlerFeature<UserAppModel, OperationStatusResult> _addUserFeature;
    private readonly IQueryHandlerFeature<UserAppModel, UserAppModel> _getUserFeature;
    private readonly IQueryHandlerFeature<UsersAppModel> _getUsersFeature;

    private readonly IConsolidator<UserApiModel, UserAppModel> _userCustomConsolidator;
    private readonly IConsolidator<UsersApiModel, UsersAppModel> _usersCustomConsolidator;

    public UserController(
        IQueryHandlerFeature<UserAppModel, OperationStatusResult> addUserFeature,
        IQueryHandlerFeature<UserAppModel, UserAppModel> getUserFeature,
        IQueryHandlerFeature<UsersAppModel> getUsersFeature,
        IConsolidator<UsersApiModel, UsersAppModel> usersCustomConsolidator,
        IConsolidator<UserApiModel, UserAppModel> userCustomConsolidator)
    {
        _addUserFeature = addUserFeature;
        _getUserFeature = getUserFeature;
        _getUsersFeature = getUsersFeature;
        _usersCustomConsolidator = usersCustomConsolidator;
        _userCustomConsolidator = userCustomConsolidator;
    }

    [HttpPost("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(UserApiModel apiModel)
    {
        if (apiModel is null)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomConsolidator.ToData(apiModel).Resolve();

        var result = await _addUserFeature.HandleAsync(model);

        return result.Equals(OperationStatusResult.Created) ? Created(API.StorageRoom.User.IndexFromUserToStorageRoomUrl(), result) : BadRequest();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UserApiModel>> Get([FromBody] UserApiModel apiModel)
    {
        if (apiModel is null || apiModel.Guid == Guid.Empty)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomConsolidator.ToData(apiModel).Resolve();

        var resultModel = await _getUserFeature.HandleAsync(model);

        // decoupling AppModel and map it in to ApiModel to return value.
        var resultApiModel = _userCustomConsolidator.ToDataReverse(resultModel).Resolve();

        return resultApiModel is null ? NoContent() : resultApiModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UsersApiModel>> GetAll()
    {
        var model = await _getUsersFeature.HandleAsync();

        // decoupling AppModel and map it in to ApiModel to return value.
        var apiModel = _usersCustomConsolidator.ToDataReverse(model).Resolve();

        return apiModel is null ? NoContent() : apiModel;
    }

    [HttpPut("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(UserApiModel apiModel)
    {
        if (apiModel is null || apiModel.Guid == Guid.Empty)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomConsolidator.ToData(apiModel).Resolve();

        // var result = await _updateUserFeature.AddHandleAsync(model);

        //if (result is null)
        //{
        //    return await Task.FromResult<IActionResult>(NotFound());
        //}

        return NoContent();
    }

    [HttpDelete("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(UserApiModel apiModel)
    {
        if (apiModel is null || apiModel.Guid == Guid.Empty)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomConsolidator.ToData(apiModel).Resolve();

        // var result = await _deleteUserFeature.AddHandleAsync(model);

        //if (result is null)
        //{
        //    return await Task.FromResult<IActionResult>(NotFound());
        //}

        return NoContent();
    }
}