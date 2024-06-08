namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về khu vực
    /// </summary>
    public class Area
    {
        public int AreaId { get; set; }
        public string Name { get; set; }

        public ICollection<BookStreet> BookStreets { get; set; }
        public ICollection<Kios> Kioses { get; set; }
    }
}
