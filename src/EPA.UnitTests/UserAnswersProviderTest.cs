/*using EPA.BusinessLogic;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace EPA.UnitTests
{
    public class UserAnswersProviderTest
    {
        /*
        private List<UserAnswer> userAnswers;
        private readonly List<GeneralDirection> directions;

        public UserAnswersProviderTest()
        {
            this.userAnswers = new List<UserAnswer>()
            {
                new UserAnswer{ IdQuestion = 1, IdAnswer = 2 },
                new UserAnswer{ IdQuestion = 2, IdAnswer = 3 },
                new UserAnswer{ IdQuestion = 3, IdAnswer = 3 }
            };

            this.directions = new List<GeneralDirection>()
            {
                new GeneralDirection{ ID = 1, Name= "Соціологія та педагогіка"},
                new GeneralDirection{ ID = 2, Name= "Гуманітарні науки"},
                new GeneralDirection{ ID = 3, Name= "Природничі науки"},
                new GeneralDirection{ ID = 4, Name= "Прикладні науки"},
                new GeneralDirection{ ID = 5, Name= "Медицина"},
                new GeneralDirection{ ID = 6, Name= "Патріотична сфера"}
            };
        }

        [Fact]
        public void EmptyAnswersTest()
        {
            this.userAnswers = null;

            var testProvider = new Mock<ITestProvider>();
            testProvider.Setup(pr => pr.GetDirectionsInfo()).Returns(this.directions);
            UserAnswersProvider userAnswersProvider = new UserAnswersProvider(testProvider.Object);

            System.Exception ex = Assert.Throws<System.ArgumentException>(
                                                    () => userAnswersProvider.CalculateScores(this.userAnswers));

            Assert.Equal("Empty user answers", ex.Message);
        }

        [Fact]
        public void CorrectAnswersTest()
        {
            var testProvider = new Mock<ITestProvider>();
            testProvider.Setup(pr => pr.GetDirectionsInfo()).Returns(this.directions);
            UserAnswersProvider userAnswersProvider = new UserAnswersProvider(testProvider.Object);

            List<DirectionScores> res = userAnswersProvider.CalculateScores(this.userAnswers);

            Assert.Equal(6, res.Count);
            Assert.Equal(2, res.FindAll(x => x.Score > 0).Count);
        }

        [Fact]
        public void WrongAnswersTest()
        {
            this.userAnswers[0].IdAnswer = -1;
            this.userAnswers[2].IdAnswer = 100;

            var testProvider = new Mock<ITestProvider>();
            testProvider.Setup(pr => pr.GetDirectionsInfo()).Returns(this.directions);
            UserAnswersProvider userAnswersProvider = new UserAnswersProvider(testProvider.Object);

            System.Exception ex = Assert.Throws<System.ArgumentException>(
                                                    () => userAnswersProvider.CalculateScores(this.userAnswers));

            Assert.Equal("Invalid answer number", ex.Message);
        }
    }
}
*/