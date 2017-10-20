using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public interface ICommonTestDetailedInfo : ICommonTestInfo
    {
        string Description { get;}
        int ApproximatedTime { get;}
        int QuestionsCount { get; }
    }
}
