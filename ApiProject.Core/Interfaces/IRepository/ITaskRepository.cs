using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IRepository
{
    public interface ITaskRepository : IBaseRepository<Tasks>
    {
        Task<IEnumerable<Tasks>> GetProjectTasks(int id);
    }
}
