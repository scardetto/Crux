using System.Collections.Generic;
using System.Linq;

namespace Crux.Domain.Entities
{
    public interface IRepositoryOfId<TId>
    {
        void SaveAll<TEntity>(IEnumerable<DomainEntityOfId<TId>> entities)
            where TEntity : DomainEntityOfId<TId>;

        void Save<TEntity>(TEntity entity)
            where TEntity : DomainEntityOfId<TId>;

        void SaveAndFlush<TEntity>(TEntity entity)
            where TEntity : DomainEntityOfId<TId>;

        void Merge<TEntity>(TEntity entity)
            where TEntity : DomainEntityOfId<TId>;

        void Evict<TEntity>(TEntity entity)
            where TEntity : DomainEntityOfId<TId>;

        TEntity Get<TEntity>(TId id)
            where TEntity : DomainEntityOfId<TId>;

        TEntity Load<TEntity>(TId id)
            where TEntity : DomainEntityOfId<TId>;

        void Delete<TEntity>(TEntity entity)
            where TEntity : DomainEntityOfId<TId>;

        IQueryable<TEntity> Query<TEntity>()
            where TEntity : DomainEntityOfId<TId>;

        IQueryable<TEntity> Query<TEntity>(IDomainQueryOfId<TEntity, TId> whereQuery)
            where TEntity : DomainEntityOfId<TId>;

        IQueryable<TEntity> Evict<TEntity>(IQueryable<TEntity> queryable)
            where TEntity : DomainEntityOfId<TId>;
    }
}
