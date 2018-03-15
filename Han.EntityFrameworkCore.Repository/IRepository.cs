// -----------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="Solentim">
//      Copyright (c) Solentim 2018. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Han.EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<TEntity> where TEntity : class
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
        /// <returns>The queried entities</returns>
        IEnumerable<TEntity> All(
            Func<TEntity, bool> predicate = null,
            Func<TEntity, object> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Retrieves entities from the <see cref="DbSet{TEntity}" /> and optionally performs a filter, order by,
        ///     number of entities to skip and take. Allows include to be performed on the <see cref="DbSet{TEntity}" />
        ///     async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="orderby">The ascending order to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="skip">The number of entites to skip. </param>
        /// <param name="take">The number of entities to take. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The queried entities</returns>
        Task<IEnumerable<TEntity>> AllAsync(
            Func<TEntity, bool> predicate = null,
            Func<TEntity, object> orderby = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Determines whether any entities in the <see cref="DbSet{TEntity}"/> satisfies the
        /// filter.
        /// </summary>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>True if any entities satisfy the filter. </returns>
        bool Any(
            Func<TEntity, bool> predicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Determines whether any entities in the <see cref="DbSet{TEntity}"/> satisfies the
        /// filter async.
        /// </summary>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>True if any entities satisfy the filter. </returns>
        Task<bool> AnyAsync(
            Func<TEntity, bool> predicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Creates the entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        bool Create(params TEntity[] entities);

        /// <summary>
        ///     Creates the entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        Task<bool> CreateAsync(params TEntity[] entities);

        /// <summary>
        ///     Deletes the given entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to delete. </param>
        /// <returns>True if all the entites have been deleted. </returns>
        bool Delete(params TEntity[] entities);

        /// <summary>
        ///     Deletes the given entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to delete. </param>
        /// <returns>True if all the entites have been deleted. </returns>
        Task<bool> DeleteAsync(params TEntity[] entities);

        /// <summary>
        ///     Retrieves the first entity from their <see cref="DbSet{TEntity}"/> otherwise returns
        /// null.
        /// </summary>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The first entity matching the filter</returns>
        TEntity Get(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Retrieves the first entity from their <see cref="DbSet{TEntity}"/> otherwise returns
        /// null async.
        /// </summary>
        /// <param name="predicate">The filter to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The first entity matching the filter</returns>
        Task<TEntity> GetAsync(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Updates the given entities in their <see cref="DbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entites have been updated. </returns>
        bool Update(params TEntity[] entities);

        /// <summary>
        ///     Updates the given entities in their <see cref="DbSet{TEntity}" /> async.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entites have been updated. </returns>
        Task<bool> UpdateAsync(params TEntity[] entities);
    }
}