using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.Interfaces;
using EPA.DB.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class TestInfoDataAccess: IProfTestInfoProvider
    {
        TestContext context = new TestContext();

        public EPA.Common.DTO.CommonTestDetailedInfo GetDate()
        {
            return context.Dates.ToList().Last();
        }

        
    }
}
