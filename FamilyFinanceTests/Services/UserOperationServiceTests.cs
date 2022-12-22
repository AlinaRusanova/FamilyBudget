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
    public class UserOperationServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationRepository _upRepository;
        private readonly IBudgetItemRepository _biRepository;
        private readonly IFinancialOperationRepository _foRepository;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public UserOperationServiceTests()
        {
            _upRepository = A.Fake<IUserOperationRepository>();
            _biRepository = A.Fake<IBudgetItemRepository>();
            _mapper = A.Fake<IMapper>();
            _foRepository = A.Fake<IFinancialOperationRepository>();
        }

        [Fact]
        public void UOServer_ListOfUOs_ReturnOK()
        {
            //Arrange
            var entities = A.Fake<ICollection<UserOperationModel>>();
            var entitiesList = A.Fake<List<UserOperationModel>>();
            A.CallTo(() => _mapper.Map<List<UserOperationModel>>(entities)).Returns(entitiesList);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act
            var result = service.ListAllAsync(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UOServer_ListOfUOs_ReturnException()
        {
            //Arrange
            var entities = A.Fake<ICollection<UserOperationModel>>();
            var entitiesList = A.Fake<List<UserOperationModel>>();
            A.CallTo(() => _mapper.Map<List<UserOperationModel>>(entities)).Returns(entitiesList);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.ListAllAsync(cancellationToken));

        }


        [Fact]
        public void UOServer_CreateUO_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act
            var result = service.AddAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UOServer_CreateUO_ReturnException()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();
            entities.Id = -1;

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.AddAsync(null, cancellationToken));
        }

        [Fact]
        public void UOServer_GetById_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act
            var result = service.GetByIdAsync(entities.Id, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UOServer_GetById_ReturnException()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(entities.Id, cancellationToken));
        }

        [Fact]
        public void UOServer_Update_ReturnException()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();
            model.Id = 0;

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.UpdateAsync(null, cancellationToken));
        }

        [Fact]
        public void UOServer_Update_ReturnOk()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act
            var result = service.UpdateAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }



        [Fact]
        public void UOServer_Delete_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            var model = A.Fake<UserOperationModel>();

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act
            var result = service.DeleteAsync(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void UOServer_Delete_ReturnException()
        {
            //Arrange

            var entities = A.Fake<UserOperation>();
            entities.Id = 0;
            var model = A.Fake<UserOperationModel>();

            A.CallTo(() => _mapper.Map<UserOperation>(model)).Returns(entities);
            var service = new UserOperationService(_upRepository, _biRepository, _mapper, _foRepository);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(null, cancellationToken));
        }

    }
}
