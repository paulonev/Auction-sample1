using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class /*BaseEntity, IAggregateRoot - DDD*/
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        
        Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

        Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    }
}