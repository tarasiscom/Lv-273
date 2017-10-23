using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto.CommonQuiz
{
    public class CommonQuestions
    {
        public int ID { get; set; }
        public string Question { get; set; }

        public List<CommonAnswers> Answer { get; set; }
        public CommonTestList TestListID { get; set; }
    }
}
