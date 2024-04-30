using BookManagementAPI.Models;
using BookManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks() => _bookService.GetAllBooks();

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            return book != null ? Ok(book) : NotFound();
        }
        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book book) => CreatedAtAction(nameof(GetBook), new { id = book.Id }, _bookService.AddBook(book));

        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, [FromBody] Book book)
        {
            var updatedBook = _bookService.UpdateBook(id, book);
            return updatedBook != null ? Ok(updatedBook) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.DeleteBook(id);
            return book != null ? Ok(book) : NotFound();
        }
    }
}