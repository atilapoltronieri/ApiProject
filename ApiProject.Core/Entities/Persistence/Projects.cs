using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Core.Entities.Persistence
{
    [Table("Projects")]
    public class Projects : BaseClass
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
