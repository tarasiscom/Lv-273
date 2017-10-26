using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO.ProfTest
{
    /// <summary>
    ///  Describes poor information for the test(needed for showing test lists)
    /// </summary>
    public class Test
    {
        /// <summary>
        ///  ID of the test
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  Name of the test
        /// </summary>
        public string Name { get; set; }
    }
}
