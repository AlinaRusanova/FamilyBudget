using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.UI.Service;
using FamilyFinanceUI;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace FamilyFinanceUITests.Services
{
    public class UserServiceTest
    {
        private readonly UserService service;

        private readonly Mock<IHttpHandler> _httpHandlerMock = new Mock<IHttpHandler>();
        public UserServiceTest()
        {
            this.service = new UserService(_httpHandlerMock.Object);
        }

        private readonly UserModel? _testEntity1 = new UserModel()
        {
            Id = 1,
            FirstName = "Peter",
            LastName = "Parker",
            UserName = "peter_parker",
            Password = "spiderman"
        };



        [Fact]
        public async Task UserServer_Register_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<UserModel>("/api/User/register",_testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

          var entity =  await service.Register(_testEntity1);

            //Assert
            Assert.Equal(entity.FirstName, _testEntity1.FirstName);
            Assert.Equal(entity.UserName, _testEntity1.UserName);
        }

        [Fact]
        public async Task UserServer_RegisterReportAsync_ReturnError()
        {
            Assert.ThrowsAsync<NotFoundException>(() => service.Register(null));
        }

        [Fact]
        public async Task UserServer_Login_ReturnOK()
        {
            //Arrange

            var response = this._httpHandlerMock.Setup(x => x.PostAsJsonAsync<UserModel>("/api/User/authenticate",_testEntity1)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(_testEntity1))
            });


            //Act

            var entity = await service.Login(_testEntity1);

            //Assert
            Assert.Equal(entity.FirstName, _testEntity1.FirstName);
            Assert.Equal(entity.UserName, _testEntity1.UserName);
        }

        [Fact]
        public async Task UserServer_Login_ReturnError()
        {
            Assert.ThrowsAsync<NotFoundException>(() => service.Login(null));
        }



    }
}
