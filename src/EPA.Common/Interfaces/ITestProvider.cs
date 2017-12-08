using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    ///  This interface describes methods for getting test related data
    /// </summary>
    public interface ITestProvider
    {
        /// <summary>
        /// This method retrieves collection of all tests
        /// </summary>
        /// <returns> Collection of tests </returns>
        IEnumerable<Test> GetTests();

        /// <summary>
        /// This method retrieves more detailed information about specific test
        /// </summary>
        /// <param name="id"> Id of the test </param>
        /// <returns> More detatiled test information </returns>
        TestInfo GetTestInfo(int id);

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test</param>
        /// <returns>Collection of questions</returns>
        IEnumerable<Question> GetQuestions(int testId);

        /// <summary>
        /// This method returns general directions
        /// </summary>
        /// <returns>Collection of general directions</returns>
        IEnumerable<GeneralDirection> GetDirectionsInfo();

        /// <summary>
        /// This method saves test results for user
        /// </summary>
        /// <param name="list">Test Results</param>
        /// <param name="userId">User's ID</param>
        /// <param name="testId">Test for which results are beign saved</param>
        /// <returns>Logical flag that represents operation status</returns>
        bool AddTestResult(List<DirectionScores> list, string userId, int testId);
    }
}
