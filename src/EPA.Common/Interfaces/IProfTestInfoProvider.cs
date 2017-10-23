using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.dto;

namespace EPA.Common.Interfaces
{
    /// <summary>
    ///  This interface describes methods that are available through the interface for getting the ProfTest data
    /// </summary>
    public interface IProfTestInfoProvider
    {
        /// <summary>
        ///  This method retrives list of accessible ProfTests 
        ///  <returns> collection of ProfTests </returns>
        /// </summary>
        // ICommonTestInfo GetTests();

        /// <summary>
        ///  This method retrives information about current ProfTest
        ///  <param> id of ProfTest </param>
        ///  <returns> ProfTest info </returns>
        /// </summary>
        CommonTestDetailedInfo GetTestInfo(int id);
    }
}
