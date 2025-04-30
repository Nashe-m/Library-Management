using AutoMapper;
using LibraryManagement.DTOs;
using LibraryManagement.Helpers;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController(IBookRepository bookRepo, IMapper mapper, ILogger<BookController> logger) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks([FromQuery]QueryObject query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogError($"Invalid Get attempt in the {nameof(GetAllBooks)}");
                    return BadRequest(ModelState);
                }
                var books = await bookRepo.GetAllAsync(query);
                if (books == null) return NotFound("No Books Available");

                return Ok(books);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetAllBooks)}:{ex.Message}.");
               return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await bookRepo.GetBookByIdAsync(id);
                if (book == null) return NotFound();
                return Ok(book);
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetBook)}:{ex.Message}");
                return StatusCode(500, "Internal Server Error"); 
            }
            
        }
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBook([FromBody]AddBookDto bookDto)
        {
            try
            {
                if (bookDto == null) return BadRequest("Invalid");
                if (!ModelState.IsValid)
                {
                    logger.LogError($"Invalid Post attempt in the {nameof(AddBookDto)}");
                    return BadRequest(ModelState);
                }
               
                var payload = mapper.Map<Book>(bookDto);
                var bookModel = await bookRepo.AddBookAsync(payload);
                // return StatusCode(201, bookModel);
                return CreatedAtAction(nameof(GetBook), new { id = bookModel.Id }, bookModel);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(AddBook)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
            
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBook([FromRoute]int id,[FromBody]UpdateBookDto book) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogError($"Invalid update attempt in the {nameof(UpdateBookDto)}");
                    return BadRequest(ModelState);
                }

                var payload = mapper.Map<Book>(book);
                var updatedBook = await bookRepo.UpdateBookAsync(id, payload);
                if (updatedBook == null) return NotFound("Book not found");
               
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(UpdateBook)} : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
           
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await bookRepo.DeleteBookAsync(id);
                if (book == null) return NotFound("Book Not Found");
                return NoContent();
            }
            catch(Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(DeleteBook)}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}

