namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về tác giả
    /// </summary>
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Portrait { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }

        public ICollection<BookAuthorsDTO> BookAuthors { get; set; }
    }
}
