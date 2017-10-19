using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public interface ICommonTestDetailedInfo : ICommonTestInfo
    {
        string Description { get; set; }
        int ApproximatedTime { get; set; }
        int QuestionsCount { get; set; }
    }
}
