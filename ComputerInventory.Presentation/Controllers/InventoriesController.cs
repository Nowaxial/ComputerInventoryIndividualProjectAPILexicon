using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Request;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;

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
        return Ok(inventories);
    }

    [HttpGet("{id}", Name = "InventoryById")]
    public async Task<IActionResult> GetInventory(int id)
    {
        var inventory = await _service.InventoryService.GetInventoryAsync(id);
        return Ok(inventory);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventory([FromBody] InventoryCreateDTO inventory)
    {
        var createdInventory = await _service.InventoryService.CreateInventoryAsync(inventory);
        return CreatedAtRoute("InventoryById", new { id = createdInventory.Id }, createdInventory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInventory(int id, [FromBody] InventoryUpdateDTO inventory)
    {
        await _service.InventoryService.UpdateInventoryAsync(id, inventory);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
        await _service.InventoryService.DeleteInventoryAsync(id);
        return NoContent();
    }

    [HttpPatch("{inventoryId}")]
    public async Task<IActionResult> PatchInventory(int inventoryId, [FromBody] JsonPatchDocument<InventoryUpdateDTO> patchDoc)
    {
        var inventory = await _service.InventoryService.PatchInventoryAsync(inventoryId, patchDoc);
        return Ok(inventory);
    }
}