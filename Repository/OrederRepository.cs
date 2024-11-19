using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class OrederRepository : Repository<Orders>, IOrder
    {
        public OrederRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
