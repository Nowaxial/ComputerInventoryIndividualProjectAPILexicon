using ComputerInventory.Core.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Contracts.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsersAsync();
    Task<UserGetDTO> GetUserAsync(int id);
    Task<UserGetDTO> GetUserByNameAsync(string name);
    Task<UserGetDTO> CreateUserAsync(UserCreateDTO user);
    Task UpdateUserAsync(int id, UserUpdateDTO user);
    Task DeleteUserAsync(int id);
    Task<UserGetDTO> PatchUserAsync(int id, JsonPatchDocument<UserUpdateDTO> patchDoc);
}