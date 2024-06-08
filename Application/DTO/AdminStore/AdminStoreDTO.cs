namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về cửa hàng
    /// </summary>
    public class AdminStoreDTO
    {
        public int AdminStoreId { get; set; }
        public int? StoreId { get; set; }
        public StoreDTO? Store { get; set; }
    }
}
