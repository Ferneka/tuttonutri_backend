using System;
using System.Collections.Generic;
using System.Text;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IAggregateRoot
    {
        void Add(TEntity entity);
        IUnitOfWork UnitOfWork { get; }
        void Update(TEntity entity);
        Task<TEntity> GetById(TKey id);
    }
}
