using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dto.CommonQuiz;

namespace EPA.Common.Interfaces
{
    public interface IAccessToQuestionsByNameTest
    {
        IEnumerable<CommonQuestions> GetQuestionByListID(int testId);

        IEnumerable<CommonAnswers> GetAnswersByQuestId(int questionId);
    }
}
