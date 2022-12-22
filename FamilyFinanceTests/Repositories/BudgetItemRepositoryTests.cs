using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.Budget;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Domain;
using FamilyFinance.Domain.Entities.Addition;

namespace FamilyFinanceTests.Repositories
{
    public class BudgetItemRepositoryTests
    {
        private readonly IBudgetItemRepository _sut = GetInMemoryBudgetItemRepository();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        private static IBudgetItemRepository GetInMemoryBudgetItemRepository()
        {
            DbContextOptions<FFDbContext> options;
            var builder = new DbContextOptionsBuilder<FFDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            FFDbContext? categoryDataContext = new FFDbContext(options);
            categoryDataContext.Database.EnsureDeleted();
            categoryDataContext.Database.EnsureCreated();
            return new BudgetItemRepository(categoryDataContext);

        }

        private readonly BudgetItem? _testEntity1 = new BudgetItem()
        {
           Id=60,
           BudgetType = BudgetType.Income,
           Item = "Test1"
        };
        private readonly BudgetItem _testEntity2 = new BudgetItem()
        {
            Id = 61,
            BudgetType = BudgetType.Income,
            Item = "Test2"
        };
        private readonly BudgetItem? _testEntity3 = new BudgetItem()
        {
            Id = 62,
            BudgetType = BudgetType.Income,
            Item = "Test3"
        };


        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var writeCategory = _sut.AddAsync(_testEntity1, cancellationToken);

            Assert.NotNull(writeCategory);
            Assert.Equal(writeCategory.Result?.BudgetType, _testEntity1?.BudgetType);
            Assert.Equal(writeCategory.Result?.Id, _testEntity1?.Id);
            Assert.Equal(writeCategory.Result?.Item, _testEntity1?.Item);
        }
        [Fact]
        public void CreateCategoryAsyncNullTest()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.AddAsync(null,cancellationToken));
        }

        [Fact]
        public async Task UpdateCategoryAsyncTestAsync()
        {
            var writeEntity = await this._sut.AddAsync(_testEntity1, cancellationToken);

            writeEntity.Item = _testEntity2.Item;


            Console.WriteLine();


            Assert.NotNull(writeEntity);
            Assert.Equal(writeEntity.Item, _testEntity2.Item);
        }

        [Fact]
        public void UpdateCategoryAsyncNullTestAsync()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.UpdateAsync(null,cancellationToken));
        }

        [Fact]
        public void DeleteCategoryAsyncInvalidDataTest()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.DeleteAsync(null,cancellationToken));
        }
        [Fact]
        public void DeleteCategoryAsyncTest()
        {
            var writeEntity = _sut.AddAsync(_testEntity1,cancellationToken);

            var res = _sut.DeleteAsync(writeEntity.Result,cancellationToken);

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
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _sut.GetByIdAsync(-1,cancellationToken));
        }
        [Fact]
        public void GetCategoryByIdAsyncTest()
        {
            var writeEntity1 = _sut.AddAsync(_testEntity1,cancellationToken);
            var writeEntity2 = _sut.AddAsync(_testEntity3,cancellationToken);

            var result = _sut.GetByIdAsync(62,cancellationToken);

            Assert.NotNull(result);
            Assert.Equal(result.Result, _testEntity3);
        }

    }
}