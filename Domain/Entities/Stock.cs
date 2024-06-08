namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về kho sách
    /// </summary>
    public class Stock
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int Status { get; set; }
    }
}
