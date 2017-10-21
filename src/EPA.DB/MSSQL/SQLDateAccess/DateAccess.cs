using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.Interfaces;
using EPA.DB.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class DateAccess: ILastSyncProvider
    {
        EpaContext context = new EpaContext();

        public EPA.Common.DTO.CommonDate GetDate()
        {
            return context.Dates.ToList().Last();
        }

        
    }
}
