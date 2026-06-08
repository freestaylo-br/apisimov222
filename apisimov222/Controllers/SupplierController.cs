using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisimov222.Models;

namespace apisimov222.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly AppDbContext _context;

    public SuppliersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSuppliers()
    {
        var suppliers = await _context.Suppliers
            .Select(x => new
            {
                x.SupplierId,
                x.SupplierName
            })
            .ToListAsync();

        return Ok(suppliers);
    }
}