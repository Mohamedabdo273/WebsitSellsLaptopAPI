using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using WebsitSellsLaptop.DTO;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Utility;

namespace WebsitSellsLaptop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(ApplicationUserDto userDto)
        {

            if (_roleManager.Roles.IsNullOrEmpty())
            {
                await _roleManager.CreateAsync(new(SD.adminRole));
                await _roleManager.CreateAsync(new(SD.CustomerRole));
            }


            if (ModelState.IsValid)
            {

                var user = mapper.Map<ApplicationUser>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Passwords);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.CustomerRole))
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole));

                    await _userManager.AddToRoleAsync(user, SD.CustomerRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok(new { Message = "Registration successful" });
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
            }
                return BadRequest(ModelState);
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(userVm.UserName);
            if (user == null)
                return Unauthorized(new { Message = "Invalid username or password" });

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, userVm.Password);
            if (!isPasswordValid)
                return Unauthorized(new { Message = "Invalid username or password" });

            await _signInManager.SignInAsync(user, userVm.RemeberMe);

            return Ok(new { Message = "Login successful" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }
}
