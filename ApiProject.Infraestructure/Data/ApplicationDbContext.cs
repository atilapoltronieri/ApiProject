using ApiProject.Core.Entities.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {

        #region DbSet Section
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TasksLog> TasksLog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TasksComment> TasksComment { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplicationDbContextConfigurations.Configure(builder);
            ApplicationDbContextConfigurations.SeedData(builder);
        }
    }
}
