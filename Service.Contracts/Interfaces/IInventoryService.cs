using ComputerInventory.Core.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Service.Contracts.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetInventoriesAsync(bool includeUsers);

        Task<InventoryDTO> GetInventoryAsync(int id);

        Task<InventoryDTO> CreateInventoryAsync(InventoryCreateDTO inventory);

        Task UpdateInventoryAsync(int id, InventoryUpdateDTO inventory);

        Task DeleteInventoryAsync(int id);
        Task<InventoryDTO> PatchInventoryAsync(int inventoryId, JsonPatchDocument<InventoryUpdateDTO> patchDoc);
    }
}