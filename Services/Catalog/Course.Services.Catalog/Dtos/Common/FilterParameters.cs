namespace Course.Services.Catalog.Dtos.Common
{
    public class FilterParameters
    {
        public ICollection<string>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
