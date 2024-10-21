using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Enum;

namespace ApiProject.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User> GetUserById(int userId);
        Task<RoleEnum> GetRoleById(int userId);
    }
}
