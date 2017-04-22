// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   Repository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace EntityFrameworkCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Core.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        private readonly IDataContext _context;

        public Repository(IDataContext context)
        {
            _context = context;
        }

        public ICollection<TEntity> All(params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return Query(context, include).ToList();
            }
        }

        public async Task<ICollection<TEntity>> AllAsync(params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return await Query(context, include).ToListAsync();
            }
        }

        public bool Delete(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Remove(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        public async Task<bool> DeleteAsync(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Remove(entity);
                }

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        public bool Exists(TKey id)
        {
            using (var context = _context.CreateInstance())
            {
                return Set(context).Any(e => e.Id.Equals(id));
            }
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            using (var context = _context.CreateInstance())
            {
                return await Set(context).AnyAsync(e => e.Id.Equals(id));
            }
        }

        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return Query(context, include).Where(predicate).ToList();
            }
        }

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return await Query(context, include).Where(predicate).ToListAsync();
            }
        }

        public TEntity Get(TKey id, params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return Query(context, include).SingleOrDefault(e => e.Id.Equals(id));
            }
        }

        public async Task<TEntity> GetAsync(TKey id, params Expression<Func<TEntity, object>>[] include)
        {
            using (var context = _context.CreateInstance())
            {
                return await Query(context, include).SingleOrDefaultAsync(e => e.Id.Equals(id));
            }
        }

        public bool Insert(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Add(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        public async Task<bool> InsertAsync(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Add(entity);
                }

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        public bool Update(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Update(entity);
                }

                return context.SaveChanges() >= entities.Length;
            }
        }

        public async Task<bool> UpdateAsync(params TEntity[] entities)
        {
            using (var context = _context.CreateInstance())
            {
                var set = Set(context);
                foreach (var entity in entities)
                {
                    set.Update(entity);
                }

                return await context.SaveChangesAsync() >= entities.Length;
            }
        }

        private IQueryable<TEntity> Query(IDataContext context, params Expression<Func<TEntity, object>>[] include)
        {
            return include.Aggregate((IQueryable<TEntity>) Set(context), (current, item) => current.Include(item));
        }

        private DbSet<TEntity> Set(IDataContext context)
        {
            return context.Set<TEntity>();
        }
    }
}
