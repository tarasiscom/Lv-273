using System;
using System.Collections.Generic;
using System.Text;
using EPA.DB.MSSQL.Models;
using System.Linq;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO.ProfTest;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class ProfTestInfoProvider : ITestProvider
    {
        private EpaContext context;

        public ProfTestInfoProvider()
        {
            this.context = new EpaContext();
        }

        public TestInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId).ToCommon();
        }

        public IEnumerable<Test> GetTests()
        {
            return context.Tests.Select(item => item.ToCommon());
        }
    }
}
