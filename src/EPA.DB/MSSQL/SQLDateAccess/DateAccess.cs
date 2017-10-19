using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.Interfaces;
using EPA.DB.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EPA.Common.DTO;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class DateAccess: ILastSyncProvider
    {
        DateContext context = new DateContext();

        public CommonDate GetDate()
        {
            return context.Dates.ToList().Last();
        }

        
    }
}
