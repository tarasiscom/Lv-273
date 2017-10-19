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
        DataContext context;
        public ProfTestInfoProvider()
        {
            context = new DataContext();
        }
        public ICommonTestDetailedInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId);
        }
        public IEnumerable<ICommonTestInfo> GetTests()
        {
            return context.Tests.ToList();
        }
    }
}
