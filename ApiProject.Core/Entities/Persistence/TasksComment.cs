using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Core.Entities.Persistence
{
    public class TasksComment : BaseClass
    {

        [Required, ForeignKey(nameof(Tasks))]
        public int TaskId { get; set; }
        [Required, StringLength(255, MinimumLength = 5, ErrorMessage = "Comments should be between 5 to 255 characters.")]
        public string? Comment { get; set; }

        public TasksComment(int taskId, string? comment)
        {
            TaskId=taskId;
            Comment=comment;
        }
    }
}
