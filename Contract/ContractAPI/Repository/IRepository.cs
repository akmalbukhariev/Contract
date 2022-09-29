using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Repository
{
    public interface IRepository<T> where T : class
    {     
        ///
        /// Find one item of an entity by asynchronous method
        /// 
        ///
        /// 
        Task FindAsync(params object[] keyValues);
        ///
        /// Insert item into an entity by asynchronous method
        /// 
        ///
        ///
        /// 
        Task AddAsync(T entity, bool saveChanges = true);
        ///
        /// Insert multiple items into an entity by asynchronous method
        /// 
        ///
        ///
        /// 
        Task AddRangeAsync(IEnumerable entities, bool saveChanges = true);
        ///
        /// Remove one item from an entity by asynchronous method
        ///
        /// 
        ///
        ///
        /// 
        Task DeleteAsync(T entity, bool saveChanges = true);
        ///
        /// Remove multiple items from an entity by asynchronous method
        /// 
        ///
        ///
        /// 
        Task DeleteRangeAsync(IEnumerable entities, bool saveChanges = true);

        Task UpdateAsync(IEnumerable entities, bool saveChanges = true);
    }
}
