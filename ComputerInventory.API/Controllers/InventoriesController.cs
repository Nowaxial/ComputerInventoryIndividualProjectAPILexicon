using AutoMapper;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using ComputerInventory.Data.Repositories;
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
        public async Task<IEnumerable<InventoryDTO>> GetInventory()
        {
            //return await _unitOfWork.InventoryRepository.GetAllAsync();
            return _mapper.Map<List<InventoryDTO>>(await _unitOfWork.InventoryRepository.GetAllAsync());
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            


            var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
            if (inventory == null)
            {
                return NotFound("Inventory doesn't exist");
            }

            _unitOfWork.InventoryRepository.Remove(inventory);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.UserRepository.AnyAsync(id))
                {
                    return NotFound("Inventory doesn't exist");
                }
                else
                {
                    return StatusCode(500, "Failed to delete inventory");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete inventory");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchInventory(int id, JsonPatchDocument<InventoryUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var inventory = await _unitOfWork.InventoryRepository.GetAsync(id);
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
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.InventoryRepository.AnyAsync(id))
                {
                    return NotFound("Inventory does not exist");
                }
                else
                {
                    return StatusCode(500, "Failed to update inventory");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update tournament");
            }
            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _unitOfWork.InventoryRepository.GetAsync(id).Result != null;
        }
    }
}