using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FamilyFinance.Domain
{
    public class FFDbContext : DbContext
    {
        public FFDbContext(DbContextOptions<FFDbContext> options) : base(options)
        {
        }

        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<FinancialOperation> FinancialOperations { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly HMACSHA512 _hmac = new HMACSHA512();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FFDbContext).Assembly);

            builder.Entity<BudgetItem>().HasData(
                  new BudgetItem { Id = 1, BudgetType = BudgetType.Income, Item = "Salary"},
                  new BudgetItem { Id = 2, BudgetType = BudgetType.Income, Item = "Deposit interest" },
                  new BudgetItem { Id = 3, BudgetType = BudgetType.Income, Item = "Financial aid" },
                  new BudgetItem { Id = 4, BudgetType = BudgetType.Expense, Item = "Insurance" },
                  new BudgetItem { Id = 5, BudgetType = BudgetType.Expense, Item = "Products" },
                  new BudgetItem { Id = 6, BudgetType = BudgetType.Expense, Item = "Clothes" },
                  new BudgetItem { Id = 7, BudgetType = BudgetType.Expense, Item = "Communal payments" },
                  new BudgetItem { Id = 8, BudgetType = BudgetType.Expense, Item = "Rent" },
                  new BudgetItem { Id = 9, BudgetType = BudgetType.Expense, Item = "Medicine" },
                  new BudgetItem { Id = 10, BudgetType = BudgetType.Expense, Item = "Auto" },
                  new BudgetItem { Id = 11, BudgetType = BudgetType.Expense, Item = "Kindergarten/School" },
                  new BudgetItem { Id = 12, BudgetType = BudgetType.Expense, Item = "Presents" },
                  new BudgetItem { Id = 13, BudgetType = BudgetType.Expense, Item = "Travel, vacation" },
                  new BudgetItem { Id = 14, BudgetType = BudgetType.Expense, Item = "Emergency expenses" },
                  new BudgetItem { Id = 15, BudgetType = BudgetType.Expense, Item = "Taxes" },
                  new BudgetItem { Id = 16, BudgetType = BudgetType.Expense, Item = "Education" },
                  new BudgetItem { Id = 17, BudgetType = BudgetType.Expense, Item = "Loan payment" },
                  new BudgetItem { Id = 18, BudgetType = BudgetType.Expense, Item = "Entertainment" }
                  );

            builder.Entity<FinancialOperation>().HasData(
                  new FinancialOperation { Id = 1, FinOperation = "Credit"},
                  new FinancialOperation { Id = 2, FinOperation = "Deposit" },
                  new FinancialOperation { Id = 3, FinOperation = "Payment by instalments" },
                  new FinancialOperation { Id = 4, FinOperation = "Real estate investment" },
                  new FinancialOperation { Id = 5, FinOperation = "Investments in securities" }
                  );


            builder.Entity<UserOperation>().HasData(
                new UserOperation { Id = 1, Date = DateTime.Parse("10.11.2022"), BudgetItemId = 1, SumBudgetItem = 50000, UserId = 1 },
                new UserOperation { Id = 2, Date = DateTime.Parse("10.10.2022"), BudgetItemId = 1, SumBudgetItem = 50000, UserId = 1 },
                new UserOperation { Id = 3, Date = DateTime.Parse("10.09.2022"), BudgetItemId = 1, SumBudgetItem = 2000, UserId = 1 },
                new UserOperation { Id = 4, Date = DateTime.Parse("08.11.2022"), BudgetItemId = 7, SumBudgetItem = 2000, UserId = 1 },
                new UserOperation { Id = 5, Date = DateTime.Parse("04.10.2022"), BudgetItemId = 7, SumBudgetItem = 2000, UserId = 1 },
                new UserOperation { Id = 6, Date = DateTime.Parse("07.09.2022"), BudgetItemId = 7, SumBudgetItem = 2000, UserId = 1 },
                new UserOperation { Id = 7, Date = DateTime.Parse("10.09.2022"), FinOperationId = 2, SumFinOperation = 30000, UserId = 1 }
            );


             builder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Peter", LastName = "Parker", UserName = "peter_parker", PasswordSalt = _hmac.Key, PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes("spiderman"))},
                new User { Id = 2, FirstName = "Wanda", LastName = "Maximoff", UserName = "wanda_maximoff", PasswordSalt = _hmac.Key, PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes("ilovevision")) }
                );

        }

    }
}
