using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository;
using WebsitSellsLaptop.Repository.IRepository;
using WebsitSellsLaptop.Utility;

namespace WebsitSellsLaptop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(SD.adminRole)]
    public class CrudProductController : ControllerBase
    {
        private readonly IProduct product;

        public CrudProductController(IProduct product)
        {
            this.product = product;
        }
        [HttpPost("Create")]
        public IActionResult Create(ProductImgs productImgs)
        {
            if (ModelState.IsValid)
            {
                if (productImgs.ImageUrl != null && productImgs.ImageUrl.Length > 0)
                {
                    var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImgs.ImageUrl.FileName);
                    var filePath = Path.Combine(imagesDirectory, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        productImgs.ImageUrl.CopyTo(stream);
                    }

                    productImgs.Product.ImgUrl = fileName;
                }

                product.Create(productImgs.Product);
                product.Commit();

                CookieOptions cookieOptions = new();
                cookieOptions.Expires = DateTime.Now.AddMinutes(1);

                Response.Cookies.Append("success", "Add Product Successfully", cookieOptions);

                return Ok(productImgs.Product);
            }

            return BadRequest(ModelState);
        }



    }
}
