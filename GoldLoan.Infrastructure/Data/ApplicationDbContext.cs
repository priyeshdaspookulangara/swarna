using GoldLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoldLoan.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CollateralItem> CollateralItems { get; set; }
        public DbSet<RepaymentSchedule> RepaymentSchedules { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoanPlan> LoanPlans { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal properties to have a specific precision and scale
            modelBuilder.Entity<Loan>()
                .Property(l => l.PrincipalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Loan>()
                .Property(l => l.AnnualInterestRate)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<CollateralItem>()
                .Property(c => c.WeightInGrams)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<CollateralItem>()
                .Property(c => c.PurityInKarat)
                .HasColumnType("decimal(4, 2)");

            modelBuilder.Entity<RepaymentSchedule>()
                .Property(r => r.InstallmentAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RepaymentSchedule>()
                .Property(r => r.PrincipalComponent)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RepaymentSchedule>()
                .Property(r => r.InterestComponent)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<JournalEntryLine>()
                .Property(j => j.Debit)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<JournalEntryLine>()
                .Property(j => j.Credit)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
