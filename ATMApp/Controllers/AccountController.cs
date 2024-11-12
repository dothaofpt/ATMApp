using Microsoft.AspNetCore.Mvc;
using ATMApp.Data;
using ATMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMManagementApp.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ATMContext _context;

        public AccountController(ATMContext context)
        {
            _context = context;
        }

        // API để lấy thông tin tài khoản theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _context.Accounts.Include(a => a.Transactions).FirstOrDefaultAsync(a => a.AccountId == id);
            if (account == null) return NotFound("Account not found.");
            return Ok(account);
        }

        // API để tạo tài khoản mới
        [HttpPost]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountId }, account);
        }

        // API để nạp tiền vào tài khoản
        [HttpPost("{id}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound("Account not found.");

            account.Balance += amount;

            var transaction = new Transaction
            {
                AccountId = id,
                Amount = amount,
                IsSuccessful = true,
                Description = "Deposit"
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok(new { account.Balance });
        }

        // API để rút tiền từ tài khoản
        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound("Account not found.");

            if (account.Balance < amount)
                return BadRequest("Insufficient funds.");

            account.Balance -= amount;

            var transaction = new Transaction
            {
                AccountId = id,
                Amount = -amount,
                IsSuccessful = true,
                Description = "Withdrawal"
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok(new { account.Balance });
        }
    }
}
