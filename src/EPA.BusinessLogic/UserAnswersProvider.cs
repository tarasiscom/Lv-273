using System.Collections.Generic;
using EPA.Common.DTO;
using EPA.Common.Interfaces;

namespace EPA.BusinessLogic
{
    public class UserAnswersProvider : IUserAnswersProdiver
    {
        private List<Direction_Score> result;
        private readonly ITestProvider testProvider;

        public UserAnswersProvider(ITestProvider testProvider)
        {
            this.testProvider = testProvider;
        }

        public List<Direction_Score> CalculateScores (List<EPA.Common.DTO.UserAnswer> userAnswers)
        {
            result = new List<Direction_Score>();
            foreach(var answ in userAnswers)
            {
                var direction = result.Find(res => res.GeneralDir.ID == answ.IdAnswer);
                if (direction != null)
                {
                    direction.Score++;
                }
                else
                {
                    result.Add(new Direction_Score {
                                          GeneralDir = testProvider.GetDirectionInfo(answ.IdAnswer),
                                          Score = 1 });

                }
            }
            return result;
        }
    }
}
