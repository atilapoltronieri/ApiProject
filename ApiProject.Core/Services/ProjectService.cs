using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Interfaces.IMapper;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;

namespace ApiProject.Core.Services
{
    public class ProjectService(IProjectRepository projectRepository,
                                IBaseMapper<ProjectDto, Projects> projectMapper,
                                ITaskService taskService) : IProjectService
    {
        private readonly IBaseMapper<ProjectDto, Projects> _projectMapper = projectMapper;
        private readonly IProjectRepository _productRepository = projectRepository;
        private readonly ITaskService _taskService = taskService;

        public static readonly string HasTasksErrorMessage = "Project has tasks. Please delete them before deleting the project.";

        public async Task<Projects> Create(ProjectDto project)
        {
            var projectMapped = _projectMapper.MapModel(project);
         
            return await projectRepository.Create(projectMapped);
        }

        public async Task<IEnumerable<Projects>> Get()
        {
            var listProject = await _productRepository.GetAll();

            return listProject;
        }

        public async Task Delete(int id)
        {
            var hasTasks = await _taskService.HasTasks(id);
            if (hasTasks)
                throw new BusinessException(HasTasksErrorMessage);

            var entity = await _productRepository.GetById(id);
            await _productRepository.Delete(entity);
        }
    }
}
