using BookManagementAPI.Models;
using BookManagementAPI.Services;

public class BookService : IBookService
{
    private List<Book> _books = new List<Book>();

    public List<Book> GetAllBooks() => _books;

    public Book GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public Book AddBook(Book book)
    {
        book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(book);
        return book;
    }

    public Book UpdateBook(int id, Book updatedBook)
    {
        var book = GetBookById(id);
        if (book == null) return null;
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Description = updatedBook.Description;
        book.Year = updatedBook.Year;
        book.ISBN = updatedBook.ISBN;
        book.AudioURL = updatedBook.AudioURL;
        book.BookType = updatedBook.BookType;
        return book;
    }

    public Book DeleteBook(int id)
    {
        var book = GetBookById(id);
        if (book == null) return null;
        _books.Remove(book);
        return book;
    }
}