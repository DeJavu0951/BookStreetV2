namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về nhà phân phối
    /// </summary>
    public class Distributor
    {
        public int DistributorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
