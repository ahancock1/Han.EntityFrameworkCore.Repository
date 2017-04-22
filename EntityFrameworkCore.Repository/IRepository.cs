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
        where TEntity : IEntity<TKey>
        where TKey : IComparable
    {
        ICollection<TEntity> All(params Expression<Func<TEntity, object>>[] include);

        Task<ICollection<TEntity>> AllAsync(params Expression<Func<TEntity, object>>[] include);

        bool Delete(params TEntity[] entities);

        Task<bool> DeleteAsync(params TEntity[] entities);

        bool Exists(TKey key);

        Task<bool> ExistsAsync(TKey key);

        TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] include);

        Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] include);

        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] include);

        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] include);

        bool Insert(params TEntity[] entities);
        
        Task<bool> InsertAsync(params TEntity[] entities);

        bool Update(params TEntity[] entities);
        
        Task<bool> UpdateAsync(params TEntity[] entities);
    }
}
