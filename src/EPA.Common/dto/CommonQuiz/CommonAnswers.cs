using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto.CommonQuiz
{
    public class CommonAnswers
    {
        public int ID { get; set; }
        public string Answer { get; set; }
        public int Point { get; set; }
        public CommonQuestions Question { get; set; }
    }
}
