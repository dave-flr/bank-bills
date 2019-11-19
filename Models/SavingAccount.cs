namespace bank_bills.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public string FreezeStartDate { get; set; }
        public string FreezeEndDate { get; set; }
        public string CurrencyType { get; set; }
        public bool Freezed { get; set; }
        public float Amount { get; set; }
        public float AmountGained { get; set; }
        public int NaturalPersonId { get; set; }
        public NaturalPerson NaturalPerson { get; set; }
        public int JuridicPersonId { get; set; }
        public JuridicPerson JuridicPerson { get; set; }
    }
}