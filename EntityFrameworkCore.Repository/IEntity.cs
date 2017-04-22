// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IEntity.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace EntityFrameworkCore.Repository
{
    using System;

    /// <summary>
    ///     Entity interface
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
        where TKey : IComparable
    {
        /// <summary>
        ///     The key for the data model
        /// </summary>
        TKey Id { get; set; }
    }
}
