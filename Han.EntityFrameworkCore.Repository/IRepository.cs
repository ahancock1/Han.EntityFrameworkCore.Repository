// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace Han.EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<out TContext>
        where TContext : DbContext
    {
        IEnumerable<TEntity> All<TEntity>(
            Func<TEntity, bool> predicate = null,
            Func<TEntity, object> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] include)
            where TEntity : class;

        bool Create<TEntity>(params TEntity[] entities)
            where TEntity : class;

        bool Delete<TEntity>(params TEntity[] entities)
            where TEntity : class;

        bool Update<TEntity>(params TEntity[] entities)
            where TEntity : class;
    }
}
