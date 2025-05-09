using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Interfaces;
using AutoMapper;
using LibraryManagement.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowingController(IBorrowingRepository borrowingRepository, IMapper mapper, ILogger<BorrowingController> logger) : ControllerBase
    {

        // GET: api/Borrowing
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <IActionResult> GetBorrowings()
        {
            try
            {
                var borrowings = await borrowingRepository.GetAllBorrowings();
                if (borrowings == null) return NotFound("No Borrowings");
                return Ok(borrowings);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetBorrowings)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: api/Borrowing/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBorrowing(int id)
        {
            try
            {
                var borrowing = await borrowingRepository.GetBorrowingById(id);
                if (borrowing is null) return NotFound("Book not found");
                return Ok(borrowing);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetBorrowing)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        // PUT: api/Borrowing/5
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBorrowing(int id, [FromBody]AddBorrowingDto borrowing)
        {
            try
            {
                if (!ModelState.IsValid) BadRequest(ModelState);
                var payload = mapper.Map<Borrowing>(borrowing);
                var borrowed = await borrowingRepository.UpdateBorrowing(id, payload);
                if (borrowed is null) return NotFound("Borrowing is not found");
                
                return Ok(borrowed);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(UpdateBorrowing)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BookReturn(int id)
        {
            try
            {
                var borrowing = await borrowingRepository.ReturnBook(id);
                if (borrowing is null) return NotFound("Borrowing Not Found");
                return Ok(borrowing);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in {nameof(BookReturn)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        // POST: api/Borrowing
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBorrowing([FromBody]AddBorrowingDto borrowing)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var payload = mapper.Map<Borrowing>(borrowing);
                var borrow = await borrowingRepository.SubmitBorrowing(payload);
                
                if (borrowing is null) return BadRequest("Invalid Details");
                return CreatedAtAction(nameof(GetBorrowing), new { id = borrow.Id }, borrow);
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(AddBorrowing)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Borrowing/5
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            try
            {
                var borrowing = await borrowingRepository.DeleteBorowing(id);
                if (borrowing is null) return NotFound("Borrowing record not found");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(DeleteBorrowing)}:{ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
