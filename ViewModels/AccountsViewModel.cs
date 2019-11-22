using System.Collections.Generic;
using bank_bills.Models;

namespace bank_bills.ViewModels
{
    public class AccountsViewModel
    {
        public NaturalPerson NaturalPerson { get; set; }
        public JuridicPerson JuridicPerson { get; set; }
    }
}