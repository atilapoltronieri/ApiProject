using ApiProject.Core.Enum;

namespace ApiProject.Core.Entities.Business
{
    public class TaskEditDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string? Details { get; set; }
        public StatusEnum Status { get; set; }
    }
}
