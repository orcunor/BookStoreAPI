using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        public IBookRepository _bookRepository { get; }
        public BooksController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        //[HttpGet("")]
        //public async Task<IActionResult> GetAllBooks()
        //{
        //    var books = await _bookRepository.GetAllBooksAsync();
        //    return Ok(books);
        //}

        [HttpGet("")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookRepository.GetAllBooks();
                if (books != null)
                    return Ok(books);
            }
            catch {}

            return NotFound();

        }

        [HttpGet("{id}")]
        public ActionResult<BookModel> GetBookById([FromRoute]int id)
        {
            try
            {
                var book = _bookRepository.GetBookById(id);
                if (book != null)
                    return Ok(book);
            }
            catch { }

            return NotFound();
        }


        [HttpPost("")]
        public IActionResult AddNewBook([FromBody]BookModel bookModel)
        {
            try
            {
                var id = _bookRepository.AddBook(bookModel);
                
               return CreatedAtAction(nameof(GetBookById), new {id = id, controller = "books"},id);
            }
            catch { }

            return NotFound();

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] BookModel bookModel,[FromRoute] int id)
        {
            try
            {
                if (_bookRepository.UpdateBook(id, bookModel))
                    return Ok();
            }
            catch { }
            return NotFound();

        }

        [HttpPatch("{id}")]
        public IActionResult UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        {
            try
            {
                if (_bookRepository.UpdateBookPatch(id, bookModel))
                    return Ok();
            }
            catch { }
            return NotFound();

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            try
            {
                if (_bookRepository.DeleteBook(id))
                    return Ok();
            }
            catch { }
            return NotFound();

        }

        //[HttpGet("")]
        //public IActionResult GetAllBooks()
        //{

        //    return Ok();

        //}

        ////[Route("{id}:int")]
        //[HttpGet("{id}")]
        //public ActionResult<BookModel> GetBookById(int id)
        //{
        //    return Ok();
        //}
    }
}
