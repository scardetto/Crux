using System.Collections.Generic;
using System.Linq;

namespace Crux.Domain.Entities
{
    public interface IRepository<TId>
    {
        void SaveAll<TEntity>(IEnumerable<DomainEntity<TId>> entities)
            where TEntity : DomainEntity<TId>;

        void Save<TEntity>(TEntity entity)
            where TEntity : DomainEntity<TId>;

        void SaveAndFlush<TEntity>(TEntity entity)
            where TEntity : DomainEntity<TId>;

        void Merge<TEntity>(TEntity entity)
            where TEntity : DomainEntity<TId>;

        void Evict<TEntity>(TEntity entity)
            where TEntity : DomainEntity<TId>;

        TEntity Get<TEntity>(TId id)
            where TEntity : DomainEntity<TId>;

        TEntity Load<TEntity>(TId id)
            where TEntity : DomainEntity<TId>;

        void Delete<TEntity>(TEntity entity)
            where TEntity : DomainEntity<TId>;

        IQueryable<TEntity> Query<TEntity>()
            where TEntity : DomainEntity<TId>;

        IQueryable<TEntity> Query<TEntity>(IDomainQuery<TEntity, TId> whereQuery)
            where TEntity : DomainEntity<TId>;

        IQueryable<TEntity> Evict<TEntity>(IQueryable<TEntity> queryable)
            where TEntity : DomainEntity<TId>;
    }
}
