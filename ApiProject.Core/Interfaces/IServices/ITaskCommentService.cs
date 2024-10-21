using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IServices
{
    public interface ITaskCommentService
    {
        Task<TasksComment> Create(TasksCommentDto model);
    }
}
