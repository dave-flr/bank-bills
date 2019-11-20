namespace bank_bills.Models
{
    public class SavingAccount
    {
        public string Id { get; set; }
        public string CreationDate { get; set; }
        public string FreezeStartDate { get; set; }
        public string FreezeEndDate { get; set; }
        public string CurrencyType { get; set; }
        public bool Freezed { get; set; }
        public float Amount { get; set; }
        public float AmountGained { get; set; }
        public string NaturalPersonId { get; set; }
        public NaturalPerson NaturalPerson { get; set; }
        public string JuridicPersonId { get; set; }
        public JuridicPerson JuridicPerson { get; set; }
    }
}