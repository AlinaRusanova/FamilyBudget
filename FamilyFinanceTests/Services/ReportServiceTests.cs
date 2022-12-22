using AutoMapper;
using FakeItEasy;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Persistence.Services.Report;
using FluentAssertions;
using Xunit;

namespace FamilyFinanceTests.Services
{
    public class ReportServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationRepository _repository;
        

        private static CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken cancellationToken = cts.Token;

        public ReportServiceTests()
        {
            _repository = A.Fake<IUserOperationRepository>();
            _mapper = A.Fake<IMapper>();
        }


        [Fact]
        public void ReportServer_GetDailyReport_ReturnOK()
        {
            //Arrange
            
            var date = DateTime.Parse("10.11.2022");
            var service = new ReportService(_repository,_mapper);

            //Act
            var result = service.GetDailyReportAsync(date,1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }



        [Fact]
        public void ReportServer_GetPeriodReport_ReturnOK()
        {
            //Arrange

            DateTime dateFrom = DateTime.Parse("01.01.2022");
            DateTime dateTo = DateTime.Parse("15.11.2022");

            var service = new ReportService(_repository, _mapper);

            //Act
            var result = service.GetPeriodReportAsync(dateFrom,dateTo,1, cancellationToken);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
