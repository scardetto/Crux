using System;
using System.Linq.Expressions;

namespace Crux.Domain.Entities
{
    public interface IDomainQuery<TEntity, TId>
        where TEntity : DomainEntity<TId>
    {
        Expression<Func<TEntity, bool>> Expression { get; }
    }
}
