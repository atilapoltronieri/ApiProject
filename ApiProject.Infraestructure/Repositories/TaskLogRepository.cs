using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Enum;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TaskLogRepository(ApplicationDbContext dbContext) : BaseRepository<TasksLog>(dbContext), ITasksLogRepository
    {
        public async Task<int> TasksDonePast30Days()
        {
            return await _dbContext.TasksLog.Where(x => x.StatusNew == StatusEnum.Done && x.AlterationDate <= DateTime.Now.AddDays(-30)).CountAsync();
        }
    }
}
