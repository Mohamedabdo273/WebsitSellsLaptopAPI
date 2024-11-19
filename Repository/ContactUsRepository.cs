using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUs
    {
        public ContactUsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
