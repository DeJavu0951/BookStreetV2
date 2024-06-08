namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về cửa hàng
    /// </summary>
    public class Store
    {
        public int StoreId { get; set; }

        public int StreetId { get; set; }
        public BookStreet BookStreet { get; set; }

        public ICollection<Location> Locations { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<AdminStore> AdminStores { get; set; }
        public ICollection<KiosStore> KiosStores { get; set; }
    }
}
