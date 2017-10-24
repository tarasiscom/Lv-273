using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dto.CommonQuiz;

namespace EPA.Common.Interfaces
{
    public interface IAccessToQuestionsByNameTest
    {
        /// <summary>
        /// This method returns a list of questions by testId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IEnumerable<CommonQuestions> GetQuestionByListID(int testId);

        /// <summary>
        /// This method returns a list of answers by questionId
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        IEnumerable<CommonAnswers> GetAnswersByQuestId(int questionId);
    }
}
