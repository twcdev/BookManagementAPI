namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }  // For hardcopy books
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }  // HTML formatted
        public int Year { get; set; }
        public string AudioURL { get; set; }  // For audiobooks
    }
}