using AutoMapper;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
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

    public async Task<InventoryDTO> GetInventoryAsync(int id)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
        if (inventory == null) throw new KeyNotFoundException($"Inventory with id {id} not found");

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
        if (inventory == null) throw new KeyNotFoundException($"Inventory with id {id} not found");

        _mapper.Map(inventoryDto, inventory);
        _unitOfWork.InventoryRepository.Update(inventory);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteInventoryAsync(int id)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
        if (inventory == null) throw new KeyNotFoundException($"Inventory with id {id} not found");

        _unitOfWork.InventoryRepository.Remove(inventory);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<InventoryDTO> PatchInventoryAsync(int inventoryId, JsonPatchDocument<InventoryUpdateDTO> patchDoc)
    {
        var inventory = await _unitOfWork.InventoryRepository.GetAsync(inventoryId);
        if (inventory == null) throw new KeyNotFoundException($"Inventory with id {inventoryId} not found");

        var inventoryToPatch = _mapper.Map<InventoryUpdateDTO>(inventory);
        patchDoc.ApplyTo(inventoryToPatch);

        _mapper.Map(inventoryToPatch, inventory);
        _unitOfWork.InventoryRepository.Update(inventory);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<InventoryDTO>(inventory);
    }

    public async Task<PagedList<InventoryDTO>> GetInventoriesAsync(InventoryRequestParams requestParams)
    {
        var inventories = await _unitOfWork.InventoryRepository.GetAllAsync(requestParams.IncludeUsers);

        var inventoryDtos = _mapper.Map<List<InventoryDTO>>(inventories);

        return await PagedList<InventoryDTO>.CreateAsync(inventoryDtos.AsQueryable(), requestParams.PageNumber, requestParams.PageSize);
    }






    //Hur skriver jag denna?
}