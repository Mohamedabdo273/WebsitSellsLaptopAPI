using AutoMapper;
using WebsitSellsLaptop.DTO;
using WebsitSellsLaptop.Models;
namespace WebsitSellsLaptop.Profiles
{
    public class ApplicationUserProfile : Profile
    {

        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUserDto,ApplicationUser>();
        }
    }
}
