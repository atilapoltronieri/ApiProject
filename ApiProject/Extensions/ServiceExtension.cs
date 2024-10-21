using ApiProject.Core.Entities.Business;
using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Interfaces.IMapper;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Core.Interfaces.IServices;
using ApiProject.Core.Mapper;
using ApiProject.Core.Services;
using ApiProject.Infraestructure.Repositories;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskLogService, TaskLogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<ITaskCommentService, TaskCommentService>();
            #endregion

            #region Repositories
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITasksLogRepository, TaskLogRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskCommentRepository, TaskCommentRepository>();
            #endregion

            #region Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Projects, ProjectDto>();
                cfg.CreateMap<ProjectDto, Projects>();
                cfg.CreateMap<Tasks, TaskDto>();
                cfg.CreateMap<TaskDto, Tasks>();
                cfg.CreateMap<Tasks, TaskEditDto>();
                cfg.CreateMap<TaskEditDto, Tasks>();                
                cfg.CreateMap<TasksComment, TasksCommentDto>();
                cfg.CreateMap<TasksCommentDto, TasksComment>();
            });
            IMapper mapper = configuration.CreateMapper();

            services.AddSingleton<IBaseMapper<Projects, ProjectDto>>(new BaseMapper<Projects, ProjectDto>(mapper));
            services.AddSingleton<IBaseMapper<ProjectDto, Projects>>(new BaseMapper<ProjectDto, Projects>(mapper));
            services.AddSingleton<IBaseMapper<Tasks, TaskDto>>(new BaseMapper<Tasks, TaskDto>(mapper));
            services.AddSingleton<IBaseMapper<TaskDto, Tasks>>(new BaseMapper<TaskDto, Tasks>(mapper));
            services.AddSingleton<IBaseMapper<Tasks, TaskEditDto>>(new BaseMapper<Tasks, TaskEditDto>(mapper));
            services.AddSingleton<IBaseMapper<TaskEditDto, Tasks>>(new BaseMapper<TaskEditDto, Tasks>(mapper));            
            services.AddSingleton<IBaseMapper<TasksComment, TasksCommentDto>>(new BaseMapper<TasksComment, TasksCommentDto>(mapper));
            services.AddSingleton<IBaseMapper<TasksCommentDto, TasksComment>>(new BaseMapper<TasksCommentDto, TasksComment>(mapper));
            #endregion

            return services;
        }
    }
}
