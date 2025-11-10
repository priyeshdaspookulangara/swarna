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
        private readonly ILoanPlanService _loanPlanService;

        public LoansController(ILoanService loanService, IClientService clientService, ILoanPlanService loanPlanService)
        {
            _loanService = loanService;
            _clientService = clientService;
            _loanPlanService = loanPlanService;
        }

        // GET: Loans/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _clientService.GetAllClientsAsync();
            var loanPlans = await _loanPlanService.GetAllLoanPlansAsync();
            var viewModel = new LoanViewModel
            {
                Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                LoanPlans = loanPlans.Select(lp => new SelectListItem
                {
                    Value = lp.Id.ToString(),
                    Text = lp.Name
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
                    LoanDate = System.DateTime.UtcNow,
                    Status = Domain.Enums.LoanStatus.Active,
                    CollateralItems = loanViewModel.CollateralItems.Select(c => new CollateralItemDto
                    {
                        Description = c.Description,
                        WeightInGrams = c.WeightInGrams,
                        PurityInKarat = c.PurityInKarat
                    }).ToList()
                };

                if (loanViewModel.LoanPlanId.HasValue)
                {
                    var loanPlan = (await _loanPlanService.GetAllLoanPlansAsync()).FirstOrDefault(lp => lp.Id == loanViewModel.LoanPlanId.Value);
                    if (loanPlan != null)
                    {
                        loanDto.AnnualInterestRate = loanPlan.AnnualInterestRate;
                        loanDto.TenureInMonths = loanPlan.TenureInMonths;
                        loanDto.RepaymentType = loanPlan.RepaymentType;
                    }
                }
                else
                {
                    loanDto.AnnualInterestRate = loanViewModel.AnnualInterestRate;
                    loanDto.TenureInMonths = loanViewModel.TenureInMonths;
                    loanDto.RepaymentType = loanViewModel.RepaymentType;
                }

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
            var loanPlans = await _loanPlanService.GetAllLoanPlansAsync();
            loanViewModel.LoanPlans = loanPlans.Select(lp => new SelectListItem
            {
                Value = lp.Id.ToString(),
                Text = lp.Name
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

        // GET: Loans/PrintReceipt/5
        public async Task<IActionResult> PrintReceipt(int id)
        {
            var transaction = await _loanService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View("Receipt", transaction);
        }
    }
}
