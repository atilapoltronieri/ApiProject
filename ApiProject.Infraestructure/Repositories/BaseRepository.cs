using ApiProject.Core.Entities.Persistence;
using ApiProject.Core.Exceptions;
using ApiProject.Core.Interfaces.IRepository;
using ApiProject.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ApiProject.Infraestructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseClass
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet.Where(x => x.Id == 1).ToList();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var data = await _dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();

            return data;
        }

        public async Task<T> GetById(int id)
        {
            var data = await _dbContext.Set<T>().AsNoTracking().SingleAsync(x => x.Id == id);
            _dbContext.SaveChanges();
            if (data == null)
                throw new NotFoundException($"No data found.");
            
            return data;
        }

        public async Task<bool> IsExists<Tvalue>(string key, Tvalue value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }

        //Before update existence check
        public async Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);

            var idProperty = Expression.Property(parameter, "Id");
            var idEquality = Expression.NotEqual(idProperty, Expression.Constant(id));

            var combinedExpression = Expression.AndAlso(equality, idEquality);
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }

        public async Task<T> Create(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task CreateRange(List<T> model)
        {
            await _dbContext.Set<T>().AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T model)
        {
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
