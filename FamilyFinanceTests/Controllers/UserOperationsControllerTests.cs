using FakeItEasy;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;
using FamilyFinance.WebApi.Controllers;
using FluentAssertions;
using Xunit;

namespace UNI.Tests.Controllers
{
    public class UserOperationControllerTests
    {
        private readonly IUserOperationService<UserOperationModel> _service;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public UserOperationControllerTests()
        {
            _service = A.Fake<IUserOperationService<UserOperationModel>>();
        }

        [Fact]
        public void UOController_GetListOfUOs_ReturnOK()
        {
            //Arrange

            var controller = new UserOperationsController(_service);

            //Act
            var result = controller.GetAllUO(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void UOController_CreateUO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            var controller = new UserOperationsController(_service);

            //Act
            var result = controller.AddUO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UOController_CreateUO_ReturnException()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            model.Id = 0;
            var controller = new UserOperationsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddUO(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddUO(null, cancellationToken));
        }


        [Fact]
        public void UOController_UpdateUO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            var controller = new UserOperationsController(_service);

            //Act
            var result = controller.UpdateUO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void UOController_UpdateUO_ReturnException()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            model.Id = 0;
            var controller = new UserOperationsController(_service);

            //Act
            var result = controller.UpdateUO(model, cancellationToken);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.UpdateUO(model, cancellationToken));
        }

        [Fact]
        public void UOController_DeleteUO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            var controller = new UserOperationsController(_service);

            //Act
            var result = controller.DeleteUO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void UOController_DeleteUO_ReturnException()
        {
            //Arrange

            var model = A.Fake<UserOperationModel>();
            model.Id = 0;
            var controller = new UserOperationsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.DeleteUO(model, cancellationToken));
        }

    }
}
