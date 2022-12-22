using FakeItEasy;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Service;
using FamilyFinanceUI;
using Microsoft.AspNetCore.Components;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace FamilyFinanceUITests.Services
{
    public class UserOperationServiceTest
    {
        private readonly UserOperationService service;

        private readonly Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
        public UserOperationServiceTest()
        {
            this.service = new UserOperationService(_httpHandlerMock.Object);
        }

        private readonly UserOperationModel? _testEntity1 = new UserOperationModel()
        {
            Id = 60,
            Date = DateTime.Parse("10.11.2022"),
            BudgetItemId = 8,
            SumBudgetItem = 500,
            UserId = 1
        };
        private readonly UserOperationModel? _testEntity2 = new UserOperationModel()
        {
            Id = 61,
            Date = DateTime.Parse("11.11.2022"),
            BudgetItemId = 1,
            SumBudgetItem = 50000,
            UserId = 1
        };
        private readonly UserOperationModel? _testEntity3 = new UserOperationModel()
        {
            Id = 62,
            Date = DateTime.Parse("12.11.2022"),
            BudgetItemId = 6,
            SumBudgetItem = 1293,
            UserId = 1
        };


        [Fact]
        public async Task UOServer_ListOfUOs_ReturnOK()
        {
            //Arrange

            var listUO = new List<UserOperationModel>() {_testEntity1,_testEntity2,_testEntity3 };

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("/api/UserOperations/GetAllUO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(listUO))
            });


            //Act

            await service.GetListOfEntities();
            var list = service.ListOfEntities;

            //Assert
            Assert.NotEqual(0, list.Count);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task UOServer_ListOfUOs_ReturnError()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("/api/UserOperations/GetAllUO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            });


            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.GetListOfEntities());
        }

        [Fact]
        public async Task UOServer_GetEntityById_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync($"/api/UserOperations/{_testEntity1.Id}")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

            var entity = await service.GetEntity((int)_testEntity1.Id);

            //Assert
            Assert.Equal(_testEntity1.Id, entity.Id);
            Assert.Equal(_testEntity1.Date, entity.Date);
        }

        [Fact]
        public async Task UOServer_GetEntityById_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetEntity(-1));
        }

        [Fact]
        public async Task UOServer_AddEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<UserOperationModel>("/api/UserOperations/AddUO", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/UserOperations/GetAllUO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("["+JsonConvert.SerializeObject(_testEntity1)+"]")
            });


            //Act

            await service.AddEntity(_testEntity1);
            var listBI = service.ListOfEntities;

            //Assert
            Assert.Equal(1, listBI.Count);
            Assert.Equal(listBI.First().Id, _testEntity1.Id);
            Assert.Equal(listBI.First().FinOperation, _testEntity1.FinOperation);
        }

        [Fact]
        public async Task UOServer_AddEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.AddEntity(null));
        }

        [Fact]
        public async Task UOServer_UpdateEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PutAsJsonAsync<UserOperationModel>("/api/UserOperations/Update/", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/UserOperations/GetAllUO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[" + JsonConvert.SerializeObject(_testEntity1) + "]")
            });


            //Act

            await service.UpdateEntity(_testEntity1);
            var listBI = service.ListOfEntities;

            //Assert
            Assert.Equal(1, listBI.Count);
            Assert.Equal(listBI.First().Id, _testEntity1.Id);
            Assert.Equal(listBI.First().FinOperation, _testEntity1.FinOperation);
        }

        [Fact]
        public async Task UOServer_UpdateEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateEntity(null));
        }

        [Fact]
        public async Task UOServer_DeleteEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<UserOperationModel>("/api/UserOperations/Delete", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/UserOperations/GetAllUO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[" + JsonConvert.SerializeObject(_testEntity1) + "]")
            });


            //Act

            await service.DeleteEntity(_testEntity1);
            var listBI = service.ListOfEntities;

            //Assert
            Assert.Equal(1, listBI.Count);
            Assert.Equal(listBI.First().Id, _testEntity1.Id);
            Assert.Equal(listBI.First().FinOperation, _testEntity1.FinOperation);
        }

        [Fact]
        public async Task UOServer_DeleteEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeleteEntity(null));
        }

    }
}
