using BookManagementAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookManagementAPITests
{
    [TestClass]
    public class BookServiceTests
    {
        private BookService _bookService;

        [TestInitialize]
        public void Setup()
        {
            _bookService = new BookService();
        }

        [TestMethod]
        public void AddBook_AddsBook_ReturnsAddedBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Author = "Author", Year = 2020 };

            // Act
            var result = _bookService.AddBook(book);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, _bookService.GetAllBooks().Count());
        }

        [TestMethod]
        public void GetBookById_ValidId_ReturnsBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Author = "Author", Year = 2020 };
            _bookService.AddBook(book);

            // Act
            var result = _bookService.GetBookById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Book", result.Title);
        }

        [TestMethod]
        public void UpdateBook_ValidId_UpdatesBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Author = "Author", Year = 2020 };
            _bookService.AddBook(book);
            var updatedBook = new Book { Title = "Updated Book", Author = "Updated Author", Year = 2021 };

            // Act
            var result = _bookService.UpdateBook(1, updatedBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Book", result.Title);
            Assert.AreEqual("Updated Author", result.Author);
        }

        [TestMethod]
        public void DeleteBook_ValidId_DeletesBook()
        {
            // Arrange
            var book = new Book { Title = "Test Book", Author = "Author", Year = 2020 };
            _bookService.AddBook(book);

            // Act
            var deletedBook = _bookService.DeleteBook(1);

            // Assert
            Assert.IsNotNull(deletedBook);
            Assert.AreEqual(0, _bookService.GetAllBooks().Count());
        }
    }
}