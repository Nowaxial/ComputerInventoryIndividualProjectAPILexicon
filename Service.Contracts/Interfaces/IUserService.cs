﻿using ComputerInventory.Core.Common;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Contracts.Interfaces;

public interface IUserService
{
    Task<PagedList<UserDTO>> GetUsersAsync(RequestParams requestParams);
    //Task<IEnumerable<UserDTO>> GetUsersAsync();
    Task<UserDTO> GetUserAsync(int id);
    Task<UserGetDTO> GetUserByNameAsync(string name);
    Task<UserDTO> CreateUserAsync(UserCreateDTO user);
    Task UpdateUserAsync(int id, UserUpdateDTO user);
    Task DeleteUserAsync(int id);
    Task<UserGetDTO> PatchUserAsync(int id, JsonPatchDocument<UserUpdateDTO> patchDoc);
}