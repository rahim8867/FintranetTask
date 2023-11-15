using CongestionTaxCalculator.Core.Common;
using CongestionTaxCalculator.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CongestionTaxCalculator.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(IUnitOfWork unitOfWork)
        {
            _dbContext = unitOfWork.Context;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity) 
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity) 
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity) 
        {
            _dbSet.Remove(entity);
        }

        public void Delete(int id) 
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                throw new RecordNotFoundException(id, typeof(T));
            _dbSet.Remove(entity);
        }

        public T Get(int id) 
        {
            return _dbSet.Find(id);
        }
        public async Task<T> GetAsync(int id) 
        {
            return await _dbSet.FindAsync(id);
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IList<T> GetAll(Expression<Func<T, bool>>? predicate = null) 
        {
            if (predicate == null)
                return _dbSet.ToList();

            return _dbSet.Where(predicate).ToList();
        }
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null) 
        {
            if(predicate == null)
                return await _dbSet.ToListAsync();

            return await _dbSet.Where(predicate).ToListAsync();
        }


        public void Update(T entity) 
        {
            _dbSet.Update(entity);
        }

    }
}
