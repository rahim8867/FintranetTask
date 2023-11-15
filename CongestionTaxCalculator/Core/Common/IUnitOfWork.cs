using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Core.Common
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
