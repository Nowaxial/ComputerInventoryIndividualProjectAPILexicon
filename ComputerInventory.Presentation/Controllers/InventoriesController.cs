﻿using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using System.Text.Json;

namespace ComputerInventory.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoriesController : ControllerBase
{
    private readonly IServiceManager _service;

    public InventoriesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetInventories([FromQuery] InventoryRequestParams requestParams)
    {
        var inventories = await _service.InventoryService.GetInventoriesAsync(requestParams);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(inventories.MetaData));
        return Ok(new
        {
            items = inventories.Items,
            metaData = inventories.MetaData
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInventory(int id)
    {
        var inventory = await _service.InventoryService.GetInventoryAsync(id);
        return Ok(inventory);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventory(InventoryCreateDTO inventory)
    {
        
        var createdInventory = await _service.InventoryService.CreateInventoryAsync(inventory);
        TryValidateModel(createdInventory);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        return CreatedAtAction(nameof(GetInventory), new { id = createdInventory.Id }, createdInventory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInventory(int id, InventoryUpdateDTO inventory)
    {
        

        await _service.InventoryService.UpdateInventoryAsync(id, inventory);
        TryValidateModel(inventory);
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
        await _service.InventoryService.DeleteInventoryAsync(id);
        return NoContent();
    }

    [HttpPatch("{inventoryId}")]
    public async Task<IActionResult> PatchInventory(int inventoryId, JsonPatchDocument<InventoryUpdateDTO> patchDoc)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var inventory = await _service.InventoryService.PatchInventoryAsync(inventoryId, patchDoc);
        return Ok(inventory);
    }
}