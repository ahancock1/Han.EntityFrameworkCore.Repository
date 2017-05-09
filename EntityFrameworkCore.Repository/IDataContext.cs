// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IDataContext.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace EntityFrameworkCore.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Seed();
        IDataContext CreateInstance();
    }
}
