using Microsoft.AspNetCore.Mvc;
using FashionShopAPI.Repositories.Interfaces;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;

namespace FashionShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts(
        [FromQuery] string? search,
        [FromQuery] string? categoryId,
        [FromQuery] string? gender,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10) 
        {
            var pagedData = await _productRepo.GetAllActiveAsync(search, categoryId, gender, page, pageSize);

            var response = new PagedResult<ProductResponse>
            {
                TotalItems = pagedData.TotalItems,
                PageNumber = pagedData.PageNumber,
                PageSize = pagedData.PageSize,
                Items = pagedData.Items.Select(p => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    Gender = p.Category.Gender,
                    Price = p.Price,
                    Img = p.Img,
                    Description = p.Description,
                    TotalStock = p.ProductSizes.Sum(ps => ps.Stock),
                    SizeQuantities = p.ProductSizes.ToDictionary(ps => ps.Size, ps => ps.Stock)
                })
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var p = await _productRepo.GetByIdAsync(id);
            if (p == null) return NotFound(new { message = "Product not found!" });

            var response = new ProductResponse
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName,
                Gender = p.Category.Gender,
                Price = p.Price,
                Img = p.Img,
                Description = p.Description,
                TotalStock = p.ProductSizes.Sum(ps => ps.Stock),
                SizeQuantities = p.ProductSizes.ToDictionary(ps => ps.Size, ps => ps.Stock)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            var newProduct = new Product
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Description = request.Description,
                Img = request.Img,
                Status = true,
                CreatedAt = DateTime.Now
            };

            await _productRepo.AddAsync(newProduct, request.SizeQuantities);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, new { message = "Thêm sản phẩm thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productRepo.SoftDeleteAsync(id);
            return Ok(new { message = "Product deleted successfully!" });
        }
    }
}
