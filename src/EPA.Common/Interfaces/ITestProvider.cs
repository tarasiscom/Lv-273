using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    ///  This interface describes methods that are available for getting test Related Data
    /// </summary>
    public interface ITestProvider
    {
        /// <summary>
        ///  This method retrives list of accessible tests 
        ///  <returns> collection of Tests </returns>
        /// </summary>
        IEnumerable<Test> GetTests();

        /// <summary>
        ///  This method retrives more detailed information about specific Test
        ///  <param> id of the Test </param>
        ///  <returns> more detatiled test Information </returns>
        /// </summary>
        TestInfo GetTestInfo(int id);

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        IEnumerable<Question> GetQuestions(int testId);

        /// <summary>
        ///  This method retrives data about persons professional directory and list of specialities
        ///  <param name="points">Amount of points achieved</param>
        ///  <param name="testId">ID of the test, whose results we need</param>
        ///  <returns>  ProfTest's Result </returns>
        /// </summary>
        Result GetResult(int points, int testId);
    }
}
