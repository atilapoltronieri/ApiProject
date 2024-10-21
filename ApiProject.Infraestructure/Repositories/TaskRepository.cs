using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TaskRepository(ApplicationDbContext dbContext) : BaseRepository<Tasks>(dbContext), ITaskRepository
    {
        public async Task<IEnumerable<Tasks>> GetProjectTasks(int id)
        {
            var tasks = await _dbContext.Tasks.Where(x => x.ProjectId == id).ToListAsync();

            return tasks;
        }
    }
}
