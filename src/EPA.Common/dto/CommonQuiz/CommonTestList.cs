using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto.CommonQuiz
{
    public class CommonTestList
    {
        public int ID { get; set; }
        public string TestName { get; set; }
        public virtual List<CommonQuestions> Questions { get; set; }
    }
}
