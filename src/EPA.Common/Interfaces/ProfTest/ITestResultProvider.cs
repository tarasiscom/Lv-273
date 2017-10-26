using EPA.Common.DTO.ProfTest.Quiz;

namespace EPA.Common.Interfaces.ProfTest
{

    /// <summary>
    ///  This interface describes methods that are available through the interface for getting the result of professional test
    /// </summary>
    public interface ITestResultProvider
    {
        /// <summary>
        ///  This method retrives data about persons professional directory and list of specialities
        ///  <param name="points">Amount of points achieved</param>
        ///  <param name="testId">ID of the test, whose results we need</param>
        ///  <returns>  ProfTest's Result </returns>
        /// </summary>
        Result GetResult(int points, int testId);
    }
    
}
