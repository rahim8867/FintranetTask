using System.Linq.Expressions;

namespace CongestionTaxCalculator.Core.Common
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IQueryable<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>>? predicate = null);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);


    }
}
