namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về phố sách
    /// </summary>
    public class BookStreet
    {
        public int StreetId { get; set; }
        public string Name { get; set; }
        public DateTime? PublicDay { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public ICollection<Store>? Stores { get; set; }
        public ICollection<Location>? Locations { get; set; }
    }
}
