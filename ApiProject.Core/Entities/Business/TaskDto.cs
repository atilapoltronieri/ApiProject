using ApiProject.Core.Enum;

namespace ApiProject.Core.Entities.Business
{
    public class TaskDto
    {
        public int ProjectId { get; set; }
        public string? Details { get; set; }
        public StatusEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }
    }
}
