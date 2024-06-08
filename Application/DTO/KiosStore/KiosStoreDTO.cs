namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về kios cửa hàng
    /// </summary>
    public class KiosStoreDTO
    {
        public int KiosId { get; set; }
        public KiosDTO Kios { get; set; }

        public int StoreId { get; set; }
        public StoreDTO Store { get; set; }
    }
}
