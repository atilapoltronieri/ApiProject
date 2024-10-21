using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IServices
{
    public interface ITaskLogService
    {
        Task Create(Tasks oldTask, Tasks newTask, int userId);
        Task<int> TasksDonePast30Days();
    }
}
