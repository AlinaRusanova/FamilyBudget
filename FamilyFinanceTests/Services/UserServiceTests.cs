using AutoMapper;
using FakeItEasy;
using FamilyFinance.Domain.Entities.Identity;
using FamilyFinance.Domain.Repositories.IRepositories.Identity;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.Persistence.Services.Identity;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FamilyFinanceTests.Services
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public UserServiceTests()
        {
            _repository = A.Fake<IUserRepository>();
            _configuration = A.Fake<IConfiguration>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void UserServer_ListOfUserss_ReturnOK()
        {
            //Arrange
            var entities = A.Fake<ICollection<UserModel>>();
            var entitiesList = A.Fake<List<UserModel>>();
            A.CallTo(() => _mapper.Map<List<UserModel>>(entities)).Returns(entitiesList);
            var service = new UserService(_repository,_configuration, _mapper);

            //Act
            var result = service.GetAllUsers(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_ListOfUserss_ReturnException()
        {
            //Arrange
            var entities = A.Fake<ICollection<UserModel>>();
            var entitiesList = A.Fake<List<UserModel>>();
            A.CallTo(() => _mapper.Map<List<UserModel>>(entities)).Returns(entitiesList);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.GetAllUsers(cancellationToken));

        }


        [Fact]
        public void UserServer_GetById_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act
            var result = service.GetById(1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_GetById_ReturnException()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.GetById((int)model.Id, cancellationToken));
        }

        [Fact]
        public void UserServer_Authenticate_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act
            var result = service.Authenticate(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_Authenticate_ReturnException()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.Authenticate(model, cancellationToken));
        }


        [Fact]
        public void UserServer_Register_ReturnOK()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act
            var result = service.Register(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_Register_ReturnException()
        {
            //Arrange

            var entities = A.Fake<User>();
            var model = A.Fake<UserModel>();
            entities.Id = 0;

            A.CallTo(() => _mapper.Map<User>(model)).Returns(entities);
            var service = new UserService(_repository, _configuration, _mapper);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => service.Register(model, cancellationToken));
        }

    }
}
