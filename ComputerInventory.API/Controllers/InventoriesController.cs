using AutoMapper;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;
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

        private readonly IMapper _mapper;

        public InventoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<IEnumerable<InventoryDTO>> GetInventory(bool includeUsers)
        {
            //return await _unitOfWork.InventoryRepository.GetAllAsync();
            //return _mapper.Map<List<InventoryDTO>>(await _unitOfWork.InventoryRepository.GetAllAsync());
            //var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            //var dto = _mapper.Map<IEnumerable<InventoryDTO>>(inventories);

            var inventoryUsers = includeUsers ? _mapper.Map<List<InventoryDTO>>(await _unitOfWork.InventoryRepository.GetAllAsync(true))
                                               : _mapper.Map<List<InventoryDTO>>(await _unitOfWork.InventoryRepository.GetAllAsync());

            return inventoryUsers;
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDTO>> GetInventory(int id)
        {
            var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return _mapper.Map<InventoryDTO>(inventory);
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, InventoryUpdateDTO inventoryDto)
        {
            var existingInventory = await _unitOfWork.InventoryRepository.GetAsync(id);
            if (existingInventory == null)
            {
                return NotFound();
            }

            _mapper.Map(inventoryDto, existingInventory);
            _unitOfWork.InventoryRepository.Update(existingInventory);

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
        public async Task<ActionResult<InventoryCreateDTO>> PostInventory(InventoryCreateDTO inventoryDto)
        {
            var inventory = _mapper.Map<Inventory>(inventoryDto);
            _unitOfWork.InventoryRepository.Add(inventory);
            await _unitOfWork.CompleteAsync();

            var createdInventoryDto = _mapper.Map<InventoryDTO>(inventory);
            return CreatedAtAction("GetInventory", new { id = inventory.Id }, createdInventoryDto);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            var inventoryToDelete = await _unitOfWork.InventoryRepository.GetAsync(id);
            if (inventoryToDelete == null)
            {
                return NotFound("Inventory doesn't exist");
            }
            
            _unitOfWork.InventoryRepository.Remove(inventoryToDelete);

            try
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete inventory");
            }
        }

        [HttpPatch("{inventoryId}")]
        public async Task<ActionResult> PatchInventory(int inventoryId, JsonPatchDocument<InventoryUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var inventory = await _unitOfWork.InventoryRepository.GetAsync(inventoryId);
            if (inventory == null)
            {
                return NotFound("Inventory does not exist");
            }

            var inventoryDTO = _mapper.Map<InventoryUpdateDTO>(inventory);
            patchDoc.ApplyTo(inventoryDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(inventoryDTO, inventory);
            _unitOfWork.InventoryRepository.Update(inventory);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return HandleConcurrencyException(ex, inventoryId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to update inventory");
            }

            return NoContent();
        }

        private ActionResult HandleConcurrencyException(DbUpdateConcurrencyException ex, int id)
        {
            if (!_unitOfWork.InventoryRepository.AnyAsync(id).Result)
            {
                return NotFound("Inventory does not exist");
            }
            else
            {
                return StatusCode(500, "Failed to update inventory");
            }
        }

        private bool InventoryExists(int id)
        {
            return _unitOfWork.InventoryRepository.GetAsync(id).Result != null;
        }
    }
}