using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPA.Common.dbinterfaces
{
    public interface ILastSyncProvider<TEntity> 
    {
        Task<TEntity> GetLastAsync(long id);
    }
}
