using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Web.Controllers
{
    public class RepaymentsController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IJournalEntryService _journalEntryService;
        private readonly IAccountRepository _accountRepository;
        private Dictionary<string, int> _accountMappings;

        public RepaymentsController(ILoanService loanService, IJournalEntryService journalEntryService, IAccountRepository accountRepository)
        {
            _loanService = loanService;
            _journalEntryService = journalEntryService;
            _accountRepository = accountRepository;
            _accountMappings = new Dictionary<string, int>();
            InitializeAccountMappings();
        }

        private async void InitializeAccountMappings()
        {
            var accounts = await _accountRepository.ListAllAsync();
            foreach (var account in accounts)
            {
                _accountMappings.Add(account.AccountName, account.Id);
            }
        }

        public async Task<IActionResult> Index()
        {
            var journalEntries = await _journalEntryService.GetAllJournalEntriesAsync();
            return View(journalEntries);
        }

        public async Task<IActionResult> Create(int loanId)
        {
            var loan = await _loanService.GetLoanByIdAsync(loanId);
            if (loan == null)
            {
                return NotFound();
            }

            var viewModel = new RepaymentViewModel
            {
                LoanId = loan.Id,
                Amount = loan.RepaymentSchedules.FirstOrDefault(rs => rs.Status == Domain.Enums.RepaymentStatus.Pending)?.InstallmentAmount ?? 0
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepaymentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var loan = await _loanService.GetLoanByIdAsync(viewModel.LoanId);
                if (loan == null)
                {
                    return NotFound();
                }

                // Create a journal entry for the repayment
                var journalEntry = new JournalEntry
                {
                    EntryDate = System.DateTime.UtcNow,
                    Description = $"Repayment for loan {loan.Id}",
                    Lines = new List<JournalEntryLine>
                    {
                        new JournalEntryLine { AccountId = _accountMappings["Cash"], Debit = viewModel.Amount, Credit = 0 },
                        new JournalEntryLine { AccountId = _accountMappings["Loans Receivable"], Debit = 0, Credit = viewModel.Amount }
                    }
                };
                await _journalEntryService.CreateJournalEntryAsync(journalEntry);

                // Update the repayment schedule
                var repaymentSchedule = loan.RepaymentSchedules.FirstOrDefault(rs => rs.Status == Domain.Enums.RepaymentStatus.Pending);
                if (repaymentSchedule != null)
                {
                    repaymentSchedule.Status = Domain.Enums.RepaymentStatus.Paid;
                    await _loanService.UpdateLoanAsync(loan);
                }

                return RedirectToAction("Details", "Loans", new { id = viewModel.LoanId });
            }

            return View(viewModel);
        }
    }
}
