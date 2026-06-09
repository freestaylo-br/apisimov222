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
        public async Task<IActionResult> GetProducts(string search = "")
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductName)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                query = query.Where(x =>
                    x.ProductName.Name.ToLower().Contains(search)
                    || x.Description.ToLower().Contains(search)
                    || x.Article.ToLower().Contains(search)
                    || x.Category.CategoryName.ToLower().Contains(search)
                    || x.Manufacturer.ManufacturerName.ToLower().Contains(search)
                    || x.Supplier.SupplierName.ToLower().Contains(search)
                    || x.UnitOfMeasurement.ToLower().Contains(search));
            }

            var products = await query
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
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductName)
                .OrderBy(x => x.Count)
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

        [HttpGet("sort/desc")]
        public async Task<IActionResult> GetProductsDesc()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductName)
                .OrderByDescending(x => x.Count)
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            var productName = await _context.ProductNames
                .FirstOrDefaultAsync(x => x.Name == dto.ProductName);

            if (productName == null)
            {
                productName = new ProductName
                {
                    Name = dto.ProductName
                };

                _context.ProductNames.Add(productName);

                await _context.SaveChangesAsync();
            }

            var product = new Product
            {
                ProductNameId = productName.ProductNameId,
                Description = dto.Description,
                Amount = dto.Amount,
                Discount = dto.Discount,
                Count = dto.Count,
                UnitOfMeasurement = dto.UnitOfMeasurement,
                Article = dto.Article,
                CategoryId = dto.CategoryId,
                ManufacturerId = dto.ManufacturerId,
                SupplierId = dto.SupplierId
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}