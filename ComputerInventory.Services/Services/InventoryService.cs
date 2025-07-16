using AutoMapper;
using ComputerInventory.Core.Common;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Exceptions;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts.Interfaces;

namespace ComputerInventory.Services.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedList<InventoryDTO>> GetInventoriesAsync(InventoryRequestParams requestParams)
    {
        var pagedList = await _unitOfWork.InventoryRepository.GetAllAsync(requestParams);
        if (pagedList == null || !pagedList.Items.Any())
        {
            throw new InventoryNotFoundException("No inventories found.");
        }

        var inventoryDtos = pagedList.Items.Select(i => _mapper.Map<InventoryDTO>(i)).ToList();

        return new PagedList<InventoryDTO>(
            inventoryDtos,
            pagedList.MetaData.TotalCount,
            pagedList.MetaData.CurrentPage,
            pagedList.MetaData.PageSize
        );
    }

    public async Task<InventoryDTO> GetInventoryAsync(int id)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
        if (inventory == null)
        {
            throw new InventoryNotFoundException($"Inventory with id {id} not found.");
        }
        return _mapper.Map<InventoryDTO>(inventory);
    }

    public async Task<InventoryDTO> CreateInventoryAsync(InventoryCreateDTO inventoryDto)
    {

        var inventory = _mapper.Map<Inventory>(inventoryDto);
        

        _unitOfWork.InventoryRepository.Add(inventory);
       
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<InventoryDTO>(inventory);
    }

    public async Task UpdateInventoryAsync(int id, InventoryUpdateDTO inventoryDto)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
        if (inventory == null)
        {
            throw new InventoryNotFoundException($"Inventory with id {id} not found.");
        }
        _mapper.Map(inventoryDto, inventory);
        _unitOfWork.InventoryRepository.Update(inventory);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteInventoryAsync(int id)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
        if (inventory == null)
        {
            throw new InventoryNotFoundException($"Inventory with id {id} not found.");
        }
        _unitOfWork.InventoryRepository.Remove(inventory);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<InventoryDTO> PatchInventoryAsync(int inventoryId, JsonPatchDocument<InventoryUpdateDTO> patchDoc)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(inventoryId);
        if (inventory == null)
        {
            throw new InventoryNotFoundException($"Inventory with id {inventoryId} not found.");
        }
        var inventoryToPatch = _mapper.Map<InventoryUpdateDTO>(inventory);

        patchDoc.ApplyTo(inventoryToPatch);

        _mapper.Map(inventoryToPatch, inventory);
        _unitOfWork.InventoryRepository.Update(inventory);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<InventoryDTO>(inventory);
    }
}