using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về tác giả
    /// </summary>
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Portrait { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }

        public ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
