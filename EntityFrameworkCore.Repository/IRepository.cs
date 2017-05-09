// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        ICollection<TEntity> All(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            int? skip = null,
            int? take = null);

        Task<ICollection<TEntity>> AllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            int? skip = null,
            int? take = null);

        int Count();

        Task<int> CountAsync();
        
        bool Create(TEntity entity);

        Task<bool> CreateAsync(TEntity entity);

        bool Delete(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        bool Exists(TEntity entity);

        Task<bool> ExistsAsync(TEntity entity);

        ICollection<TEntity> Find(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            int? skip = null,
            int? take = null);

        Task<ICollection<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderBy = null,
            int? skip = null,
            int? take = null);

        void Include(params Expression<Func<TEntity, object>>[] includes);

        TEntity Retrieve(TKey id);

        Task<TEntity> RetrieveAsync(TKey id);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);
    }
}
