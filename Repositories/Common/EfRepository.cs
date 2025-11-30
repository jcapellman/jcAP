using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using jcAP.API.Data;
using Microsoft.EntityFrameworkCore;

namespace jcAP.API.Repositories.Common
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> _set;

        public EfRepository(AppDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _set = _db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(bool asNoTracking = true)
            => asNoTracking ? _set.AsNoTracking() : _set;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await Query(true).ToListAsync(cancellationToken);

        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var found = await _set.FindAsync(new object[] { id }, cancellationToken);
            return found;
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(entity, cancellationToken);
        }

        public virtual void Update(TEntity entity) => _set.Update(entity);

        public virtual void Remove(TEntity entity) => _set.Remove(entity);

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => _set.AsNoTracking().AnyAsync(predicate, cancellationToken);

        public virtual Task<long> CountAsync(CancellationToken cancellationToken = default)
            => _set.LongCountAsync(cancellationToken);

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _db.SaveChangesAsync(cancellationToken);
    }
}