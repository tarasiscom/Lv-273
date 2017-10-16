using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dbinterfaces;
using EPA.DB.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EPA.DB.DataAcess
{
    public class DateOperation : LastSyncProvider<Date> 
    {

        public DateOperation(DateContext context) : base (context)
        {
        }
        public override Task<Date> GetLastAsync( long id)
        {
            return context.Dates.LastOrDefaultAsync(x => x.Id == id);
        }
    }
}
