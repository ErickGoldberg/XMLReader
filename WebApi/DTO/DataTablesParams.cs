namespace WebApi.DTO
{
    public class DataTablesParams
    {
        public int Draw { get; set; } 
        public int Start { get; set; } 
        public int Length { get; set; } 
        public string SearchValue { get; set; } 
        public bool IsSearchRegex { get; set; } 
        public List<ColumnOrder> Order { get; set; } 
        public List<ColumnSearch> ColumnSearches { get; set; } 
    }

    public class ColumnOrder
    {
        public int Column { get; set; } 
        public string Dir { get; set; } 
    }

    public class ColumnSearch
    {
        public int Column { get; set; } 
        public string Value { get; set; } 
        public bool IsRegex { get; set; } 
    }
}
