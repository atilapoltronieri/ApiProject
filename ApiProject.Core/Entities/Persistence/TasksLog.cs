using ApiProject.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Core.Entities.Persistence
{
    [Table("TasksLog")]
    public class TasksLog(int taskId, int userId) : BaseClass
    {
        [Required, ForeignKey(nameof(Tasks))]
        public int TaskId { get; set; } = taskId;
        [Required, ForeignKey(nameof(User))]
        public int UserId { get; set; } = userId;
        public string? DetailsOld { get; set; }
        public string? DetailsNew { get; set; }
        public string? CommentsOld { get; set; }
        public string? CommentsNew { get; set; }
        public StatusEnum? StatusOld { get; set; }
        public StatusEnum? StatusNew { get; set; }
        public DateTime AlterationDate { get; set; } = DateTime.Now;
    }
}
