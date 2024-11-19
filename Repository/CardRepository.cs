using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class CardRepository : Repository<Card>, ICard
    {
        public CardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
