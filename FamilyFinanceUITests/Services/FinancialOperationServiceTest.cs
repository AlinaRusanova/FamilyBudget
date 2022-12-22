using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Entities.Budget;
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
    public class FinancialOperationServiceTest
    {
        private readonly FinancialOperationService service;

        private readonly Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
        public FinancialOperationServiceTest()
        {
            this.service = new FinancialOperationService(_httpHandlerMock.Object);
        }

        private readonly FinancialOperationModel? _testEntity1 = new FinancialOperationModel()
        {
            Id = 1,
            FinOperation = "Test1"
        };
        private readonly FinancialOperationModel? _testEntity2 = new FinancialOperationModel()
        {
            Id = 2,
            FinOperation = "Test2"
        };
        private readonly FinancialOperationModel? _testEntity3 = new FinancialOperationModel()
        {
            Id = 3,
            FinOperation = "Test3"
        };


        [Fact]
        public async Task FOServer_ListOfFOs_ReturnOK()
        {
            //Arrange

            var listFO = new List<FinancialOperationModel>() {_testEntity1,_testEntity2,_testEntity3 };

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("/api/FinancialOperations/GetAllFO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(listFO))
            });


            //Act

            await service.GetListOfEntities();
            var list = service.ListOfEntities;

            //Assert
            Assert.NotEqual(0, list.Count);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public async Task FOServer_ListOfFOs_ReturnError()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync("api/FinancialOperations/GetAllFO")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            });


            //Act

            //Assert

            Assert.ThrowsAsync<NotFoundException>(() => service.GetListOfEntities());
        }

        [Fact]
        public async Task FOServer_GetEntityById_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync($"/api/FinancialOperations/{_testEntity1.Id}")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

            var entity = await service.GetEntity((int)_testEntity1.Id);

            //Assert
            Assert.Equal(_testEntity1.Id, entity.Id);
            Assert.Equal(_testEntity1.FinOperation, entity.FinOperation);
        }

        [Fact]
        public async Task FOServer_GetEntityById_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetEntity(-1));
        }

        [Fact]
        public async Task FOServer_AddEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<FinancialOperationModel>("/api/FinancialOperations/AddFO", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/FinancialOperations/GetAllFO")).ReturnsAsync(new HttpResponseMessage
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
        public async Task FOServer_AddEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.AddEntity(null));
        }

        [Fact]
        public async Task FOServer_UpdateEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PutAsJsonAsync<FinancialOperationModel>("/api/FinancialOperations/Update/", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/FinancialOperations/GetAllFO")).ReturnsAsync(new HttpResponseMessage
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
        public async Task FOServer_UpdateEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateEntity(null));
        }

        [Fact]
        public async Task FOServer_DeleteEntity_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<FinancialOperationModel>("/api/FinancialOperations/Delete", _testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });

            var list = this._httpHandlerMock.Setup(x => x.GetAsync("/api/FinancialOperations/GetAllFO")).ReturnsAsync(new HttpResponseMessage
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
        public async Task FOServer_DeleteEntity_ReturnError()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeleteEntity(null));
        }

    }
}
