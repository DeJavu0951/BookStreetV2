namespace Application.DTO
{
    /// <summary>
    /// Lớp này chứa thông tin về kios
    /// </summary>
    public class KiosDTO
    {
        public int KiosId { get; set; }

        public int AreaId { get; set; }
        public AreaDTO Area { get; set; }

        public ICollection<KiosStoreDTO> KiosStores { get; set; }
    }
}
