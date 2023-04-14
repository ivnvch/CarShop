
namespace CarWorkShop.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        IQueryable<T> GetAll();//возможно стоит заменить IQueryable на IEnumerable
    }
}
