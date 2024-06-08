using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về sách
    /// </summary>
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }

        public string Title { get; set; }
        public string BookCover { get; set; }
        public string Description { get; set; }
        public DateTime? PublicDay { get; set; }
        public decimal Price { get; set; }
        public string Isbn { get; set; }

        public ICollection<BookAuthors> BookAuthors { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
