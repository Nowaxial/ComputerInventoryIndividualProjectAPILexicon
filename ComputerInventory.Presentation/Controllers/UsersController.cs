using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;

namespace ComputerInventory.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;

    public UsersController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.UserService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}", Name = "UserById")]
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

        return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO user)
    {
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