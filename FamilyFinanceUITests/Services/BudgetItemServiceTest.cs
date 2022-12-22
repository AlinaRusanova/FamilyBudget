using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Service;
using FamilyFinanceUI;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace FamilyFinanceUITests.Services
{
    public class BudgetItemServiceTest
    {
        private readonly BudgetItemService service;

        private readonly Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
        public BudgetItemServiceTest()
        {
            this.service = new BudgetItemService(_httpHandlerMock.Object);
        }

        private readonly BudgetItemModel? _testEntity1 = new BudgetItemModel()
        {
            Id = 1,
            BudgetType = BudgetType.Income,
            Item = "Test1"
        };
        private readonly BudgetItemModel _testEntity2 = new BudgetItemModel()
        {
            Id = 2,
            BudgetType = BudgetType.Income,
            Item = "Test2"
        };
        private readonly BudgetItemModel? _testEntity3 = new BudgetItemModel()
        {
            Id = 3,
            BudgetType = BudgetType.Income,
            Item = "Test3"
        };


        [Fact]
        public async Task BIServer_ListOfBIs_ReturnOK()
        {
            //Arrange

            var listBI = new List<BudgetItemModel>() {_testEntity1,_testEntity2,_testEntity3 };

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("api/BudgetItems/GetAllBI")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(listBI))
            });


            //Act

            await service.GetListOfEntities();
            var list = service.ListOfEntities;

            //Assert
            Assert.NotEqual(0, list.Count);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task BIServer_ListOfBIs_ReturnError()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("api/BudgetItems/GetAllBI")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            });


            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.GetListOfEntities());
        }

        [Fact]
        public async Task BIServer_GetEntityById_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync($"/api/BudgetItems/{_testEntity1.Id}")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

            var entity = await service.GetEntity((int)_testEntity1.Id);

            //Assert
            Assert.Equal(_testEntity1.Id, entity.Id);
            Assert.Equal(_testEntity1.Item, entity.Item);
        }

        [Fact]
        public async Task BIServer_GetEntityById_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetEntity(-1));
        }

        [Fact]
        public async Task BIServer_AddEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<BudgetItemModel>("/api/BudgetItems/AddBI", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/BudgetItems/GetAllBI")).ReturnsAsync(new HttpResponseMessage
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
            Assert.Equal(listBI.First().Item, _testEntity1.Item);
        }

        [Fact]
        public async Task BIServer_AddEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.AddEntity(null));
        }

        [Fact]
        public async Task BIServer_UpdateEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PutAsJsonAsync<BudgetItemModel>("/api/BudgetItems/UpdateBI/", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/BudgetItems/GetAllBI")).ReturnsAsync(new HttpResponseMessage
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
            Assert.Equal(listBI.First().Item, _testEntity1.Item);
        }

        [Fact]
        public async Task BIServer_UpdateEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateEntity(null));
        }

        [Fact]
        public async Task BIServer_DeleteEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<BudgetItemModel>("/api/BudgetItems/DeleteBI", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/BudgetItems/GetAllBI")).ReturnsAsync(new HttpResponseMessage
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
            Assert.Equal(listBI.First().Item, _testEntity1.Item);
        }

        [Fact]
        public async Task BIServer_DeleteEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeleteEntity(null));
        }

    }
}
