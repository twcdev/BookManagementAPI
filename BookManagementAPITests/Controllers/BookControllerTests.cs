using BookManagementAPI.Controllers;
using BookManagementAPI.Models;
using BookManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookManagementAPITests
{
    [TestClass]
    public class BookControllerTests
    {
        private Mock<IBookService> _mockBookService;
        private BookController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockBookService = new Mock<IBookService>();
            _controller = new BookController(_mockBookService.Object);
        }

        [TestMethod]
        public void GetBooks_ReturnsAllBooks()
        {
            // Arrange
            var mockBooks = new List<Book> { new Book { Id = 1, Title = "Test Book 1" }, new Book { Id = 2, Title = "Test Book 2" } };
            _mockBookService.Setup(service => service.GetAllBooks()).Returns(mockBooks);

            // Act
            var result = _controller.GetBooks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockBooks.Count, result.Count);
            Assert.AreEqual("Test Book 1", result[0].Title);
            Assert.AreEqual("Test Book 2", result[1].Title);
        }

        [TestMethod]
        public void GetBook_ValidId_ReturnsBook()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookById(1)).Returns(new Book { Id = 1, Title = "Test Book 1" });

            // Act
            var result = _controller.GetBook(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var book = okResult.Value as Book;
            Assert.IsNotNull(book);
            Assert.AreEqual("Test Book 1", book.Title);
        }

        [TestMethod]
        public void GetBook_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookById(It.IsAny<int>())).Returns((Book)null);

            // Act
            var result = _controller.GetBook(99);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateBook_ValidBook_ReturnsCreatedBook()
        {
            // Arrange
            var book = new Book { Title = "New Book", Author = "Author", Year = 2021 };
            _mockBookService.Setup(service => service.AddBook(It.IsAny<Book>())).Returns(book);

            // Act
            var result = _controller.CreateBook(book);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnedBook = createdAtActionResult.Value as Book;
            Assert.AreEqual("New Book", returnedBook.Title);
        }

        [TestMethod]
        public void CreateBook_InvalidBook_ReturnsBadRequest()
        {
            // Arrange
            var book = new Book { Title = "", Author = "", Year = 0 };
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = _controller.CreateBook(book);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void UpdateBook_ValidId_ReturnsUpdatedBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author", Year = 2022 };
            _mockBookService.Setup(service => service.UpdateBook(1, book)).Returns(book);

            // Act
            var result = _controller.UpdateBook(1, book);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedBook = okResult.Value as Book;
            Assert.AreEqual("Updated Book", returnedBook.Title);
        }

        [TestMethod]
        public void UpdateBook_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var book = new Book { Id = 99, Title = "Nonexistent Book", Author = "Author", Year = 2022 };
            _mockBookService.Setup(service => service.UpdateBook(99, book)).Returns((Book)null);

            // Act
            var result = _controller.UpdateBook(99, book);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteBook_ValidId_ReturnsOkAndDeletedBook()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Delete Me", Author = "Author", Year = 2021 };
            _mockBookService.Setup(service => service.DeleteBook(1)).Returns(book);

            // Act
            var result = _controller.DeleteBook(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedBook = okResult.Value as Book;
            Assert.AreEqual("Delete Me", returnedBook.Title);
        }

        [TestMethod]
        public void DeleteBook_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBook(99)).Returns((Book)null);

            // Act
            var result = _controller.DeleteBook(99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}