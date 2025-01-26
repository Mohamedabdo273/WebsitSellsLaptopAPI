using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsitSellsLaptop.Models;
using WebsitSellsLaptop.Repository.IRepository;
using WebsitSellsLaptop.Utility;

namespace WebsitSellsLaptop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.adminRole)] // Ensure SD.adminRole is properly defined
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categories = _category.Get();
            if (categories == null || !categories.Any()) // Check if categories exist
                return NotFound("No categories found.");
            return Ok(categories);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Category data is required.");

            if (ModelState.IsValid)
            {
                _category.Create(category);
                _category.Commit();
                return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
            }

            return BadRequest(ModelState); // Return validation errors
        }

        [HttpPut("Edit/{categoryId}")]
        public IActionResult Edit(int categoryId, [FromBody] Category category)
        {
            if (categoryId != category.Id)
                return BadRequest("Category ID mismatch.");

            var existingCategory = _category.GetOne(expression:e => e.Id == categoryId);
            if (existingCategory == null)
                return NotFound($"Category with ID {categoryId} not found.");

            if (ModelState.IsValid)
            {
                _category.Edit(category);
                _category.Commit();
                return NoContent(); // 204 No Content is more appropriate for an update
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("Delete/{categoryId}")]
        public IActionResult Delete(int categoryId)
        {
            var category = _category.GetOne(expression:e => e.Id == categoryId);
            if (category == null)
                return NotFound($"Category with ID {categoryId} not found.");

            _category.Delete(category);
            _category.Commit();
            return Ok($"Category with ID {categoryId} deleted successfully.");
        }
    }
}
