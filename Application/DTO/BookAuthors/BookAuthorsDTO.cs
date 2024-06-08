namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về tác giả của sách
    /// </summary>
    public class BookAuthorsDTO
    {
        public int BookId { get; set; }
        public BookDTO Book { get; set; }

        public int AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
