using System.ComponentModel.DataAnnotations;

namespace ApiProject.Core.Entities.Business
{
    public class ProjectDto
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
