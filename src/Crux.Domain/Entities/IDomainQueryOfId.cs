using System;
using System.Linq.Expressions;

namespace Crux.Domain.Entities
{
    public interface IDomainQueryOfId<TEntity, TId>
        where TEntity : DomainEntityOfId<TId>
    {
        Expression<Func<TEntity, bool>> Expression { get; }
    }
}
