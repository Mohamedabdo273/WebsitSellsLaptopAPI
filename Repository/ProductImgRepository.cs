using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class ProductImgRepository : Repository<ProductImgs>, IProductImg
    {
        public ProductImgRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
