using System.Collections.Generic;
using System.Linq;
using Crux.Core.Extensions;
using Crux.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Crux.Domain.Persistence.NHibernate
{
    public class NHibernateRepository<TId> : IRepository<TId>
    {
        private readonly INHibernateUnitOfWork _unitOfWork;

        public NHibernateRepository(INHibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ISession Session
        {
            get { return _unitOfWork.CurrentSession; }
        }

        public void SaveAll<TEntity>(IEnumerable<DomainEntity<TId>> entities) where TEntity : DomainEntity<TId>
        {
            entities.Each(Save);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : DomainEntity<TId>
        {
            Session.SaveOrUpdate(entity);
        }

        public void SaveAndFlush<TEntity>(TEntity entity) where TEntity : DomainEntity<TId>
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
        }

        public void Merge<TEntity>(TEntity entity) where TEntity : DomainEntity<TId>
        {
            Session.Merge(entity);
        }

        public void Evict<TEntity>(TEntity entity) where TEntity : DomainEntity<TId>
        {
            Session.Evict(entity);
        }

        public TEntity Get<TEntity>(TId id) where TEntity : DomainEntity<TId>
        {
            return Session.Get<TEntity>(id);
        }

        public TEntity Load<TEntity>(TId id) where TEntity : DomainEntity<TId>
        {
            return Session.Load<TEntity>(id);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : DomainEntity<TId>
        {
            Session.Delete(entity);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : DomainEntity<TId>
        {
            return Session.Query<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(IDomainQuery<TEntity, TId> whereQuery) where TEntity : DomainEntity<TId>
        {
            return Session.Query<TEntity>().Where(whereQuery.Expression);
        }

        public IQueryable<TEntity> Evict<TEntity>(IQueryable<TEntity> queryable) where TEntity : DomainEntity<TId>
        {
            queryable.Each(i => Session.Evict(i));
            return queryable;
        }
    }
}
