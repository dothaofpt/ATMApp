using Microsoft.AspNetCore.Mvc;
using ATMApp.Data;
using ATMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMManagementApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ATMContext _context;

        public UserController(ATMContext context)
        {
            _context = context;
        }

        // API để lấy tất cả người dùng
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // API để lấy thông tin người dùng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Accounts).FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        // API để tạo người dùng mới
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // API để cập nhật thông tin người dùng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            user.Name = updatedUser.Name;
            user.Password = updatedUser.Password;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // API để xóa người dùng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
