using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<IEnumerable<Projects>> Get();
        Task<Projects> Create(ProjectDto project);
        Task Delete(int id);
    }
}
