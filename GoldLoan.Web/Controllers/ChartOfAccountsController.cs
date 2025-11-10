using GoldLoan.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoldLoan.Web.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public ChartOfAccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return View(accounts);
        }
    }
}
