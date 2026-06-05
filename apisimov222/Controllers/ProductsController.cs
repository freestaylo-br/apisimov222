using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisimov222.Models;

namespace apisimov222.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductName)
                .Select(x => new
                {
                    x.ProductId,
                    ProductName = x.ProductName.Name,
                    x.Article,
                    x.Description,
                    x.Amount,
                    x.Discount,
                    x.Count,
                    x.Photo,
                    x.UnitOfMeasurement,

                    Category = x.Category.CategoryName,
                    Manufacturer = x.Manufacturer.ManufacturerName,
                    Supplier = x.Supplier.SupplierName
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("sort/asc")]
        public async Task<IActionResult> GetProductsAsc()
        {
            var products = await _context.Products
                .OrderBy(x => x.Count)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("sort/desc")]
        public async Task<IActionResult> GetProductsDesc()
        {
            var products = await _context.Products
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return Ok(products);
        }
    }
}