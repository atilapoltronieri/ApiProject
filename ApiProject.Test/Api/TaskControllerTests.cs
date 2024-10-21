using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Services;
using ApiProject.Test.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Test.Api
{
    public class TaskControllerTests(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture = fixture;

        [Fact]
        public async Task Get_ReturnsTasks()
        {
            // Act
            var result = await _fixture.taskController.Get(_fixture.project.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (IEnumerable<Tasks>)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.Equal(model.Count(), _fixture.tasks19.Count);
        }

        [Fact]
        public async Task Create_ReturnsReportUserManager()
        {
            // Act
            var result = await _fixture.taskController.Create(_fixture.taskDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (Tasks)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.Equal(model, _fixture.task);
        }

        [Fact]
        public async Task Create_ThowsIfProjectHas20Tasks()
        {
            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(async () => await _fixture.taskController.Create(_fixture.taskDtoProject2));

            // Assert
            Assert.IsType<BusinessException>(result);
            Assert.Equal(result.Message, TaskService.ProjectHas20TasksErrorMessage);
        }

        [Fact]
        public async Task Edit_ReturnsOk()
        {
            // Act
            var result = await _fixture.taskController.Edit(_fixture.taskEditDto);

            // Assert
            Assert.IsType<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.NotNull(okResult);

        }
        
        [Fact]
        public async Task Edit_ReturnsOk2()
        {
            // Act
            var result = await _fixture.taskController.Edit(_fixture.taskEditDto2);

            // Assert
            Assert.IsType<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.NotNull(okResult);

        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Act
            var result = await _fixture.taskController.Delete(_fixture.task.Id);

            // Assert
            Assert.IsType<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.NotNull(okResult);
        }
    }
}
