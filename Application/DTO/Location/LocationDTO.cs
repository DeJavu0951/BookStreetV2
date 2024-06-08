namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về vị trí
    /// </summary>
    public class LocationDTO
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int StoreId { get; set; }
        public StoreDTO Store { get; set; }
    }
}
