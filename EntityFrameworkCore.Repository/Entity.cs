// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   Entity.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace EntityFrameworkCore.Repository
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     Base Entity class
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>
        where TKey : IComparable
    {
        [Key]
        [Column("Key")]
        public TKey Id { get; set; }
    }
}
