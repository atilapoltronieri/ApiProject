using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IServices
{
    public interface ITaskService
    {
        Task<IEnumerable<Tasks>> Get(int projectId);
        Task<Tasks> Create(TaskDto model);
        Task Update(TaskEditDto model);
        Task Delete(int id);
        Task<bool> HasTasks(int projectId);
    }
}
