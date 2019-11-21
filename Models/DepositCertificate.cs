using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_bills.Models
{
    public class DepositCertificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DateHour { get; set; }
        public string Currency { get; set; }
        public float Amount { get; set; }
        public float Balance { get; set; }
        public string SavingAccountId { get; set; }
        public SavingAccount SavingAccount { get; set; }
        public string CheckingAccountId { get; set; }
        public CheckingAccount CheckingAccount { get; set; }
    }
}