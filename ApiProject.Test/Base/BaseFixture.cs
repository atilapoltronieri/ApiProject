using ApiProject.Api.Controllers;
using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IMapper;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;
using ApiProject.Core.Services;
using AutoFixture;
using Moq;

namespace ApiProject.Test.Base
{
    public class BaseFixture : Fixture
    {
        private Mock<IProjectRepository> _projectRepository;
        private Mock<ITaskRepository> _taskRepository;
        private Mock<ITasksLogRepository> _tasksLogRepository;
        private Mock<ITaskCommentRepository> _taskCommentRepository;
        private Mock<IUserRepository> _userRepository;

        private Mock<IBaseMapper<ProjectDto, Projects>> _projectMapperMock;
        private Mock<IBaseMapper<TaskDto, Tasks>> _tasksMapperMock;
        private Mock<IBaseMapper<TaskEditDto, Tasks>> _tasksEditMapperMock;
        private Mock<IBaseMapper<TasksCommentDto, TasksComment>> _tasksCommentMapperMock;

        private IProjectService _projectService;
        private ITaskService _taskService;
        private ITaskLogService _taskLogService;
        private ITaskCommentService _taskCommentService;
        private IUserService _userService;
        private IReportsService _reportsService;

        public ProjectController projectController;
        public TaskController taskController;
        public TaskCommentController taskCommentController;
        public ReportsController reportsController;

        public Projects project;
        public List<Projects> projects = [];
        public Tasks task;
        public Tasks taskProject2;
        public List<Tasks> tasks19 = [];
        public List<Tasks> tasks20 = [];
        public TasksLog tasksLog;
        public List<TasksLog> tasksLogs = [];
        public TasksComment tasksComment;
        public List<TasksComment> tasksComments = [];
        public User userAdm;
        public User user;

        public ProjectDto projectDto;
        public TaskDto taskDto;
        public TaskDto taskDtoProject2;
        public TaskEditDto taskEditDto;
        public TaskEditDto taskEditDto2;
        public TasksCommentDto taskCommentsDto;

        public BaseFixture()
        {
            CreateBaseObjects();

            SetupMock();

            ConfigureServices();

            ConfigureControllers();
        }

        private void CreateBaseObjects()
        {
            project = new Projects
            {
                Id = 1,
                Name = "Test"
            };

            projects.Add(project);

            task = new Tasks
            {
                Id = 1,
                ProjectId = project.Id,
                Priority = Core.Enum.PriorityEnum.Low,
                Details = "Test",
                Status = Core.Enum.StatusEnum.New,
            };
            
            taskProject2 = new Tasks
            {
                Id = 2,
                ProjectId = 2,
                Priority = Core.Enum.PriorityEnum.Medium,
                Details = "Test 2",
                Status = Core.Enum.StatusEnum.InProgress,
            };

            tasks20.Add(task);

            for (int i = 0; i < 19;  i++) 
            {
                tasks19.Add(task);
                tasks20.Add(task);
            }

            tasksLog = new TasksLog(1, 1);
            tasksLogs.Add(tasksLog);

            tasksComment = new TasksComment(1, "test");
            tasksComments.Add(tasksComment);

            userAdm = new User("adm", Core.Enum.RoleEnum.Manager)
            {
                Id = 1
            };
            user = new User("user", Core.Enum.RoleEnum.Employee)
            {
                Id = 2
            };

            projectDto = new ProjectDto();
            taskDto = new TaskDto() { ProjectId = 1, Status = Core.Enum.StatusEnum.Done, Details = "Details", Priority = Core.Enum.PriorityEnum.High };
            taskDtoProject2 = new TaskDto() { ProjectId = 2, Status = Core.Enum.StatusEnum.Done, Details = "Details", Priority = Core.Enum.PriorityEnum.High };
            taskEditDto = new TaskEditDto() { Id = 1, Status = Core.Enum.StatusEnum.Done, Details = "Details" };
            taskEditDto2 = new TaskEditDto() { Id = 2, Status = Core.Enum.StatusEnum.InProgress, Details = "Details 2" };
            taskCommentsDto = new TasksCommentDto();
        }

        private void SetupMock()
        {
            _projectRepository = new Mock<IProjectRepository>();
            _projectRepository.Setup(service => service.Create(project)).ReturnsAsync(project);
            _projectRepository.Setup(service => service.Update(project));
            _projectRepository.Setup(service => service.Delete(project));
            _projectRepository.Setup(service => service.GetAll()).ReturnsAsync(projects);
            _projectRepository.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(project);

            _taskRepository = new Mock<ITaskRepository>();
            _taskRepository.Setup(service => service.Create(task)).ReturnsAsync(task);
            _taskRepository.Setup(service => service.Update(task));
            _taskRepository.Setup(service => service.Delete(task));
            _taskRepository.Setup(service => service.GetAll()).ReturnsAsync(tasks19);
            _taskRepository.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync(task);
            _taskRepository.Setup(service => service.GetProjectTasks(1)).ReturnsAsync(tasks19);
            _taskRepository.Setup(service => service.GetProjectTasks(2)).ReturnsAsync(tasks20);
            _taskRepository.Setup(service => service.GetProjectTasks(3));

            _tasksLogRepository = new Mock<ITasksLogRepository>();
            _tasksLogRepository.Setup(service => service.Create(tasksLog)).ReturnsAsync(tasksLog);
            _tasksLogRepository.Setup(service => service.TasksDonePast30Days()).ReturnsAsync(30);

            _taskCommentRepository = new Mock<ITaskCommentRepository>();
            _taskCommentRepository.Setup(service => service.Create(tasksComment)).ReturnsAsync(tasksComment);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(service => service.GetById(1)).ReturnsAsync(userAdm);
            _userRepository.Setup(service => service.GetById(2)).ReturnsAsync(user);
            
            SetupMapperMock();
        }

        private void SetupMapperMock()
        {
            _projectMapperMock = new Mock<IBaseMapper<ProjectDto, Projects>>();
            _projectMapperMock.Setup(mapper => mapper.MapModel(projectDto))
                              .Returns(project);

            _tasksMapperMock = new Mock<IBaseMapper<TaskDto, Tasks>>();
            _tasksMapperMock.Setup(mapper => mapper.MapModel(taskDto))
                              .Returns(task);

            _tasksEditMapperMock = new Mock<IBaseMapper<TaskEditDto, Tasks>>();
            _tasksEditMapperMock.Setup(mapper => mapper.MapModel(taskEditDto))
                              .Returns(task);
            _tasksEditMapperMock.Setup(mapper => mapper.MapModel(taskEditDto2))
                              .Returns(taskProject2);

            _tasksCommentMapperMock = new Mock<IBaseMapper<TasksCommentDto, TasksComment>>();
            _tasksCommentMapperMock.Setup(mapper => mapper.MapModel(taskCommentsDto))
                              .Returns(tasksComment);
        }

        private void ConfigureServices()
        {
            _userService = new UserService(_userRepository.Object);
            _taskLogService = new TaskLogService(_tasksLogRepository.Object);
            _taskCommentService = new TaskCommentService(_taskCommentRepository.Object, _tasksCommentMapperMock.Object);
            _taskService = new TaskService(_taskRepository.Object, _tasksEditMapperMock.Object, _tasksMapperMock.Object, _taskLogService, _userService);
            _projectService = new ProjectService(_projectRepository.Object, _projectMapperMock.Object, _taskService);
            _reportsService = new ReportsService(_taskLogService, _userService);
        }

        private void ConfigureControllers()
        {
            projectController = new ProjectController(_projectService);
            taskController = new TaskController(_taskService);
            taskCommentController = new TaskCommentController(_taskCommentService);
            reportsController = new ReportsController(_reportsService);
        }
    }
}
