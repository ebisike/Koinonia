using Koinonia.Domain.Interface;
using Koinonia.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected KoinoniaDbContext _context;

        public Repository(KoinoniaDbContext context)
        {
            _context = context;
        }
        public T AddNew(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddNewAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(Guid Id)
        {
            var entity = _context.Set<T>().Find(Id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public T Get(Guid Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }        

        public async Task<T> GetAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public T GetKoinoniaUser(string userId)
        {
            return _context.Set<T>().Find(userId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
