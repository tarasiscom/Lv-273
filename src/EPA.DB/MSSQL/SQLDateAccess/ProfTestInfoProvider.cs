using System;
using System.Collections.Generic;
using System.Text;
using EPA.DB.MSSQL.Models;
using System.Linq;
using EPA.Common.Interfaces;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class ProfTestInfoProvider:IProfTestInfoProvider
    {
        DateContext context;
        public ProfTestInfoProvider()
        {
            context = new DateContext();
        }
        CommonTestDetailedInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId);
        }
        IEnumerable<CommonTestInfo> GetTests()
        {
            return context.Tests.ToList();
        }
    }
}
