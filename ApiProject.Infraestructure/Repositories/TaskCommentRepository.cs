using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Infraestructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TaskCommentRepository(ApplicationDbContext dbContext) : BaseRepository<TasksComment>(dbContext), ITaskCommentRepository
    {
    }
}
