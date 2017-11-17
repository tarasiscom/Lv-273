﻿using System.Collections.Generic;
using EPA.Common.DTO;
using System.Linq;
using EPA.Common.Interfaces;

namespace EPA.BusinessLogic
{
    public class AnswersProvider : IAnswersProdiver
    {
        private readonly ITestProvider testProvider;

        public AnswersProvider(ITestProvider testProvider)
        {
            this.testProvider = testProvider;
        }

        public List<DirectionScores> CalculateScores(List<UserAnswer> userAnswers)
        {
            List<DirectionScores> result = new List<DirectionScores>();
            result.AddRange(this.testProvider.GetDirectionsInfo()
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
                    {
                        result[answ.IdAnswer - 1].Score++;
                    }
                    else
                    {
                        throw new System.ArgumentException("Invalid answer number");
                    }
                }
            }
            else
            {
                throw new System.ArgumentException("Empty user answers");
            }

            return result;
        }
    }
}
