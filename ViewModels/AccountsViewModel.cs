using System.Collections.Generic;
using bank_bills.Models;

namespace bank_bills.ViewModels
{
    public class AccountsViewModel
    {
        public List<SavingAccount> SavingAccounts { get; set; }
        public List<CheckingAccount> CheckingAccounts { get; set; }

        public AccountsViewModel()
        {
            SavingAccounts = new List<SavingAccount>();
            CheckingAccounts = new List<CheckingAccount>();
        }
    }
}