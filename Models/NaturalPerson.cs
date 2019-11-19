using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bank_bills.Models
{
    public class NaturalPerson
    {
        public NaturalPerson()
        {
            SavingAccounts = new List<SavingAccount>();
            CheckingAccounts = new List<CheckingAccount>();
        }

        //public int Id { get; set; }
        [Key] public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Cedula { get; set; }
        public string Pasaport { get; set; }
        public string CardNumber { get; set; }
        public string Direction { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }
        public List<SavingAccount> SavingAccounts { get; set; }
        public List<CheckingAccount> CheckingAccounts { get; set; }
    }
}