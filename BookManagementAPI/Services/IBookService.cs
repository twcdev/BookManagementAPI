using BookManagementAPI.Models;

namespace BookManagementAPI.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        Book AddBook(Book book);
        Book UpdateBook(int id, Book book);
        Book DeleteBook(int id);
    }
}