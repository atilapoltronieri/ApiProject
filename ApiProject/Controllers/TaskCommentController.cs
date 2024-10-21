using ApiProject.Core.Entities.Business;
using ApiProject.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskCommentController(ITaskCommentService taskCommentService) : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService = taskCommentService;


        // POST api/taskcomment
        [HttpPost]
        public async Task<IActionResult> Create(TasksCommentDto model)
        {
            var data = await _taskCommentService.Create(model);

            return Ok(data);
        }
    }
}
