using System.Collections.Generic;
using EPA.Common.DTO.ProfTest.Quiz;

namespace EPA.Common.Interfaces.ProfTest
{
    /// <summary>
    ///  This interface describes methods that are available for getting questions and answers to the quiz
    /// </summary>
    public interface ITestQuizProvider
    {
        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        IEnumerable<Question> GetQuestions(int testId);

        /// <summary>
        /// This method returns a collection of answers for a specific question
        /// </summary>
        /// <param name="questionId">ID of the question, whose answers we need</param>
        /// <returns>Collection of answers</returns>
        IEnumerable<Answer> GetAnswers(int questionId);
    }
}
