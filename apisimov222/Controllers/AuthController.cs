using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisimov222.Models;
using apisimov222.Dtos;

namespace apisimov222.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Staff
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x =>
                    x.Login == dto.Login &&
                    x.Password == dto.Password);

            if (user == null)
            {
                return BadRequest("Неверный логин или пароль");
            }

            return Ok(new
            {
                user.StaffId,
                user.Surname,
                user.Name,
                user.Patronymic,
                user.Login,
                Role = user.Role.RoleName
            });
        }
    }
}