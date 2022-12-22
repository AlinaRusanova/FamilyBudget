using AutoMapper;
using FakeItEasy;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.Budget;
using FluentAssertions;
using Xunit;

namespace FamilyFinanceTests.Services
{
    public class FinancialOperationServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IFinancialOperationRepository _repository;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public FinancialOperationServiceTests()
        {
            _repository = A.Fake<IFinancialOperationRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void FOServer_ListOfFOs_ReturnOK()
        {
            //Arrange
            var entities = A.Fake<ICollection<FinancialOperationModel>>();
            var entitiesList = A.Fake<List<FinancialOperationModel>>();
            A.CallTo(() => _mapper.Map<List<FinancialOperationModel>>(entities)).Returns(entitiesList);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act
            var result = service.ListAllAsync(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FOServer_ListOfFOs_ReturnException()
        {
            //Arrange
            var entities = A.Fake<ICollection<FinancialOperationModel>>();
            var entitiesList = A.Fake<List<FinancialOperationModel>>();
            A.CallTo(() => _mapper.Map<List<FinancialOperationModel>>(entities)).Returns(entitiesList);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.ListAllAsync(cancellationToken));

        }


        [Fact]
        public void FOServer_CreateFO_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act
            var result = service.AddAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FOServer_CreateFO_ReturnException()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();
            model.Id = -1;

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(null, cancellationToken));
        }

        [Fact]
        public void FOServer_GetById_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act
            var result = service.GetByIdAsync(1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FOServer_GetById_ReturnException()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync((int)model.Id, cancellationToken));
        }

        [Fact]
        public void FOServer_Update_ReturnException()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();
            model.Id = 0;

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(null, cancellationToken));
        }

        [Fact]
        public void FOServer_Update_ReturnOk()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act
            var result = service.UpdateAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }



        [Fact]
        public void FOServer_Delete_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            var model = A.Fake<FinancialOperationModel>();

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act
            var result = service.DeleteAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void FOServer_Delete_ReturnException()
        {
            //Arrange

            var entities = A.Fake<FinancialOperation>();
            entities.Id = 0;
            var model = A.Fake<FinancialOperationModel>();

            A.CallTo(() => _mapper.Map<FinancialOperation>(model)).Returns(entities);
            var service = new FinancialOperationService(_repository, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(null, cancellationToken));
        }

    }
}
