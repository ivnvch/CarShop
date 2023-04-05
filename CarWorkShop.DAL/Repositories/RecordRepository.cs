﻿using CarWorkShop.DAL.Interfaces;
using CarWorkShop.Models.Entity;

namespace CarWorkShop.DAL.Repositories
{
    public class RecordRepository : IBaseRepository<Record>
    {
        private readonly DataContext _context;

        public RecordRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Record entity)
        {
            await _context.Records.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Record entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Record> Update(Record entity)
        {
            _context.Records.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public IQueryable<Record> GetAll()
        {
            return _context.Records;
        }
    }
}
