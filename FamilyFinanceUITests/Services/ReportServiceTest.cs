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
    public class ReportServiceTest
    {
        private readonly ReportService service;

        private readonly Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
        public ReportServiceTest()
        {
            this.service = new ReportService(_httpHandlerMock.Object);
        }

        private readonly ReportModel? _testEntity1 = new ReportModel()
        {
            Date = DateTime.Parse("15.12.2022"),
            Incomes = 100,
            Expenses = 10,
            Profit = 90
        };

        private readonly ReportModel? _testEntity2 = new ReportModel()
        {
            DateFrom = DateTime.Parse("15.12.2022"),
            DateTo = DateTime.Parse("16.12.2022"),
            Incomes = 100,
            Expenses = 10,
            Profit = 90
        };


        [Fact]
        public async Task ReportServer_GetDailyReportAsync_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync($"/api/Report/GetDailyReport/{"15.12.2022"}")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

          var entity =  await service.GetDailyReportAsync("15.12.2022");

            //Assert
            Assert.Equal(entity.Incomes, _testEntity1.Incomes);
            Assert.Equal(entity.Expenses, _testEntity1.Expenses);
        }

        [Fact]
        public async Task ReportServer_GetPeriodReportAsync_ReturnError()
        {
            Assert.ThrowsAsync<NotFoundException>(() => service.GetDailyReportAsync(null));
        }

        [Fact]
        public async Task ReportServer_GetPeriodReportAsync_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.GetAsync($"/api/Report/GetPeriodReport/{"15.12.2022"}/{"16.12.2022"}")).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity2))
            });


            //Act

            var entity = await service.GetPeriodReportAsync("15.12.2022","16.12.2022");

            //Assert
            Assert.Equal(entity.Incomes, _testEntity2.Incomes);
            Assert.Equal(entity.Expenses, _testEntity2.Expenses);
        }

        [Fact]
        public async Task ReportServer_GetDailyReportAsync_ReturnError()
        {
            Assert.ThrowsAsync<NotFoundException>(() => service.GetPeriodReportAsync(null,null));
        }



    }
}
