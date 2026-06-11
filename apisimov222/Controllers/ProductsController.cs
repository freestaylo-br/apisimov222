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
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.ProductName)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var words = search
                    .ToLower()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {

                    query = query.Where(p =>
                        p.ProductName.Name.ToLower().Contains(word)
                        || p.Description.ToLower().Contains(word)
                        || p.Article.ToLower().Contains(word)
                        || p.Category.CategoryName.ToLower().Contains(word)
                        || p.Manufacturer.ManufacturerName.ToLower().Contains(word)
                        || p.Supplier.SupplierName.ToLower().Contains(word)
                        || p.UnitOfMeasurement.ToLower().Contains(word));
                }
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
                Photo = dto.Photo,

                CategoryId = dto.CategoryId,
                ManufacturerId = dto.ManufacturerId,
                SupplierId = dto.SupplierId
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products
                .FindAsync(id);

            if (product == null)
                return NotFound();

            var inOrder = await _context.Carts
                .AnyAsync(x => x.ProductId == id);

            if (inOrder)
            {
                return BadRequest(
                    "Товар присутствует в заказе и не может быть удалён");
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
    int id,
    ProductDto dto)
        {
            var product = await _context.Products
                .Include(x => x.ProductName)
                .FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            product.ProductName.Name =
                dto.ProductName;

            product.Description =
                dto.Description;

            product.Amount =
                dto.Amount;

            product.Discount =
                dto.Discount;

            product.Count =
                dto.Count;

            product.UnitOfMeasurement =
                dto.UnitOfMeasurement;

            product.Article =
                dto.Article;

            product.CategoryId =
                dto.CategoryId;

            product.ManufacturerId =
                dto.ManufacturerId;

            product.SupplierId =
                dto.SupplierId;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}