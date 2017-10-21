using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes ProfTest structure
    /// </summary>
    public interface ICommonTestDetailedInfo : ICommonTestInfo
    {
        string Description { get;}
        int ApproximatedTime { get;}
        int QuestionsCount { get; }
    }
}
