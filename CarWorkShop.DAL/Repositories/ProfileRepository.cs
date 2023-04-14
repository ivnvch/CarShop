using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;

namespace CarWorkShop.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Profile entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Profile entity)
        {
             _context.Remove(entity);
           await _context.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return _context.Profiles;
        }

        public async Task<Profile> Update(Profile entity)
        {
             _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
