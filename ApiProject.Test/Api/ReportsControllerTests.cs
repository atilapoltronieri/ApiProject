using ApiProject.Core.Exceptions;
using ApiProject.Core.Services;
using ApiProject.Test.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Test.Api
{
    public class ReportsControllerTests(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture = fixture;

        [Fact]
        public async Task PerformanceReport_ReturnsReportUserManager()
        {
            // Act
            var result = await _fixture.reportsController.PerformanceReport(_fixture.userAdm.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (string)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.StartsWith("Tasks Done Past 30 Days", model);
        }

        [Fact]
        public async Task PerformanceReport_ThrowsUserNotManager()
        {
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => await _fixture.reportsController.PerformanceReport(_fixture.user.Id));

            // Assert
            Assert.IsType<BusinessException>(result);
            Assert.Equal(result.Message, ReportsService.UserNotManagerErrorMessage);
        }
    }
}
