using AutoMapper;
using FakeItEasy;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.Budget;
using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit;

namespace FamilyFinanceTests.Services
{
    public class BudgetItemServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IBudgetItemRepository _repository;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public BudgetItemServiceTests()
        {
            _repository = A.Fake<IBudgetItemRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void BIServer_ListOfBIs_ReturnOK()
        {
            //Arrange
            var entities = A.Fake<ICollection<BudgetItemModel>>();
            var entitiesList = A.Fake<List<BudgetItemModel>>();
            A.CallTo(() => _mapper.Map<List<BudgetItemModel>>(entities)).Returns(entitiesList);
            var service = new BudgetItemService(_repository, _mapper);

            //Act
            var result = service.ListAllAsync(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void BIServer_ListOfBIs_ReturnException()
        {
            //Arrange
            var entities = A.Fake<ICollection<BudgetItemModel>>();
            var entitiesList = A.Fake<List<BudgetItemModel>>();
            A.CallTo(() => _mapper.Map<List<BudgetItemModel>>(entities)).Returns(entitiesList);
            var service = new BudgetItemService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.ListAllAsync(cancellationToken));

        }


        [Fact]
        public void BIServer_CreateBI_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act
            var result = service.AddAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void BIServer_CreateBI_ReturnException()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();
            model.Id = -1;

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(null, cancellationToken));
        }

        [Fact]
        public void BIServer_GetById_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act
            var result = service.GetByIdAsync(1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void BIServer_GetById_ReturnException()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync((int)model.Id, cancellationToken));
        }

        [Fact]
        public void BIServer_Update_ReturnException()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();
            model.Id = 0;

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(null, cancellationToken));
        }

        [Fact]
        public void BIServer_Update_ReturnOk()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act
            var result = service.UpdateAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }



        [Fact]
        public void BIServer_Delete_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            var model = A.Fake<BudgetItemModel>();

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act
            var result = service.DeleteAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void BIServer_Delete_ReturnException()
        {
            //Arrange

            var entities = A.Fake<BudgetItem>();
            entities.Id = 0;
            var model = A.Fake<BudgetItemModel>();

            A.CallTo(() => _mapper.Map<BudgetItem>(model)).Returns(entities);
            var service = new BudgetItemService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(null, cancellationToken));
        }

    }
}
