namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về nhà xuất bản
    /// </summary>
    public class PublisherDTO
    {
        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Year { get; set; }
        public ICollection<BookDTO>? Books { get; set; }
    }
}
