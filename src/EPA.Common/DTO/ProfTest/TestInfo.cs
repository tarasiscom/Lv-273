using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO.ProfTest
{
    /// <summary>
    ///  Describes more detailed information for the tests
    /// </summary>
    public class TestInfo : Test
    {
        /// <summary>
        ///  Description for the test
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  Approximated amount of time one spends on passing a test
        /// </summary>
        public int ApproximatedTime { get; set; }
        /// <summary>
        ///  Ammount of questions in the test
        /// </summary>
        public int QuestionsCount { get; set; }
    }
}
