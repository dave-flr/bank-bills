using System.Collections.Generic;
using bank_bills.Models;

namespace bank_bills.ViewModels
{
    public class MovementsViewModel
    {
        public List<DepositCertificate> DepositCertificates { get; set; }
        public List<WithdrawalCertificate> WithdrawalCertificates { get; set; }

        public MovementsViewModel()
        {
            DepositCertificates = new List<DepositCertificate>();
            WithdrawalCertificates = new List<WithdrawalCertificate>();
        }
    }
}