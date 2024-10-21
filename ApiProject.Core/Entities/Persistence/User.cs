using ApiProject.Core.Enum;

namespace ApiProject.Core.Entities.Persistence
{
    public class User(string name, RoleEnum role) : BaseClass
    {
        public string Name { get; set; } = name;
        public RoleEnum Role { get; set; } = role;
    }
}
