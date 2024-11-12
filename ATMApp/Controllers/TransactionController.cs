using Microsoft.AspNetCore.Mvc;
using ATMApp.Data;
using ATMApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMManagementApp.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ATMContext _context;

        public TransactionController(ATMContext context)
        {
            _context = context;
        }

        // API để lấy tất cả giao dịch cho một tài khoản
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .ToListAsync();

            if (!transactions.Any()) return NotFound("No transactions found for this account.");
            return Ok(transactions);
        }

        // API để lấy thông tin giao dịch theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Transaction not found.");
            return Ok(transaction);
        }

        // API để tạo giao dịch mới (có thể dùng khi không có quy trình nạp/rút tiền)
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
        }

        // API để xóa giao dịch
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Transaction not found.");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
