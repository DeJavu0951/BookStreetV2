using Domain.Attribute;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về thể loại sách
    /// </summary>
    [ValidateDuplicate("Name")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
