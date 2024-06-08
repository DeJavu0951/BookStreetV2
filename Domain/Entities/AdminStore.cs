namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về cửa hàng
    /// </summary>
    public class AdminStore
    {
        public int AdminStoreId { get; set; }
        public int? StoreId { get; set; }
        public Store? Store { get; set; }
    }
}
