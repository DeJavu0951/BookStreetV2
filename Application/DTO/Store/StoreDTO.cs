namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về cửa hàng
    /// </summary>
    public class StoreDTO
    {
        public int StoreId { get; set; }

        public int StreetId { get; set; }
        public BookStreetDTO BookStreet { get; set; }

        public ICollection<LocationDTO> Locations { get; set; }
        public ICollection<StockDTO> Stocks { get; set; }
        public ICollection<AdminStoreDTO> AdminStores { get; set; }
        public ICollection<KiosStoreDTO> KiosStores { get; set; }
    }
}
