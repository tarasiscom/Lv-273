using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public interface CommonTestDetailedInfo : CommonTestInfo
    {
        string Description { get; set; }
        int ApproximatedTime { get; set; }
        int QuestionsCount { get; set; }
    }
}
/*
namespace EPA.Common.dto
{
    public class CommonTestDetailedInfo:CommonTestInfo
    {
        public virtual string Description { get; set; }
        public virtual int ApproximatedTime { get; set; }
        public virtual int QuestionsCount { get; set; }
    }
}
*/
