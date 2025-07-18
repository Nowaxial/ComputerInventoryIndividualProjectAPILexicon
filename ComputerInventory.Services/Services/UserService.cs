﻿using AutoMapper;
using ComputerInventory.Core.Common;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Exceptions;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.Interfaces;

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

    //public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    //{
    //    var users = await _unitOfWork.UserRepository.GetAllAsync();
    //    if (users == null || !users.Any())
    //        throw new KeyNotFoundException("No users found");

    //    var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
    //    return userDtos;
    //}

    public async Task<PagedList<UserDTO>> GetUsersAsync(RequestParams requestParams)
    {
        var pagedList = await _unitOfWork.UserRepository.GetAllAsync(requestParams);
        var userDtos = pagedList.Items.Select(u => _mapper.Map<UserDTO>(u)).ToList();

        return new PagedList<UserDTO>(
            userDtos,
            pagedList.MetaData.TotalCount,
            pagedList.MetaData.CurrentPage,
            pagedList.MetaData.PageSize
        );
    }


    public async Task<UserDTO> GetUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new UserNotFoundException($"User with id {id} not found");

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserGetDTO> GetUserByNameAsync(string name)
    {
        var user = await _unitOfWork.UserRepository.GetByNameAsync(name);
        if (user == null) throw new UserNotFoundByNameException($"User with name {name} not found");

        return _mapper.Map<UserGetDTO>(user);
    }

    public async Task<UserDTO> CreateUserAsync(UserCreateDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);

        if (!await _unitOfWork.InventoryRepository.AnyAsync(user.InventoryId))
        {
            throw new InventoryNotFoundException($"Inventory with inventoryID {user.InventoryId} not found");
        }

        var users = await _unitOfWork.UserRepository.GetAllAsync();
        if (users.Count(u => u.InventoryId == user.InventoryId) >= 10)
        {
            throw new MaxUsersReachedInInventoryException("Maximum number of users (10) per inventory has been reached.");
        }
        if (users.Any(u => u.Name == user.Name))
        {
            throw new UserFoundWithTheSameNameException("A user with the same name already exists.");
        }

        _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<UserDTO>(user);
    }

    public async Task UpdateUserAsync(int id, UserUpdateDTO userDto)
    {
 


        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (!await _unitOfWork.UserRepository.AnyAsync(id))
        {
            throw new UserNotFoundException($"User with id {id} not found");
        }

        if (user == null) throw new UserNotFoundException($"User with id {id} not found");

        if (!await _unitOfWork.InventoryRepository.AnyAsync(user.InventoryId))
        {
            throw new InventoryNotFoundException($"Inventory with inventoryID {user.InventoryId} not found");
        }
        if (user.Name != userDto.Name)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            if (users.Any(u => u.Name == userDto.Name))
            {
                throw new UserFoundWithTheSameNameException("A user with the same name already exists.");
            }
        }

        _mapper.Map(userDto, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new UserNotFoundException($"User with id {id} not found");

        _unitOfWork.UserRepository.Remove(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<UserGetDTO> PatchUserAsync(int id, JsonPatchDocument<UserUpdateDTO> patchDoc)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);
        if (user == null) throw new UserNotFoundException($"User with id {id} not found");
        if (!await _unitOfWork.InventoryRepository.AnyAsync(user.InventoryId))
        {
            throw new InventoryNotFoundException($"Inventory with inventoryID {user.InventoryId} not found");
        }

        var userToPatch = _mapper.Map<UserUpdateDTO>(user);
        patchDoc.ApplyTo(userToPatch);
        

        _mapper.Map(userToPatch, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<UserGetDTO>(user);
    }
}