using ApiProject.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Core.Entities.Persistence
{
    [Table("Tasks")]
    public class Tasks : BaseClass
    {
        [Required, ForeignKey(nameof(Projects))]
        public int ProjectId { get; set; }
        [Required, StringLength(255, MinimumLength = 5, ErrorMessage = "Task details should be between 5 to 255 characters.")]
        public string? Details { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
        [Required]
        public PriorityEnum Priority { get; set; }
    }
}
