namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về sự kiện
    /// </summary>
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
