using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bank_bills.Models;
using bank_bills.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bank_bills.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private const int DefaultAccount = 0;

        public HomeController(ILogger<HomeController> logger, BankDbContext dbContext,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user

            var natural = await GetNaturalAsync(user.Id);
            if (natural != null)
                return View("Natural", natural);

            var juridic = await GetJuridicAsync(user.Id);
            if (juridic != null)
                return View("Juridic", juridic);
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Table()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user
            var movements = new MovementsViewModel();

            var natural = await GetNaturalAsync(user.Id);
            if (natural != null)
            {
                movements.DepositCertificates.AddRange(
                    natural.SavingAccounts[DefaultAccount].DepositCertificates
                        .OrderBy(certificate => certificate.DateHour));
                movements.WithdrawalCertificates.AddRange(
                    natural.SavingAccounts[DefaultAccount].WithdrawalCertificates
                        .OrderBy(certificate => certificate.DateHour));
                return View("Table", movements);
            }

            var juridic = await GetJuridicAsync(user.Id);
            if (juridic != null)
            {
                movements.DepositCertificates.AddRange(juridic.SavingAccounts[DefaultAccount].DepositCertificates
                    .OrderBy(certificate => certificate.DateHour));
                movements.WithdrawalCertificates.AddRange(juridic.SavingAccounts[DefaultAccount].WithdrawalCertificates
                    .OrderBy(certificate => certificate.DateHour));
                return View("Table", movements);
            }

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Accounts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user
            var accounts = new AccountsViewModel {NaturalPerson = await GetNaturalAsync(user.Id)};

            if (accounts.NaturalPerson != null)
                return View("Accounts", accounts);
            
            accounts.JuridicPerson = await GetJuridicAsync(user.Id);
            if (accounts.JuridicPerson != null)
                return View("Accounts", accounts);
            
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Deposits(float amount)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user

            var natural = await GetNaturalAsync(user.Id);
            if (natural != null)
            {
                natural.SavingAccounts[DefaultAccount].Amount += amount;
                var depositCertificate = new DepositCertificate
                {
                    Amount = amount,
                    Balance = natural.SavingAccounts[DefaultAccount].Amount,
                    Currency = "US Dollar",
                    DateHour = DateTime.Now.ToString("f"),
                    SavingAccountId = natural.SavingAccounts[DefaultAccount].Id
                };
                await _dbContext.DepositCertificates.AddAsync(depositCertificate);
                await _dbContext.SaveChangesAsync();
                return View("Natural", natural);
//                return LocalRedirect(returnUrl);
            }

            var juridic = await GetJuridicAsync(user.Id);
            if (juridic != null)
            {
                juridic.SavingAccounts[DefaultAccount].Amount += amount;
                var depositCertificate = new DepositCertificate
                {
                    Amount = amount,
                    Balance = juridic.SavingAccounts[DefaultAccount].Amount,
                    Currency = "US Dollar",
                    DateHour = DateTime.Now.ToString("f"),
                    SavingAccountId = juridic.SavingAccounts[DefaultAccount].Id
                };
                await _dbContext.DepositCertificates.AddAsync(depositCertificate);
                await _dbContext.SaveChangesAsync();
                return View("Juridic", juridic);
//                    return LocalRedirect(returnUrl);
            }

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Withdrawals(float amount)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user

            var natural = await GetNaturalAsync(user.Id);
            if (natural != null)
            {
                if (natural.SavingAccounts[DefaultAccount].Amount < amount)
                {
                    ViewData["NotEnough"] = true;
                    return View("Natural", natural);
                }

                natural.SavingAccounts[DefaultAccount].Amount -= amount;
                var withdrawalCertificate = new WithdrawalCertificate()
                {
                    Amount = amount,
                    Balance = natural.SavingAccounts[DefaultAccount].Amount,
                    Currency = "US Dollar",
                    DateHour = DateTime.Now.ToString("f"),
                    SavingAccountId = natural.SavingAccounts[DefaultAccount].Id
                };
                await _dbContext.WithdrawalCertificates.AddAsync(withdrawalCertificate);
                await _dbContext.SaveChangesAsync();
                return View("Natural", natural);
//                return LocalRedirect(returnUrl);
            }

            var juridic = await GetJuridicAsync(user.Id);
            if (juridic != null)
            {
                if (juridic.SavingAccounts[DefaultAccount].Amount < amount)
                {
                    ViewData["NotEnough"] = true;
                    return View("Juridic", juridic);
                }

                juridic.SavingAccounts[DefaultAccount].Amount -= amount;
                var withdrawalCertificate = new WithdrawalCertificate
                {
                    Amount = amount,
                    Balance = juridic.SavingAccounts[DefaultAccount].Amount,
                    Currency = "US Dollar",
                    DateHour = DateTime.Now.ToString("f"),
                    SavingAccountId = juridic.SavingAccounts[DefaultAccount].Id
                };
                await _dbContext.WithdrawalCertificates.AddAsync(withdrawalCertificate);
                await _dbContext.SaveChangesAsync();
                return View("Juridic", juridic);
//                    return LocalRedirect(returnUrl);
            }

            return NotFound();
        }

        private async Task<NaturalPerson> GetNaturalAsync(string id)
        {
            var natural = await _dbContext.NaturalPersons.FindAsync(id);
            if (natural == null) return null;

            natural.SavingAccounts = await _dbContext.SavingAccounts
                .Where(np => np.NaturalPersonId == natural.UserId)
                .Include(deposits => deposits.DepositCertificates)
                .Include(withdrawals => withdrawals.WithdrawalCertificates)
                .ToListAsync();
            return natural;
        }

        private async Task<JuridicPerson> GetJuridicAsync(string id)
        {
            var juridic = await _dbContext.JuridicPersons.FindAsync(id);
            if (juridic == null) return null;

            juridic.SavingAccounts = await _dbContext.SavingAccounts
                .Where(jp => jp.JuridicPersonId == juridic.UserId)
                .Include(deposits => deposits.DepositCertificates)
                .Include(withdrawals => withdrawals.WithdrawalCertificates)
                .ToListAsync();

            return juridic;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}