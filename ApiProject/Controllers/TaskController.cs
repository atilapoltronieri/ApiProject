using ApiProject.Core.Entities.Business;
using ApiProject.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        // GET: api/task
        [HttpGet]
        public async Task<IActionResult> Get(int projectId)
        {
            var tasks = await _taskService.Get(projectId);
            return Ok(tasks);
        }

        // POST api/task
        [HttpPost]
        public async Task<IActionResult> Create(TaskDto model)
        {
            var data = await _taskService.Create(model);
            return Ok(data);
        }

        // PUT api/task/
        [HttpPut]
        public async Task<IActionResult> Edit(TaskEditDto model)
        {
            await _taskService.Update(model);
            return Ok();
        }


        // DELETE api/task/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }
}
