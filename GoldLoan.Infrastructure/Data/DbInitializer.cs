using GoldLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GoldLoan.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any clients.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Client[]
            {
                new Client{Name="John Doe", Address="123 Main St, Anytown, USA", ContactNumber="555-1234", IdProofDetails="A1B2C3D4"},
                new Client{Name="Jane Smith", Address="456 Oak Ave, Anytown, USA", ContactNumber="555-5678", IdProofDetails="E5F6G7H8"}
            };

            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var loans = new Loan[]
            {
                new Loan{ClientId=1, PrincipalAmount=10000, AnnualInterestRate=12, LoanDate=DateTime.Parse("2023-01-15"), TenureInMonths=12, Status=Domain.Enums.LoanStatus.Active, RepaymentType=Domain.Enums.RepaymentType.EMI},
            };

            foreach (Loan l in loans)
            {
                context.Loans.Add(l);
            }
            context.SaveChanges();
        }
    }
}