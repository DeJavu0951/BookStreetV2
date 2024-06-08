namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về kios cửa hàng
    /// </summary>
    public class KiosStore
    {
        public int KiosId { get; set; }
        public Kios Kios { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
