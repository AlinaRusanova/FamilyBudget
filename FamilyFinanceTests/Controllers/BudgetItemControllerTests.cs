using FakeItEasy;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;
using FamilyFinance.WebApi.Controllers;
using FluentAssertions;
using Xunit;

namespace UNI.Tests.Controllers
{
    public class BudgetItemControllerTests
    {
        private readonly IBudgetItemService<BudgetItemModel> _service;

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public BudgetItemControllerTests()
        {
            _service = A.Fake<IBudgetItemService<BudgetItemModel>>();
        }

        [Fact]
        public void BIController_GetListOfBIs_ReturnOK()
        {
            //Arrange

            var controller = new BudgetItemsController(_service);

            //Act
            var result = controller.GetAllBI(cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void BIController_CreateBI_ReturnOK()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            var controller = new BudgetItemsController(_service);

            //Act
            var result = controller.AddBI(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void BIController_CreateBI_ReturnException()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            model.Id = 0;
            var controller = new BudgetItemsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddBI(model, cancellationToken));
            Assert.ThrowsAsync<NotFoundException>(() => controller.AddBI(null, cancellationToken));
        }


        [Fact]
        public void BIController_UpdateBI_ReturnOK()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            var controller = new BudgetItemsController(_service);

            //Act
            var result = controller.UpdateBI(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }


        [Fact]
        public void BIController_UpdateBI_ReturnException()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            model.Id = 0;
            var controller = new BudgetItemsController(_service);

            //Act
            var result = controller.UpdateBI(model, cancellationToken);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.UpdateBI(model, cancellationToken));
        }

        [Fact]
        public void BIController_DeleteBI_ReturnOK()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            var controller = new BudgetItemsController(_service);

            //Act
            var result = controller.DeleteBI(model, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void BIController_DeleteBI_ReturnException()
        {
            //Arrange

            var model = A.Fake<BudgetItemModel>();
            model.Id = 0;
            var controller = new BudgetItemsController(_service);

            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => controller.DeleteBI(model, cancellationToken));
        }

    }
}
