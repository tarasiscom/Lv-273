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
        public ICommonTestDetailedInfo GetTestInfo(int testId)
        {
            return context.Tests.Find(testId);
        }
        public IEnumerable<ICommonTestInfo> GetTests()
        {
            List<ICommonTestInfo> cti = new List<ICommonTestInfo>();
            foreach (var v in context.Tests)
            {
                cti.Add(new TestInfo { Id = v.Id, Name = v.Name });
            }
            return cti;
        }
    }
}
