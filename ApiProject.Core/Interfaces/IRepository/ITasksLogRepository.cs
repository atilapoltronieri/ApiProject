using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IRepository
{
    public interface ITasksLogRepository : IBaseRepository<TasksLog>
    {
        Task<int> TasksDonePast30Days();
    }
}
