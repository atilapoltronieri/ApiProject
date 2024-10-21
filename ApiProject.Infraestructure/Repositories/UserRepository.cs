using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Infraestructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
    {
    }
}
