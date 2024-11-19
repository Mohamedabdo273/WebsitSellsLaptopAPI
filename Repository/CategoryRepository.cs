using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
