namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về kios
    /// </summary>
    public class Kios
    {
        public int KiosId { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public ICollection<KiosStore> KiosStores { get; set; }
    }
}
