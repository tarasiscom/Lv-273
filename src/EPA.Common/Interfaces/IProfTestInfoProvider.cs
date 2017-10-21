using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dto;

namespace EPA.Common.Interfaces
{
    public interface IProfTestInfoProvider
    {
        // ICommonTestInfo GetTests();
        IEnumerable<CommonTestInfo> GetTests();
        CommonTestDetailedInfo GetTestInfo(int id);
    }
}
