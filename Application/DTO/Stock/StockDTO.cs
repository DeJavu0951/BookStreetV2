namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về kho sách
    /// </summary>
    public class StockDTO
    {
        public int BookId { get; set; }
        public BookDTO Book { get; set; }
        public int StoreId { get; set; }
        public StoreDTO Store { get; set; }
        public int Status { get; set; }
    }
}
