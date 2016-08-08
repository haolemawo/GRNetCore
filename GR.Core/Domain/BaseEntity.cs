using System;
using System.Collections.Generic;

namespace GR.Core.Domain
{
    public abstract class BaseEntity
    {
        public  int Id { get; set; }
        
        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public bool IsTransient()
        {
            return EqualityComparer<int>.Default.Equals(Id, default(int));
        }
    }
}
