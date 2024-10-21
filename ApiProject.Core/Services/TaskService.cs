using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Interfaces.IMapper;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;

namespace ApiProject.Core.Services
{
    public class TaskService(ITaskRepository taskRepository,
                            IBaseMapper<TaskEditDto, Tasks> taskEditMapper,
                            IBaseMapper<TaskDto, Tasks> taskMapper,
                            ITaskLogService taskLogService,
                            IUserService userService) : ITaskService
    {
        private readonly IBaseMapper<TaskDto, Tasks> _taskMapper = taskMapper;
        private readonly IBaseMapper<TaskEditDto, Tasks> _taskEditMapper = taskEditMapper;

        private readonly ITaskRepository _taskRepository = taskRepository;

        private readonly ITaskLogService _taskLogService = taskLogService;
        private readonly IUserService _userService = userService;

        public static readonly string ProjectHas20TasksErrorMessage = "This project is on it's limit of 20 taks.";

        public async Task<Tasks> Create(TaskDto model)
        {
            var listTasks = await _taskRepository.GetProjectTasks(model.ProjectId);
            if (listTasks.Count() >= 20)
                throw new BusinessException(ProjectHas20TasksErrorMessage);

            var task = _taskMapper.MapModel(model);

            return await _taskRepository.Create(task);
        }

        public async Task Delete(int id)
        {
            var entity = await _taskRepository.GetById(id);

            await _taskRepository.Delete(entity);
        }

        public async Task<IEnumerable<Tasks>> Get(int projectId)
        {
            return await _taskRepository.GetProjectTasks(projectId);
        }

        public async Task Update(TaskEditDto model)
        {
            // It will throw NotFoundException in case user doesn't exist.
            await _userService.GetUserById(model.UserId);

            var entity = await _taskRepository.GetById(model.Id);
            var task = _taskEditMapper.MapModel(model);
            task.Priority = entity.Priority;

            await _taskRepository.Update(task);

            await _taskLogService.Create(entity, task, model.UserId);
        }

        public async Task<bool> HasTasks(int projectId)
        {
            var tasks = await _taskRepository.GetProjectTasks(projectId);

            return tasks.Any();
        }
    }
}
