﻿using System.Collections.Generic;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using System.Linq;

namespace EPA.BusinessLogic
{
    public class UserAnswersProvider : IUserAnswersProdiver
    {
        private readonly ITestProvider testProvider;

        public UserAnswersProvider(ITestProvider testProvider)
        {
            this.testProvider = testProvider;
        }

        public List<DirectionScores> CalculateScores (List<UserAnswer> userAnswers)
        {
            List<DirectionScores> result = new List<DirectionScores>();
            result.AddRange(testProvider.GetDirectionsInfo()
                                        .Select(direction => new DirectionScores()
                                        {
                                            GeneralDir = direction,
                                            Score = 0
                                        }));

            if (userAnswers != null)
            {
                foreach (var answ in userAnswers)
                {
                    if (answ.IdAnswer > 0 && answ.IdAnswer <= result.Count)
                        result[answ.IdAnswer - 1].Score++;
                    else throw new System.ArgumentException("Invalid answer number");
                }
            }
            else throw new System.ArgumentException("Empty user answers");
            return result;
        }
    }
}
