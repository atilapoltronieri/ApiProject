using ApiProject.Core.Enum;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Interfaces.IServices;
using System.Text;

namespace ApiProject.Core.Services
{
    public class ReportsService(ITaskLogService taskLogService,
                                IUserService userService) : IReportsService
    {
        private readonly ITaskLogService _taskLogService = taskLogService;
        private readonly IUserService _userService = userService;

        public static readonly string UserNotManagerErrorMessage = "Only managers can acess this report.";

        public async Task<string> GetPerformanceReport(int userId)
        {
            var userRole = await _userService.GetRoleById(userId);
            if (userRole != RoleEnum.Manager)
                throw new BusinessException(UserNotManagerErrorMessage);

            var tasksDone = await _taskLogService.TasksDonePast30Days();

            return BuildReport(tasksDone);
        }

        private static string BuildReport(int tasksDone)
        {
            var report = new StringBuilder();
            
            report.AppendLine("Tasks Done Past 30 Days");
            report.AppendLine("--------------------------------------------------------------------------");
            report.AppendLine($"{tasksDone} tasks was done in past 30 days.");
            report.AppendLine($"Average of {Math.Abs(tasksDone / 30)} tasks was done in past 30 days.");
            report.AppendLine("--------------------------------------------------------------------------");
            
            return report.ToString();
        }
    }
}
