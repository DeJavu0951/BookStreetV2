namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về sách
    /// </summary>
    public class BookDTO
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        public int? DistributorId { get; set; }
        public DistributorDTO Distributor { get; set; }

        public int? PublisherId { get; set; }
        public PublisherDTO Publisher { get; set; }

        public int? GenreId { get; set; }
        public GenreDTO Genre { get; set; }

        public string Title { get; set; }
        public string BookCover { get; set; }
        public string Description { get; set; }
        public DateTime? PublicDay { get; set; }
        public decimal Price { get; set; }
        public string Isbn { get; set; }

        public ICollection<BookAuthorsDTO> BookAuthors { get; set; }
        public ICollection<StockDTO> Stocks { get; set; }
    }
}
