using Microsoft.AspNetCore.Mvc;
using CoreServicesTemplate.Shared.Core.Enums;
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

    private readonly ICustomMapper<UserApiModel, UserAppModel> _userCustomMapper;
    private readonly ICustomMapper<UsersApiModel, UsersAppModel> _usersCustomMapper;

    public UserController(
        IAddUserFeature addUserFeature,
        IGetUserFeature getUserFeature,
        IGetUsersFeature getUsersFeature,
        ICustomMapper<UsersApiModel, UsersAppModel> usersCustomMapper,
        ICustomMapper<UserApiModel, UserAppModel> userCustomMapper)
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
    public async Task<IActionResult> Add(UserApiModel apiModel)
    {
        if (!ModelState.IsValid)
        {
            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return BadRequest(message);
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        var operationResult = await _addUserFeature.ExecuteAsync(model);

        if (operationResult.State.Equals(OutcomeState.Success))
        {
            return Ok();
        }

        return Problem(operationResult.Message);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UserApiModel>> Get([FromBody] UserApiModel apiModel)
    {
        if (!ModelState.IsValid || apiModel.Guid == Guid.Empty)
        {
            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return BadRequest(message);
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        var operationResult = await _getUserFeature.ExecuteAsync(model);

        if (operationResult.State.Equals(OutcomeState.Success))
        {
            if (operationResult.Value != null)
            {
                // decoupling AppModel and map it in to ApiModel to return value.
                var resultApiModel = _userCustomMapper.Map(operationResult.Value);

                return resultApiModel;
            }
        }

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<UsersApiModel>> GetAll()
    {
        var operationResult = await _getUsersFeature.ExecuteAsync();

        if (operationResult.State.Equals(OutcomeState.Success))
        {
            // decoupling AppModel and map it in to ApiModel to return value.
            if (operationResult.Value != null)
            {
                var apiModel = _usersCustomMapper.Map(operationResult.Value);

                return apiModel;
            }
        }

        return NoContent();
    }

    [HttpPut("{guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<IActionResult> Update(Guid guid, [FromBody] UserApiModel apiModel)
    {
        if (!ModelState.IsValid || apiModel.Guid == Guid.Empty)
        {
            return Task.FromResult<IActionResult>(BadRequest());
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        // var result = await _updateUserFeature.ExecuteAddAsync(model);

        //if (result is null)
        //{
        //    return await Task.FromResult<IActionResult>(NotFound());
        //}

        return Task.FromResult<IActionResult>(NoContent());
    }

    [HttpDelete("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<ActionResult> Delete(UserApiModel apiModel)
    {
        if (!ModelState.IsValid || apiModel.Guid == Guid.Empty)
        {
            return Task.FromResult<ActionResult>(BadRequest());
        }

        // decoupling ApiModel and map it in to AppModel.
        var model = _userCustomMapper.Map(apiModel);

        // var result = await _deleteUserFeature.ExecuteAddAsync(model);

        //if (result is null)
        //{
        //    return await Task.FromResult<IActionResult>(NotFound());
        //}

        return Task.FromResult<ActionResult>(NoContent());
    }

    [HttpGet("error")]
    public IActionResult GetError() => Problem("Something went wrong.");
}