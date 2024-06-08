namespace Application.Extentions
{
    public class DataResult<T> where T : class
    {
        public long RecordsTotal { get; set; }
        public long RecordsFiltered { get; set; }
        public List<T>? List { get; set; }
        public Pagination? Pagination { get; set; }
        public Dictionary<string, decimal> TotalColumns { get; set; }
        public Dictionary<string, object> TotalGroupColumns { get; set; }
    }

    public class CustomDataResult<T> : DataResult<T> where T : class
    {
        public Dictionary<string, decimal> TotalColumns { get; set; }
    }

    public class Pagination
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public long Total { get; set; }
    }
}
