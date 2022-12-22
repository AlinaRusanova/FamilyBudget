using FakeItEasy;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.Persistence.Services.IServices.Identity;
using FamilyFinance.WebApi.Controllers;
using FluentAssertions;
using Xunit;

namespace UNI.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly IUserService<UserModel> _service;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public UserControllerTests()
        {
            _service = A.Fake<IUserService<UserModel>>();
        }

        [Fact]
        public void UserController_GetListOfUsers_ReturnOK()
        {
            //Arrange

            var controller = new UserController(_service);

            //Act
            var result = controller.GetAllUsers(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserController_GetById_ReturnOK()
        {
            //Arrange
            
            var controller = new UserController(_service);

            //Act
            var result = controller.FindUserById(1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserController_GetById_ReturnException()
        {
            //Arrange

            var controller = new UserController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.FindUserById(0, cancellationToken));
        }

        [Fact]
        public void UserServer_Authenticate_ReturnOK()
        {
            //Arrange

            var controller = new UserController(_service);
            var model = A.Fake<UserModel>();

            //Act
            var result = controller.Authenticate(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_Authenticate_ReturnException()
        {
            //Arrange

            var controller = new UserController(_service);
            var model = A.Fake<UserModel>();
            model.Id = 0;

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.Authenticate(model, cancellationToken));
        }


        [Fact]
        public void UserServer_Register_ReturnOK()
        {
            //Arrange

            var controller = new UserController(_service);
            var model = A.Fake<UserModel>();

            //Act
            var result = controller.Register(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UserServer_Register_ReturnException()
        {  
            //Arrange

            var controller = new UserController(_service);
            var model = A.Fake<UserModel>();
            model.Id = 0;

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.Register(model, cancellationToken));
        }

    }
}
