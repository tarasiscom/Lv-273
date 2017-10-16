using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dbinterfaces;
using System.Threading.Tasks;
using EPA.DB.Models;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EPA.DB.DataAcess
{
    public abstract class LastSyncProvider<TEntity> : ILastSyncProvider<TEntity> where TEntity : Date
    {
        protected DateContext context;

        public LastSyncProvider(DateContext context)
        {
            this.context = context;
        }

        public virtual  Task<TEntity> GetLastAsync(long id)
        {
            return null;
        }
    }
}
