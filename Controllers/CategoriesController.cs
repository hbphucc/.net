using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoriesController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepo.GetAllActiveAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
        {
            var category = new Category
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = request.CategoryName,
                Gender = request.Gender,
                Description = request.Description,
                Status = true
            };

            await _categoryRepo.AddAsync(category);
            return Ok(new { message = "Category added successfully!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryRequest request)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return NotFound(new { message = "Category not found" });

            category.CategoryName = request.CategoryName;
            category.Gender = request.Gender;
            category.Description = request.Description;

            await _categoryRepo.UpdateAsync(category);
            return Ok(new { message = "Update Success!" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var success = await _categoryRepo.SoftDeleteAsync(id);
            if (!success) return BadRequest(new { message = "Delete failed" });

            return Ok(new { message = "Category deleted successfully!" });
        }
    }
}