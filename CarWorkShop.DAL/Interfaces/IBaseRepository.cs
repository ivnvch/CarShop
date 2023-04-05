
namespace CarWorkShop.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(T entity);
        Task<T> Update(T entity);
        IQueryable<T> GetAll();//возможно стоит заменить IQueryable на IEnumerable
    }
}
