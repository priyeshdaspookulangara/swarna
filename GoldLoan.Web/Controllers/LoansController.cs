using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Web.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IClientService _clientService;

        public LoansController(ILoanService loanService, IClientService clientService)
        {
            _loanService = loanService;
            _clientService = clientService;
        }

        // GET: Loans/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _clientService.GetAllClientsAsync();
            var viewModel = new LoanViewModel
            {
                Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };
            return View(viewModel);
        }

        // POST: Loans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanViewModel loanViewModel)
        {
            if (ModelState.IsValid)
            {
                var loanDto = new LoanDto
                {
                    ClientId = loanViewModel.ClientId,
                    PrincipalAmount = loanViewModel.PrincipalAmount,
                    AnnualInterestRate = loanViewModel.AnnualInterestRate,
                    TenureInMonths = loanViewModel.TenureInMonths,
                    RepaymentType = loanViewModel.RepaymentType,
                    LoanDate = System.DateTime.UtcNow,
                    Status = Domain.Enums.LoanStatus.Active,
                    CollateralItems = loanViewModel.CollateralItems.Select(c => new CollateralItemDto
                    {
                        Description = c.Description,
                        WeightInGrams = c.WeightInGrams,
                        PurityInKarat = c.PurityInKarat
                    }).ToList()
                };

                var createdLoan = await _loanService.CreateLoanAsync(loanDto);
                return RedirectToAction("Details", "Clients", new { id = createdLoan.ClientId });
            }

            // If we got this far, something failed, redisplay form
            var clients = await _clientService.GetAllClientsAsync();
            loanViewModel.Clients = clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            return View(loanViewModel);
        }

        // GET: Loans
        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var loans = await _loanService.GetLoansForClientAsync(clientId);

            var viewModel = new LoanIndexViewModel
            {
                ClientId = clientId,
                ClientName = client.Name,
                Loans = loans
            };

            return View(viewModel);
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }
    }
}
