namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về tác giả của sách
    /// </summary>
    public class BookAuthors
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
