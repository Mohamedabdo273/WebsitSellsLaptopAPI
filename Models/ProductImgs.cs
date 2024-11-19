namespace WebsitSellsLaptop.Models
{
    public class ProductImgs
    {
        public int Id { get; set; }
        public IFormFile ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
