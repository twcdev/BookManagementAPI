namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }  // ID is assigned by the server, not input by the user
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string AudioURL { get; set; }
        public BookType BookType { get; set; }
    }
}