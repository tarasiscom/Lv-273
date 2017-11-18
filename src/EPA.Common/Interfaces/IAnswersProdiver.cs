using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// Interface that contains methods for calculating scores of test quiz and getting result
    /// </summary>
    public interface IAnswersProdiver
    {
        /// <summary>
        /// Calculates scores for each direction depends on user answers
        /// </summary>
        /// <param name="userAnswers"> pair of question and answer that user chose </param>
        /// <returns>list of directions with scores</returns>
        List<DirectionScores> CalculateScores(List<UserAnswer> userAnswers);
    }
}
