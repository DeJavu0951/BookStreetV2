namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về vị trí
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
