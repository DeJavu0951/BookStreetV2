namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về thể loại sách
    /// </summary>
    public class GenreDTO
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
