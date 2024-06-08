namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về phố sách
    /// </summary>
    public class BookStreetDTO
    {
        public int StreetId { get; set; }
        public string Name { get; set; }
        public DateTime? PublicDay { get; set; }

        public int AreaId { get; set; }
        public AreaDTO Area { get; set; }

        public ICollection<StoreDTO>? Stores { get; set; }
        public ICollection<LocationDTO>? Locations { get; set; }
    }
}
