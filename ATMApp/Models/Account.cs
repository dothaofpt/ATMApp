using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace ATMApp.Models{

    public enum AccountType{
        Checking,//TK thanh toán
        Saving,//TK tiết kiệm
        Credit
    }
    public class Account{
        [Key]//Primary key in MySql
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public decimal Balance { get; set; }
        // public int AccountType { get; set; }
        public AccountType Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? InterestRate { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}