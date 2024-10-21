using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IMapper;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;

namespace ApiProject.Core.Services
{
    public class TaskCommentService(ITaskCommentRepository taskCommentRepository,
                                    IBaseMapper<TasksCommentDto, TasksComment> taskEditMapper) : ITaskCommentService
    {
        readonly ITaskCommentRepository _taskCommentRepository = taskCommentRepository;
        readonly IBaseMapper<TasksCommentDto, TasksComment> mapper = taskEditMapper;

        public async Task<TasksComment> Create(TasksCommentDto model)
        {
            var tasksComment = mapper.MapModel(model);

            return await _taskCommentRepository.Create(tasksComment);
        }
    }
}
