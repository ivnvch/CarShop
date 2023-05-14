using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarWorkShop.DAL.Repositories
{
    public class OwnerRepository : IBaseRepository<Owner>
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Owner entity)
        {
            await _context.Owners.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Owner entity)
        {
            _context.Owners.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Owner> Update(Owner entity)
        {
            _context.Owners.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Owner> GetAll()
        {
            return _context.Owners.Include(x => x.Profile);
        }
    }
}
