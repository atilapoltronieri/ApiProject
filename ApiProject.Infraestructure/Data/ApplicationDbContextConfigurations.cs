using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Enum;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Infraestructure.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContextConfigurations
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            // Add any additional entity configurations here
        }

        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Generate fake project data.
            var productIds = Enumerable.Range(1, 20).ToList();
            var fakerSeedProducts = new Faker<Projects>()
                .RuleFor(c => c.Id, f =>
                {
                    var index = f.Random.Int(0, productIds.Count - 1);
                    var id = productIds[index];
                    productIds.RemoveAt(index);
                    return id;
                })
                .RuleFor(p => p.Name, f => f.Commerce.ProductName());

            var fakeProjects = fakerSeedProducts.Generate(10);
            modelBuilder.Entity<Projects>().HasData(fakeProjects);
            var projectsIds = fakeProjects.Select(x => x.Id).ToList();

            // Generate fake task data.
            var taskIds = Enumerable.Range(1, 20).ToList();
            var fakerSeedTasks = new Faker<Tasks>()
                .RuleFor(c => c.Id, f =>
                {
                    var index = f.Random.Int(0, taskIds.Count - 1);
                    var id = taskIds[index];
                    taskIds.RemoveAt(index);
                    return id;
                })
                .RuleFor(p => p.Details, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Status, StatusEnum.New)
                .RuleFor(p => p.Priority, f => f.PickRandom<PriorityEnum>())
                .RuleFor(p => p.ProjectId, f => f.PickRandom(projectsIds));

            var fakeTasks = fakerSeedTasks.Generate(10);
            modelBuilder.Entity<Tasks>().HasData(fakeTasks);

            // Generate fake user.
            var fakeUsers = new List<User>();
            var adm = new User("adm", RoleEnum.Manager)
            {
                Id = 1
            };
            var user = new User("user", RoleEnum.Employee)
            {
                Id = 2
            };

            fakeUsers.Add(adm);
            fakeUsers.Add(user);

            modelBuilder.Entity<User>().HasData(fakeUsers);
        }
    }
}
