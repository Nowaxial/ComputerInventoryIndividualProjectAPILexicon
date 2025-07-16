using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using System.Text.Json;

namespace ComputerInventory.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;

    public UsersController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] RequestParams requestParams)
    {
        var users = await _service.UserService.GetUsersAsync(requestParams);

        Response.Headers["X-Pagination"] = JsonSerializer.Serialize(users.MetaData);

        return Ok(new
        {
            items = users.Items,
            metaData = users.MetaData,
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _service.UserService.GetUserAsync(id);
        return Ok(user);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        var user = await _service.UserService.GetUserByNameAsync(name);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserCreateDTO user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _service.UserService.CreateUserAsync(user);

        return CreatedAtRoute(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _service.UserService.UpdateUserAsync(id, user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _service.UserService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(int id, [FromBody] JsonPatchDocument<UserUpdateDTO> patchDoc)
    {
        var user = await _service.UserService.PatchUserAsync(id, patchDoc);

        return Ok(user);
    }
}