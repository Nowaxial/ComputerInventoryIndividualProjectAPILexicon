using AutoMapper;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.Interfaces;
using System.Text.Json;

namespace ComputerInventory.Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        if (users == null || !users.Any())
            throw new KeyNotFoundException("No users found");

        var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
        return userDtos;
    }

    public async Task<UserDTO> GetUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} not found");

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserGetDTO> GetUserByNameAsync(string name)
    {
        var user = await _unitOfWork.UserRepository.GetByNameAsync(name);
        if (user == null) throw new KeyNotFoundException($"User with name {name} not found");

        return _mapper.Map<UserGetDTO>(user);
    }

    public async Task<UserDTO> CreateUserAsync(UserCreateDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);

        if (!await _unitOfWork.InventoryRepository.AnyAsync(user.InventoryId))
        {
            throw new KeyNotFoundException($"Inventory with id {user.InventoryId} not found");
        }

        var users = await _unitOfWork.UserRepository.GetAllAsync();
        if (users.Count(u => u.InventoryId == user.InventoryId) >= 10)
        {
            throw new InvalidOperationException("Maximum number of users (10) per inventory has been reached.");
        }
        if (users.Any(u => u.Name == user.Name))
        {
            throw new InvalidOperationException("A user with the same name already exists.");
        }

        _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<UserDTO>(user);
    }

    public async Task UpdateUserAsync(int id, UserUpdateDTO userDto)
    {
        if (id != userDto.Id) throw new ArgumentException("ID mismatch");

        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} not found");

        _mapper.Map(userDto, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} not found");

        _unitOfWork.UserRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<UserGetDTO> PatchUserAsync(int id, JsonPatchDocument<UserUpdateDTO> patchDoc)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} not found");

        var userToPatch = _mapper.Map<UserUpdateDTO>(user);
        patchDoc.ApplyTo(userToPatch);

        _mapper.Map(userToPatch, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<UserGetDTO>(user);
    }
}