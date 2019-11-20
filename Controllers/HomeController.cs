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
            else
            {
                var juridic = await GetJuridicAsync(user.Id);
                if (juridic != null)
                    return View("Juridic", juridic);
                else
                    return NotFound();
            }
        }

        [Authorize]
        public IActionResult Table()
        {
            return View("Table");
        }

        [Authorize]
        public IActionResult Accounts()
        {
            return View("Accounts");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Deposit(float amount)
        {
            var defaultAccount = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User); //Current logged user

            var natural = await GetNaturalAsync(user.Id);
            if (natural != null)
            {
                natural.SavingAccounts[defaultAccount].Amount += amount;
                return View("Natural", natural);
            }
            else
            {
                var juridic = await GetJuridicAsync(user.Id);
                if (juridic != null)
                    return View("Juridic", juridic);
                else
                    return NotFound();
            }
        }

        private async Task<NaturalPerson> GetNaturalAsync(string Id)
        {
            var natural = await _dbContext.NaturalPersons.FindAsync(Id);
            if (natural == null) return null;

            natural.SavingAccounts = await _dbContext.SavingAccounts
                .Where(np => np.NaturalPersonId == natural.UserId)
                .Include(deposits => deposits.DepositCertificates)
                .Include(withdrawals => withdrawals.WithdrawalCertificates)
                .ToListAsync();
            return natural;
        }

        private async Task<JuridicPerson> GetJuridicAsync(string Id)
        {
            var juridic = await _dbContext.JuridicPersons.FindAsync(Id);
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