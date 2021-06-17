using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EfRepository<TEntity> : IAsyncRepository<TEntity> where TEntity: class
    {
        protected readonly AuctionDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public EfRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            if(saveChanges) await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var keys = new object[] {id};
            return await _dbSet.FindAsync(keys, cancellationToken);
        }
        
        public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.CountAsync(cancellationToken);
        }

        public async Task<TEntity> FirstAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync(cancellationToken);
        }
        
        public async Task<TEntity> LastAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.LastAsync(cancellationToken);
        }

        public async Task<TEntity> LastOfDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.LastOrDefaultAsync(cancellationToken);
        }
        
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec, bool evaluateCriteriaOnly = false)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery<TEntity>(_dbContext.Set<TEntity>().AsQueryable(), spec, evaluateCriteriaOnly);
        }
    }
}