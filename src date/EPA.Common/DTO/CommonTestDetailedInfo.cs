using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    public class CommonTestDetailedInfo :CommonTestInfo
    {
        public virtual string Description { get; set; }
        public virtual int ApproximatedTime { get; set; }
        public virtual int QuestionsCount { get; set; } 
    }
}
