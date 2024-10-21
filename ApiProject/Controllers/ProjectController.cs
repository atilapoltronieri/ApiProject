using ApiProject.Core.Entities.Business;
using ApiProject.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        // GET: api/project
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _projectService.Get();
            return Ok(products);
        }

        // POST api/project
        [HttpPost]
        public async Task<IActionResult> Create(ProjectDto model)
        {
            var data = await _projectService.Create(model);
            return Ok(data);
        }

        // DELETE api/project/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.Delete(id);
            return Ok();
        }
    }
}
