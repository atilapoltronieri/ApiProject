using System.ComponentModel.DataAnnotations;

namespace ApiProject.Core.Entities.Business
{
    public class TasksCommentDto
    {
        public int TaskId { get; set; }
        [Required, StringLength(255, MinimumLength = 5, ErrorMessage = "Comments should be between 5 to 255 characters.")]
        public string? Comment { get; set; }
    }
}
