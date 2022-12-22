using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.Budget;
using Xunit;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Domain;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinanceTests.Repositories
{
    public class FinancialOperationRepositoryTests
    {
        private readonly IFinancialOperationRepository _sut = GetInMemoryEntityRepository();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        private static IFinancialOperationRepository GetInMemoryEntityRepository()
        {
            DbContextOptions<FFDbContext> options;
            var builder = new DbContextOptionsBuilder<FFDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            FFDbContext? categoryDataContext = new FFDbContext(options);
            categoryDataContext.Database.EnsureDeleted();
            categoryDataContext.Database.EnsureCreated();


            return new FinancialOperationRepository(categoryDataContext);

        }

        private readonly FinancialOperation? _testEntity1 = new FinancialOperation()
        {
            Id = 60,
            FinOperation = "Test1"
        };
        private readonly FinancialOperation? _testEntity2 = new FinancialOperation()
        {
            Id = 61,
            FinOperation = "Test2"
        };
        private readonly FinancialOperation? _testEntity3 = new FinancialOperation()
        {
            Id = 62,
            FinOperation = "Test3"
        };


        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var writeCategory = _sut.AddAsync(_testEntity1, cancellationToken);

            Assert.NotNull(writeCategory);
            Assert.Equal(writeCategory.Result?.FinOperation, _testEntity1?.FinOperation);
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

            writeEntity.FinOperation = _testEntity2.FinOperation;


            Console.WriteLine();


            Assert.NotNull(writeEntity);
            Assert.Equal(writeEntity.FinOperation, _testEntity2.FinOperation);
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
            
            Assert.NotNull(list.Result);
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
            var writeEntity1 = _sut.AddAsync(_testEntity1, cancellationToken).Result;
            var writeEntity2 = _sut.AddAsync(_testEntity3, cancellationToken).Result;

            var result = _sut.GetByIdAsync(62, cancellationToken);
            
            Assert.NotNull(result.Result);
            Assert.Equal(result.Result.Id, 62);
        }

    }
}