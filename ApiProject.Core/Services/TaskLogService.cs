using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;

namespace ApiProject.Core.Services
{
    public class TaskLogService(ITasksLogRepository tasksLogRepository): ITaskLogService
    {
        private readonly ITasksLogRepository _tasksLogRepository = tasksLogRepository;

        public async Task Create(Tasks oldTask, Tasks newTask, int userId)
        {
            var taskLog = new TasksLog(oldTask.Id, userId);

            if (oldTask.Status != newTask.Status)
            {
                taskLog.StatusOld = oldTask.Status;
                taskLog.StatusNew = newTask.Status;
            }

            if (oldTask.Details != newTask.Details)
            {
                taskLog.DetailsOld = oldTask.Details;
                taskLog.DetailsNew = newTask.Details;
            }

            await _tasksLogRepository.Create(taskLog);
        }

        public async Task<int> TasksDonePast30Days()
        {
            return await _tasksLogRepository.TasksDonePast30Days();
        }
    }
}
