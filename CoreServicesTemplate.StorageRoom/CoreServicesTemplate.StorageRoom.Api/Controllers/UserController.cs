﻿using Microsoft.AspNetCore.Mvc;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.AppModels;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

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
    public async Task<ActionResult> Post(UserApiModel apiModel)
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
            return Created(new Uri("api/storageroom/user/..."), model);
        }

        return Problem(operationResult.Message);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

                return Ok(resultApiModel);
            }
        }

        return NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsersApiModel>> Gets()
    {
        var operationResult = await _getUsersFeature.ExecuteAsync();

        if (operationResult.State.Equals(OutcomeState.Success))
        {
            // decoupling AppModel and map it in to ApiModel to return value.
            if (operationResult.Value != null)
            {
                var apiModel = _usersCustomMapper.Map(operationResult.Value);

                return Ok(apiModel);
            }
        }

        return NotFound();
    }

    [HttpPut("{guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public Task<IActionResult> Put(Guid guid, [FromBody] UserApiModel apiModel)
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
        //    return await Task.FromResult<IActionResult>(Ok());
        //}

        return Task.FromResult<IActionResult>(Conflict());
    }

    [HttpDelete("{apiModel}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        //    return await Task.FromResult<IActionResult>(NoContent());
        //}

        return Task.FromResult<ActionResult>(NotFound());
    }

    [HttpGet("error")]
    public IActionResult GetError() => Problem("Something went wrong.");
}