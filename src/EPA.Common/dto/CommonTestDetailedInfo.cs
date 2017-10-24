using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public class CommonTestDetailedInfo : CommonTestInfo
    {
        public string Description { get; set; }
        public int ApproximatedTime { get; set;  }
        public int QuestionsCount { get; set; }
    }
}
