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
    public abstract class Repository<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///     Retrieves entities from the <see cref="DbSet{TEntity}" /> and optionally performs a filter, order by,
        ///     number of entities to skip and take. Allows include to be performed on the <see cref="DbSet{TEntity}" />
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="orderby">The ascending order to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="skip">The number of entites to skip. </param>
        /// <param name="take">The number of entities to take. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns></returns>
        protected IEnumerable<TEntity> All<TEntity>(
            Func<TEntity, bool> predicate = null,
            Func<TEntity, object> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class
        {
            using (var context = GetDataContext())
            {
                var items = includes.Aggregate((IQueryable<TEntity>) context.Set<TEntity>(),
                    (current, item) => current.Include(item)).AsEnumerable();

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

                return items.ToList();
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderby"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        protected Task<IEnumerable<TEntity>> AllAsync<TEntity>(
            Func<TEntity, bool> predicate = null,
            Func<TEntity, object> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] include)
            where TEntity : class
        {
            return Task.Run(() => All(predicate, orderby, skip, take, include));
        }

        /// <summary>
        ///     Creates the entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        protected bool Create<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();
                foreach (var entity in entities)
                {
                    set.Add(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <summary>
        ///     Creates the entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        protected Task<bool> CreateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            return Task.Run(() => Create(entities));
        }

        /// <summary>
        ///     Deletes the given entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to delete. </param>
        /// <returns>True if all the entites have been deleted. </returns>
        protected bool Delete<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();
                foreach (var entity in entities)
                {
                    set.Remove(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <summary>
        ///     Deletes the given entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to delete. </param>
        /// <returns>True if all the entites have been deleted. </returns>
        protected Task<bool> DeleteAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            return Task.Run(() => Delete(entities));
        }

        /// <summary>
        ///     Updates the given entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entites have been updated. </returns>
        protected bool Update<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            using (var context = GetDataContext())
            {
                var set = context.Set<TEntity>();
                foreach (var entity in entities)
                {
                    set.Update(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        /// <summary>
        ///     Updates the given entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entites have been updated. </returns>
        protected Task<bool> UpdateAsync<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            return Task.Run(() => Update(entities));
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