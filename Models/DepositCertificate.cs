namespace bank_bills.Models
{
    public class DepositCertificate
    {

        public int Id { get; set; }
        public int SavingAccountId { get; set; }
        public SavingAccount SavingAccount { get; set; }
        public int CheckingAccountId { get; set; }
        public CheckingAccount CheckingAccount { get; set; }
        public string DateHour { get; set; }
        public string Currency { get; set; }
        public float Amount { get; set; }
        public float Balance { get; set; }
    }
}