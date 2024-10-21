using System.ComponentModel.DataAnnotations;

namespace ApiProject.Core.Entities.Persistence
{
    public abstract class BaseClass
    {
        [Key]
        public int Id { get; set; }
    }
}
