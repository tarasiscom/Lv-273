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
            List<CommonTestInfo> cti = new List<CommonTestInfo>();
            foreach (var v in context.Tests)
            {
                cti.Add((new TestInfo { Id = v.Id, Name = v.Name }).ToCommon());
            }
            return cti;
        }
    }
}
