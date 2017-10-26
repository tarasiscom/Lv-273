using System.Collections.Generic;
using EPA.Common.DTO.ProfTest;

namespace EPA.Common.Interfaces.ProfTest
{
    /// <summary>
    ///  This interface describes methods that are available for getting the ProfTest data
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
    }
}
