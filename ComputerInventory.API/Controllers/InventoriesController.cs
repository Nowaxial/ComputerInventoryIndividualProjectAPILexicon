using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        //private readonly ComputerInventoryContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public InventoriesController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            return await _unitOfWork.InventoryRepository.GetAllAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _unitOfWork.InventoryRepository.Update(inventory);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            _unitOfWork.InventoryRepository.Add(inventory);
            await _unitOfWork.CompleteAsync();
            

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _unitOfWork.InventoryRepository.Remove(inventory);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _unitOfWork.InventoryRepository.Equals(id);
        }
    }
}