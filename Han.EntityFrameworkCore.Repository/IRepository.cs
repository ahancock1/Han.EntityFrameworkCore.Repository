namespace Han.EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Interface that exposes basic CRUD functionality in a generic repository for a <see cref="DbSet{TEntity}" />.
    /// </summary>
    /// <typeparam name="TEntity">The entity type used for this repository</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Filters the <see cref="DbSet{TEntity}"/> based on a predicate, sorts in ascending order, skips a number
        ///     of entities and returns the specified number of entities. Includes specifies which related entities to 
        ///     include in the query results.
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="sort">The ascending order to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="skip">The number of entities to skip. </param>
        /// <param name="take">The number of entities to take. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The queried entities</returns>
        IEnumerable<TEntity> All(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort = null,
            int? skip = null,
            int? take = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Asynchronously filters the <see cref="DbSet{TEntity}"/> based on a predicate, sorts in ascending order, skips a number
        ///     of entities and returns the specified number of entities. Includes specifies which related entities to 
        ///     include in the query results.
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="sort">The ascending order to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="skip">The number of entities to skip. </param>
        /// <param name="take">The number of entities to take. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The queried entities</returns>
        Task<IEnumerable<TEntity>> AllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort = null,
            int? skip = null,
            int? take = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Determines whether any entities in the <see cref="DbSet{TEntity}" /> satisfy
        ///     a condition.
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>True if any entities satisfy the condition. </returns>
        bool Any(
            Expression<Func<TEntity, bool>> predicate = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Asynchronously determines whether any entities in the <see cref="DbSet{TEntity}" /> satisfy
        ///     a condition.
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>True if any entities satisfy the condition. </returns>
        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Inserts the specified entities into the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity used in <see cref="DbSet{TEntity}" />. </typeparam>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        bool Create(params TEntity[] entities);

        /// <summary>
        ///     Asynchronously inserts the specified entities into the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <param name="entities">The entities to create. </param>
        /// <returns>True if all entities have been created. </returns>
        Task<bool> CreateAsync(params TEntity[] entities);

        /// <summary>
        ///     Removes the specified entities from the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <param name="entities">The entities to remove. </param>
        /// <returns>True if all the entities have been deleted. </returns>
        bool Delete(params TEntity[] entities);

        /// <summary>
        ///     Asynchronously removes the specified entities from the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <param name="entities">The entities to remove. </param>
        /// <returns>True if all the entities have been deleted. </returns>
        Task<bool> DeleteAsync(params TEntity[] entities);

        /// <summary>
        ///     Retrieves the first entity from the <see cref="DbSet{TEntity}" /> that satisfies the specified
        ///     condition otherwise returns default value.
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The first entity matching the condition</returns>
        TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Asynchronously retrieves the first entity from the <see cref="DbSet{TEntity}" /> that satisfies the specified
        ///     condition otherwise returns default value
        /// </summary>
        /// <param name="predicate">The condition to apply to the <see cref="DbSet{TEntity}" />. </param>
        /// <param name="includes">The related entities to include. </param>
        /// <returns>The first entity matching the condition</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        /// <summary>
        ///     Updates the specified entities in the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entities have been updated. </returns>
        bool Update(params TEntity[] entities);

        /// <summary>
        ///     Asynchronously updates the specified entities in the <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <param name="entities">The entities to update. </param>
        /// <returns>True if all the entities have been updated. </returns>
        Task<bool> UpdateAsync(params TEntity[] entities);
    }
}