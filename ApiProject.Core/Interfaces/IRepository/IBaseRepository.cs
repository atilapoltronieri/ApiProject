using ApiProject.Core.Entities.Persistence;

namespace ApiProject.Core.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : BaseClass
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> IsExists<Tvalue>(string key, Tvalue value);
        Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value);
        Task<T> Create(T model);
        Task CreateRange(List<T> model);
        Task Update(T model);
        Task Delete(T model);
        Task SaveChangeAsync();
    }
}
