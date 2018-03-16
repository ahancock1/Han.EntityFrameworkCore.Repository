// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Repository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace Han.EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Creates an instance of a generic repository for a <see cref="DbContext" /> and
    ///     exposes basic CRUD functionality
    /// </summary>
    /// <typeparam name="TContext">The data context used for this repository</typeparam>
    /// <typeparam name="TEntity">The entity type used for this repository</typeparam>
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        /// <inheritdoc cref="IRepository{TEntity}.All"/>
        public IEnumerable<TEntity> All(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return Query(predicate, orderby, skip, take, includes).ToList();
        }

        /// <inheritdoc cref="IRepository{TEntity}.AllAsync"/>
        public async Task<IEnumerable<TEntity>> AllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await Query(predicate, orderby, skip, take, includes).ToListAsync();
        }

        /// <inheritdoc cref="IRepository{TEntity}.AnyAsync"/>
        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = GetDataContext())
            {
                var entities = AggregateIncludes(context, includes);

                return await (predicate != null ? entities.AnyAsync(predicate) : entities.AnyAsync());
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.Create"/>
        public bool Create(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                set.AddRange(entities);

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.CreateAsync"/>
        public async Task<bool> CreateAsync(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                await set.AddRangeAsync(entities);

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.Delete"/>
        public bool Delete(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                set.RemoveRange(entities);

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.DeleteAsync"/>
        public async Task<bool> DeleteAsync(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                set.RemoveRange(entities);

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.Update"/>
        public bool Update(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                set.UpdateRange(entities);

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.UpdateAsync"/>
        public async Task<bool> UpdateAsync(params TEntity[] entities)
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();

                set.UpdateRange(entities);

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.Any"/>
        public bool Any(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = GetDataContext())
            {
                var entities = AggregateIncludes(context, includes);

                return predicate != null ? entities.Any(predicate) : entities.Any();
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.Get"/>
        public TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = GetDataContext())
            {
                return AggregateIncludes(context, includes).FirstOrDefault(predicate);
            }
        }

        /// <inheritdoc cref="IRepository{TEntity}.GetAsync"/>
        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = GetDataContext())
            {
                return await AggregateIncludes(context, includes).FirstOrDefaultAsync(predicate);
            }
        }

        private IQueryable<TEntity> AggregateIncludes(TContext context,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return includes.Aggregate((IQueryable<TEntity>) context.Set<TEntity>(),
                (current, item) => current.Include(item)).AsQueryable();
        }

        private IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, object>> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = GetDataContext())
            {
                var items = AggregateIncludes(context, includes);

                if (predicate != null)
                {
                    items = items.Where(predicate);
                }

                if (orderby != null)
                {
                    items = items.OrderBy(orderby);
                }

                if (skip.HasValue)
                {
                    items = items.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    items = items.Take(take.Value);
                }

                return items;
            }
        }

        /// <summary>
        ///     Creates an instance of the <see cref="DbContext" />
        /// </summary>
        /// <returns>An instance of the <see cref="DbContext" /></returns>
        /// <remarks>This should always be disposed of afterwards</remarks>
        protected virtual TContext GetDataContext()
        {
            return Activator.CreateInstance<TContext>();
        }
    }
}