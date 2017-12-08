using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// This interface describes methods for calculating scores of test quiz and getting result
    /// </summary>
    public interface IScoreProdiver
    {
        /// <summary>
        /// Calculates scores for all directions depending on user answers
        /// </summary>
        /// <param name="userAnswers"> Collection of questions and answers that user chose </param>
        /// <returns> Collection of directions with scores</returns>
        List<DirectionScores> CalculateScores(List<UserAnswer> userAnswers);
    }
}
