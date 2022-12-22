using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.Budget;
using Xunit;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Domain;
using Microsoft.EntityFrameworkCore;
namespace FamilyFinanceTests.Repositories
{
    public class UserOperationRepositoryTests
    {

        private readonly IUserOperationRepository _sut = GetInMemoryEntityRepository();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        private static IUserOperationRepository GetInMemoryEntityRepository()
        {
            DbContextOptions<FFDbContext> options;
            var builder = new DbContextOptionsBuilder<FFDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            FFDbContext? categoryDataContext = new FFDbContext(options);
            categoryDataContext.Database.EnsureDeleted();
            categoryDataContext.Database.EnsureCreated();
            return new UserOperationRepository(categoryDataContext);

        }

        private readonly UserOperation? _testEntity1 = new UserOperation()
        {
            Id = 60,
            Date = DateTime.Parse("10.11.2022"),
            BudgetItemId = 8,
            SumBudgetItem = 500,
            UserId = 1
        };
        private readonly UserOperation? _testEntity2 = new UserOperation()
        {
            Id = 61,
            Date = DateTime.Parse("11.11.2022"),
            BudgetItemId = 1,
            SumBudgetItem = 50000,
            UserId = 1
        };
        private readonly UserOperation? _testEntity3 = new UserOperation()
        {
            Id = 62,
            Date = DateTime.Parse("12.11.2022"),
            BudgetItemId = 6,
            SumBudgetItem = 1293,
            UserId = 1
        };



        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var writeCategory = _sut.AddAsync(_testEntity1, cancellationToken);

            Assert.NotNull(writeCategory);
            Assert.Equal(writeCategory.Result?.BudgetItemId, _testEntity1?.BudgetItemId);
            Assert.Equal(writeCategory.Result?.Id, _testEntity1?.Id);
        }

        [Fact]
        public void CreateCategoryAsyncNullTest()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.AddAsync(null, cancellationToken));
        }

        [Fact]
        public async Task UpdateCategoryAsyncTestAsync()
        {
            var writeEntity = await this._sut.AddAsync(_testEntity1, cancellationToken);

            writeEntity.BudgetItemId = _testEntity2.BudgetItemId;


            Console.WriteLine();


            Assert.NotNull(writeEntity);
            Assert.Equal(writeEntity.BudgetItemId, _testEntity2.BudgetItemId);
        }

        [Fact]
        public void UpdateCategoryAsyncNullTestAsync()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.UpdateAsync(null, cancellationToken));
        }

        [Fact]
        public void DeleteCategoryAsyncInvalidDataTest()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.DeleteAsync(null, cancellationToken));
        }
        [Fact]
        public void DeleteCategoryAsyncTest()
        {
            var writeEntity = _sut.AddAsync(_testEntity1, cancellationToken);

            var res = _sut.DeleteAsync(writeEntity.Result, cancellationToken);

            var list = _sut.ListAllAsync(cancellationToken);
            Assert.DoesNotContain(writeEntity.Result, list.Result);
        }


        [Fact]
        public void GetAllCategoriesAsyncTest()
        {
            var writeEntity1 = _sut.AddAsync(_testEntity1, cancellationToken);
            var writeEntity2 = _sut.AddAsync(_testEntity3, cancellationToken);

            var list = _sut.ListAllAsync(cancellationToken);

            Assert.NotNull(list);
            Assert.Contains(writeEntity1.Result, list.Result);
            Assert.Contains(writeEntity2.Result, list.Result);
        }

        [Fact]
        public void GetCategoryByIdAsyncInvalidDataTest()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.GetByIdAsync(-1, cancellationToken));
        }
        [Fact]
        public void GetCategoryByIdAsyncTest()
        {
            var writeEntity1 = _sut.AddAsync(_testEntity1, cancellationToken);
            var writeEntity2 = _sut.AddAsync(_testEntity3, cancellationToken);

            var result = _sut.GetByIdAsync(62, cancellationToken);

            Assert.NotNull(result);
            Assert.Equal(result.Result.Id, 62);
        }

    }
}