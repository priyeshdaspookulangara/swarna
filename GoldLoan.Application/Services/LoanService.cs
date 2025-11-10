using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using GoldLoan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IFinancialCalculatorService _calculatorService;
        private readonly IJournalEntryService _journalEntryService;
        private readonly IAccountRepository _accountRepository;
        private Dictionary<string, int> _accountMappings;

        public LoanService(ILoanRepository loanRepository, IFinancialCalculatorService calculatorService, IJournalEntryService journalEntryService, IAccountRepository accountRepository)
        {
            _loanRepository = loanRepository;
            _calculatorService = calculatorService;
            _journalEntryService = journalEntryService;
            _accountRepository = accountRepository;
            _accountMappings = new Dictionary<string, int>();
        }

        private async Task InitializeAccountMappings()
        {
            if (_accountMappings.Count == 0)
            {
                var accounts = await _accountRepository.ListAllAsync();
                foreach (var account in accounts)
                {
                    _accountMappings.Add(account.AccountName, account.Id);
                }
            }
        }

        public async Task<LoanDto> CreateLoanAsync(LoanDto loanDto)
        {
            await InitializeAccountMappings();
            var loan = new Loan
            {
                ClientId = loanDto.ClientId,
                PrincipalAmount = loanDto.PrincipalAmount,
                AnnualInterestRate = loanDto.AnnualInterestRate,
                LoanDate = loanDto.LoanDate,
                TenureInMonths = loanDto.TenureInMonths,
                Status = loanDto.Status,
                RepaymentType = loanDto.RepaymentType,
                CollateralItems = loanDto.CollateralItems.Select(c => new CollateralItem
                {
                    Description = c.Description,
                    WeightInGrams = c.WeightInGrams,
                    PurityInKarat = c.PurityInKarat
                }).ToList()
            };

            if (loan.RepaymentType == RepaymentType.EMI)
            {
                GenerateRepaymentSchedule(loan);
            }

            var newLoan = await _loanRepository.AddAsync(loan);
            loanDto.Id = newLoan.Id;

            // Create a journal entry for the new loan
            var journalEntry = new JournalEntry
            {
                EntryDate = DateTime.UtcNow,
                Description = $"Loan disbursement for client {loan.ClientId}",
                Lines = new List<JournalEntryLine>
                {
                    new JournalEntryLine { AccountId = _accountMappings["Loans Receivable"], Debit = loan.PrincipalAmount, Credit = 0 },
                    new JournalEntryLine { AccountId = _accountMappings["Cash"], Debit = 0, Credit = loan.PrincipalAmount }
                }
            };
            await _journalEntryService.CreateJournalEntryAsync(journalEntry);

            return loanDto;
        }

        private void GenerateRepaymentSchedule(Loan loan)
        {
            loan.RepaymentSchedules = new List<RepaymentSchedule>();
            var emi = _calculatorService.CalculateEmi(loan.PrincipalAmount, loan.AnnualInterestRate, loan.TenureInMonths);
            var balance = loan.PrincipalAmount;
            var monthlyRate = loan.AnnualInterestRate / 12 / 100;

            for (int i = 1; i <= loan.TenureInMonths; i++)
            {
                var interestComponent = balance * monthlyRate;
                var principalComponent = emi - interestComponent;
                balance -= principalComponent;

                loan.RepaymentSchedules.Add(new RepaymentSchedule
                {
                    DueDate = loan.LoanDate.AddMonths(i),
                    InstallmentAmount = emi,
                    PrincipalComponent = principalComponent,
                    InterestComponent = interestComponent,
                    Status = RepaymentStatus.Pending
                });
            }
        }

        public async Task<LoanDto?> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null) return null;

            return new LoanDto
            {
                Id = loan.Id,
                ClientId = loan.ClientId,
                ClientName = loan.Client.Name,
                PrincipalAmount = loan.PrincipalAmount,
                AnnualInterestRate = loan.AnnualInterestRate,
                LoanDate = loan.LoanDate,
                TenureInMonths = loan.TenureInMonths,
                Status = loan.Status,
                RepaymentType = loan.RepaymentType,
                CollateralItems = loan.CollateralItems.Select(c => new CollateralItemDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    WeightInGrams = c.WeightInGrams,
                    PurityInKarat = c.PurityInKarat
                }).ToList(),
                RepaymentSchedules = loan.RepaymentSchedules.Select(rs => new RepaymentScheduleDto
                {
                    Id = rs.Id,
                    DueDate = rs.DueDate,
                    InstallmentAmount = rs.InstallmentAmount,
                    PrincipalComponent = rs.PrincipalComponent,
                    InterestComponent = rs.InterestComponent,
                    Status = rs.Status
                }).ToList()
            };
        }

        public async Task<IReadOnlyList<LoanDto>> GetLoansForClientAsync(int clientId)
        {
            var loans = await _loanRepository.GetLoansByClientIdAsync(clientId);
            var loanDtos = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                ClientId = loan.ClientId,
                PrincipalAmount = loan.PrincipalAmount,
                AnnualInterestRate = loan.AnnualInterestRate,
                LoanDate = loan.LoanDate,
                TenureInMonths = loan.TenureInMonths,
                Status = loan.Status,
                RepaymentType = loan.RepaymentType
            }).ToList();

            return loanDtos;
        }

        public async Task UpdateLoanAsync(LoanDto loanDto)
        {
            var loan = await _loanRepository.GetByIdAsync(loanDto.Id);
            if (loan != null)
            {
                loan.Status = loanDto.Status;
                // In a real application, you would map all the properties
                // For now, we only need to update the repayment schedule status
                foreach (var scheduleDto in loanDto.RepaymentSchedules)
                {
                    var schedule = loan.RepaymentSchedules.FirstOrDefault(s => s.Id == scheduleDto.Id);
                    if (schedule != null)
                    {
                        schedule.Status = scheduleDto.Status;
                    }
                }
                await _loanRepository.UpdateAsync(loan);
            }
        }
    }
}
