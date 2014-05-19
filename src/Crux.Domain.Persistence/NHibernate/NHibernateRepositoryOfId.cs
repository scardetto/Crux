using System.Collections.Generic;
using System.Linq;
using Crux.Core.Extensions;
using Crux.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Crux.Domain.Persistence.NHibernate
{
    public class NHibernateRepositoryOfId<TId> : IRepositoryOfId<TId>
    {
        private readonly INHibernateUnitOfWork _unitOfWork;

        public NHibernateRepositoryOfId(INHibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ISession Session
        {
            get { return _unitOfWork.CurrentSession; }
        }

        public void SaveAll<TEntity>(IEnumerable<DomainEntityOfId<TId>> entities) where TEntity : DomainEntityOfId<TId>
        {
            entities.Each(Save);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : DomainEntityOfId<TId>
        {
            Session.SaveOrUpdate(entity);
        }

        public void SaveAndFlush<TEntity>(TEntity entity) where TEntity : DomainEntityOfId<TId>
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
        }

        public void Merge<TEntity>(TEntity entity) where TEntity : DomainEntityOfId<TId>
        {
            Session.Merge(entity);
        }

        public void Evict<TEntity>(TEntity entity) where TEntity : DomainEntityOfId<TId>
        {
            Session.Evict(entity);
        }

        public TEntity Get<TEntity>(TId id) where TEntity : DomainEntityOfId<TId>
        {
            return Session.Get<TEntity>(id);
        }

        public TEntity Load<TEntity>(TId id) where TEntity : DomainEntityOfId<TId>
        {
            return Session.Load<TEntity>(id);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : DomainEntityOfId<TId>
        {
            Session.Delete(entity);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : DomainEntityOfId<TId>
        {
            return Session.Query<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(IDomainQueryOfId<TEntity, TId> whereQuery) where TEntity : DomainEntityOfId<TId>
        {
            return Session.Query<TEntity>().Where(whereQuery.Expression);
        }

        public IQueryable<TEntity> Evict<TEntity>(IQueryable<TEntity> queryable) where TEntity : DomainEntityOfId<TId>
        {
            queryable.Each(i => Session.Evict(i));
            return queryable;
        }
    }
}
