using ApiProject.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController(IReportsService reportsService) : ControllerBase
    {
        readonly IReportsService _reportsService = reportsService;
        
        // GET: api/reports
        [HttpGet]
        public async Task<IActionResult> PerformanceReport(int userId)
        {
            var report = await _reportsService.GetPerformanceReport(userId);
            return Ok(report);
        }
    }
}
