using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Enum;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;

namespace ApiProject.Core.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task<RoleEnum> GetRoleById(int userId)
        {
            var user = await GetUserById(userId);

            return user.Role;
        }
    }
}
