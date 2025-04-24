namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Response
{
    public class GetProductAsPaginatedListResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ProductImagesUrls { get; set; }
    }
}
