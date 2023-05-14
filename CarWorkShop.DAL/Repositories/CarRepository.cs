using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;


namespace CarWorkShop.DAL.Repositories
{
    public class CarRepository : IBaseRepository<Car>
    {

        private DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Car entity)
        {
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Car> Update(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}
