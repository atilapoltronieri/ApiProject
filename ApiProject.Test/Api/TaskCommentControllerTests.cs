using ApiProject.Core.Entities.Persistence;
using ApiProject.Test.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Test.Api
{
    public class TaskCommentControllerTests(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture = fixture;

        [Fact]
        public async Task Create_ReturnsTaskComment()
        {
            // Act
            var result = await _fixture.taskCommentController.Create(_fixture.taskCommentsDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.NotNull(okObjectResult);

            var model = (TasksComment)okObjectResult.Value;
            Assert.NotNull(model);
            Assert.Equal(model, _fixture.tasksComment);
        }
    }
}
