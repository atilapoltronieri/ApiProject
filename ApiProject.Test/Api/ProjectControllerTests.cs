using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Services;
using ApiProject.Test.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Test.Api
{
    public class ProjectControllerTests(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture = fixture;

        [Fact]
        public async Task Get_ReturnsProjects()
        {
            // Act
            var result = await _fixture.projectController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (IEnumerable<Projects>)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.Equal(model?.Count(), _fixture.projects.Count);
        }

        [Fact]
        public async Task Create_ReturnsProject()
        {
            // Act
            var result = await _fixture.projectController.Create(_fixture.projectDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (Projects)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.Equal(model, _fixture.project);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Act
            var result = await _fixture.projectController.Delete(3);

            // Assert
            Assert.IsType<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task Delete_ThrowsIfHasTasks()
        {
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => await _fixture.projectController.Delete(1));

            // Assert
            Assert.IsType<BusinessException>(result);
            Assert.Equal(result.Message, ProjectService.HasTasksErrorMessage);
        }
    }
}
