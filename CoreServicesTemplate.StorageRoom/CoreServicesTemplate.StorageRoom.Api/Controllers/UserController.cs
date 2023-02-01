using Microsoft.AspNetCore.Mvc;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers;

[ApiController]
[Route("api/storageroom/[controller]/[action]", Name = "[controller]_[action]")]
public class UserController : ControllerBase
{
    private readonly IAddUserFeature _addUserFeature;
    private readonly IGetUserFeature _getUserFeature;
    private readonly IGetUsersFeature _getUsersFeature;

    private readonly IMapperService<UserApiModel, UserAppModel> _userCustomMapper;
    private readonly IMapperService<UsersApiModel, UsersAppModel> _usersCustomMapper;

    public UserController(
        IAddUserFeature addUserFeature,
        IGetUserFeature getUserFeature,
        IGetUsersFeature getUsersFeature,
        IMapperService<UsersApiModel, UsersAppModel> usersCustomMapper,
        IMapperService<UserApiModel, UserAppModel> userCustomMapper)
    {
        _addUserFeature = addUserFeature;
        _getUserFeature = getUserFeature;
        _getUsersFeature = getUsersFeature;
        _usersCustomMapper = usersCustomMapper;
        _userCustomMapper = userCustomMapper;
    }

    [HttpPost("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add(UserApiModel apiModel)
    {
        if (apiModel is null)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        var result = await _addUserFeature.ExecuteAsync(model);

        var location = ApiUrl.StorageRoom.User.IndexFromUserToStorageRoom();
        return result.Equals(OperationStatusResult.Created) ? Created(location, result) : BadRequest();
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
        var model = _userCustomMapper.Map(apiModel);

        var resultModel = await _getUserFeature.ExecuteAsync(model);

        // decoupling AppModel and map it in to ApiModel to return value.
        var resultApiModel = _userCustomMapper.Map(resultModel);

        return resultApiModel is null ? NoContent() : resultApiModel;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UsersApiModel>> GetAll()
    {
        var model = await _getUsersFeature.ExecuteAsync();

        // decoupling AppModel and map it in to ApiModel to return value.
        var apiModel = _usersCustomMapper.Map(model);

        return apiModel is null ? NoContent() : apiModel;
    }

    [HttpPut("{guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Update(Guid guid, [FromBody] UserApiModel apiModel)
    {
        if (apiModel is null || apiModel.Guid == Guid.Empty)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

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
    public async Task<ActionResult> Delete(UserApiModel apiModel)
    {
        if (apiModel is null || apiModel.Guid == Guid.Empty)
        {
            return BadRequest();
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        // var result = await _deleteUserFeature.AddHandleAsync(model);

        //if (result is null)
        //{
        //    return await Task.FromResult<IActionResult>(NotFound());
        //}

        return NoContent();
    }

    [HttpGet("error")]
    public IActionResult GetError() => Problem("Something went wrong.");
}