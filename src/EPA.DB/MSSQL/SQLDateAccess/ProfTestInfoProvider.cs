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
        EpaContext context;
        public ProfTestInfoProvider()
        {
            context = new EpaContext();
        }
        public CommonTestDetailedInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId).ToCommon();
        }
        public IEnumerable<CommonTestInfo> GetTests()
        {
            return context.Tests.Select(item => item.ToCommon());
        }
    }
}
