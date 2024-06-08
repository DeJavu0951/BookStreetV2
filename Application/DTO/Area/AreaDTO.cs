namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về khu vực
    /// </summary>
    public class AreaDTO
    {
        public int AreaId { get; set; }
        public string Name { get; set; }

        public ICollection<BookStreetDTO> BookStreets { get; set; }
        public ICollection<KiosDTO> Kioses { get; set; }
    }
}
