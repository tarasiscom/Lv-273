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
        // Not keeped naming convention with Natalia. It is temp.
        public TestDetailedInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId);
        }
        public IEnumerable<TestInfo> GetTests()
        {
            return context.Tests.ToList();
        }
    }
}
