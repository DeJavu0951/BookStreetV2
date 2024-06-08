using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity<T>
    {
        [NotMapped]
        public int? _action { get; set; }
    }
}
