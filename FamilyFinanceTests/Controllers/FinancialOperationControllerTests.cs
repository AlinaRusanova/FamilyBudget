using FakeItEasy;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;
using FamilyFinance.WebApi.Controllers;
using FluentAssertions;
using Xunit;

namespace UNI.Tests.Controllers
{
    public class FinancialOperationControllerTests
    {
        private readonly IFinancialOperationService<FinancialOperationModel> _service;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public FinancialOperationControllerTests()
        {
            _service = A.Fake<IFinancialOperationService<FinancialOperationModel>>();
        }

        [Fact]
        public void FOController_GetListOfFOs_ReturnOK()
        {
            //Arrange

            var controller = new FinancialOperationsController(_service);

            //Act
            var result = controller.GetAllFO(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void FOController_CreateFO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            var controller = new FinancialOperationsController(_service);

            //Act
            var result = controller.AddFO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FOController_CreateFO_ReturnException()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            model.Id = 0;
            var controller = new FinancialOperationsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddFO(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddFO(null, cancellationToken));
        }


        [Fact]
        public void FOController_UpdateFO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            var controller = new FinancialOperationsController(_service);

            //Act
            var result = controller.UpdateFO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void FOController_UpdateFO_ReturnException()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            model.Id = 0;
            var controller = new FinancialOperationsController(_service);

            //Act
            var result = controller.UpdateFO(model, cancellationToken);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.UpdateFO(model, cancellationToken));
        }

        [Fact]
        public void FOController_DeleteFO_ReturnOK()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            var controller = new FinancialOperationsController(_service);

            //Act
            var result = controller.DeleteFO(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FOController_DeleteFO_ReturnException()
        {
            //Arrange

            var model = A.Fake<FinancialOperationModel>();
            model.Id = 0;
            var controller = new FinancialOperationsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.DeleteFO(model, cancellationToken));
        }

    }
}
