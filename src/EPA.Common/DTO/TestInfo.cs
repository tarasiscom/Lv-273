using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
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
        ///  Approximate amount of time one spends on passing a test
        /// </summary>
        public int ApproximateTime { get; set; }

        /// <summary>
        ///  Amount of questions in the test
        /// </summary>
        public int QuestionsCount { get; set; }
    }
}
