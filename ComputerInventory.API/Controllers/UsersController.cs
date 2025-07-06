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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            //return await _unitOfWork.UserRepository.GetAllAsync();
            //return _mapper.Map<List<UserDTO>>(await _unitOfWork.UserRepository.GetAllAsync());
            var userDto = _mapper.Map<List<UserDTO>>(await _unitOfWork.UserRepository.GetAllAsync());
            if (userDto == null || !userDto.Any())
            {
                return NotFound("No users found");
            }
            return Ok(userDto);

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTO>> GetUser(int id, int inventoryId)
        {
            //var user = await _unitOfWork.UserRepository.GetAsync(id);

            //if (user == null)
            //{
            //    return NotFound();
            //}

            //return user;
            //var user = _mapper.Map<UserGetDTO>(await _unitOfWork.UserRepository.GetAsync(id));
            //if (id != userGetDTO.Id)
            //{
            //    return BadRequest();
            //}
            //if (user == null)
            //{
            //    return NotFound("User doesn't exist");
            //}
            //return Ok(user);

            var user = _mapper.Map<UserGetDTO>(await _unitOfWork.UserRepository.GetAsync(id, inventoryId));
            if (user == null)
            {
                return NotFound("User doesn't exist");
            }
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch");
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO dto, int inventoryId)
        {
            //if (id != user.Id)
            //{
            //    return BadRequest();
            //}

            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_unitOfWork.UserRepository.Update(user);

            //try
            //{
            //    await _unitOfWork.CompleteAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            var existingUser = await _unitOfWork.UserRepository.GetAsync(id, inventoryId);
            if (existingUser == null)
            {
                return NotFound("User doesn't exist");
            }
            var userToUpdate = _mapper.Map<UserUpdateDTO, User>(dto, existingUser);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.UserRepository.AnyAsync(id))
                {
                    return NotFound("User doesn't exist");
                }
                else
                {
                    return StatusCode(500, "Failed to update user");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update user");
            }

            return NoContent();

        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserCreateDTO userCreateDTO)
        {
            //_unitOfWork.UserRepository.Add(user);
            //await _unitOfWork.CompleteAsync();

            //return CreatedAtAction("GetUser", new { id = user.Id }, user);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = _mapper.Map<User>(userCreateDTO);
            _unitOfWork.UserRepository.Add(userToCreate);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to create user");
            }

            return CreatedAtAction("GetUser", new { id = userToCreate.Id }, userToCreate);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, int inventoryId)
        {
            var inventoryExists = await _unitOfWork.InventoryRepository.AnyAsync(inventoryId);

            if (!inventoryExists)
            {
                return NotFound("Inventory doesn't exist");
            }


            var user = await _unitOfWork.UserRepository.GetAsync(id, inventoryId);
            if (user == null)
            {
                return NotFound("User doesn't exist");
            }

            _unitOfWork.UserRepository.Remove(user);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.UserRepository.AnyAsync(id))
                {
                    return NotFound("User doesn't exist");
                }
                else
                {
                    return StatusCode(500, "Failed to delete user");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete user");
            }

            return NoContent();


            //var user = await _unitOfWork.UserRepository.GetAsync(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //_unitOfWork.UserRepository.Remove(user);
            //await _unitOfWork.CompleteAsync();

            //return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, JsonPatchDocument<UserUpdateDTO> patchDoc, int inventoryId)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null");
            }
            var user = await _unitOfWork.UserRepository.GetAsync(id, inventoryId);
            if (user == null)
            {
                return NotFound("User doesn't exist");
            }
            var userToPatch = _mapper.Map<UserUpdateDTO>(user);
            patchDoc.ApplyTo(userToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(userToPatch, user);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.UserRepository.AnyAsync(id))
                {
                    return NotFound("User doesn't exist");
                }
                else
                {
                    return StatusCode(500, "Failed to update user");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update user");
            }
            return NoContent();

        }

        private bool UserExists(int id, int inventoryId)
        {
            return _unitOfWork.UserRepository.Equals(id);
        }
    }
}