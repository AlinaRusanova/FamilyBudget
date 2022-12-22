using Xunit;
using FamilyFinance.Domain.Repositories.Identity;
using FamilyFinance.Domain.Entities.Identity;
using System.Security.Cryptography;
using System.Text;
using FamilyFinance.Domain;
using Microsoft.EntityFrameworkCore;
using FamilyFinance.Domain.Repositories.IRepositories.Identity;

namespace FamilyFinanceTests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _sut = GetInMemoryEntityRepository();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static HMACSHA512 _hmac = new HMACSHA512();
        CancellationToken cancellationToken = cts.Token;

        private static IUserRepository GetInMemoryEntityRepository()
        {
            DbContextOptions<FFDbContext> options;
            var builder = new DbContextOptionsBuilder<FFDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            FFDbContext? categoryDataContext = new FFDbContext(options);
            categoryDataContext.Database.EnsureDeleted();
            categoryDataContext.Database.EnsureCreated();
            return new UserRepository(categoryDataContext);

        }

        private readonly User? _testEntity1 = new User()
        {
            Id = 60,
            FirstName = "Peter",
            LastName = "Parker",
            UserName = "peter_parker",
            PasswordSalt = _hmac.Key,
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes("spiderman"))
        };

        private readonly User? _testEntity2 = new User()
        {
            Id = 61,
            FirstName = "Wanda",
            LastName = "Maximoff",
            UserName = "wanda_maximoff",
            PasswordSalt = _hmac.Key,
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes("ilovevision"))
        };

        private readonly User? _testEntity3 = new User()
        {
            Id = 62,
            FirstName = "Naruto",
            LastName = "Uzumaki",
            UserName = "hokuge777",
            PasswordSalt = _hmac.Key,
            PasswordHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes("kuramamyfoxy"))
        };



        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var writeCategory = _sut.AddAsync(_testEntity1, cancellationToken);

            Assert.NotNull(writeCategory);
            Assert.Equal(writeCategory.Result?.UserName, _testEntity1?.UserName);
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

            writeEntity.UserName = _testEntity2.UserName;


            Console.WriteLine();


            Assert.NotNull(writeEntity);
            Assert.Equal(writeEntity.UserName, _testEntity2.UserName);
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
            Assert.Equal(result.Result, _testEntity3);
        }
    }
}