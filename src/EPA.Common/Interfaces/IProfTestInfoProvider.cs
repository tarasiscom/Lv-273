using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// Interface gets data from a database 
    /// is implemented in EPA.DB
    /// </summary>
    public interface IProfTestInfoProvider 
    {
        IEnumerable<CommonTestInfo> GetTests();
        CommonTestDetailedInfo GetTestInfo(int testId);
    }
}
