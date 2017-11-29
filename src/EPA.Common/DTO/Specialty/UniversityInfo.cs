using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes university
    /// </summary>
    public class UniversityInfo
    {
           /// <summary>
        ///  Name of the university
        /// </summary>
        public string Name { get; set; }

        public string Site { get; set; }

        public byte[] Logo { get; set; }
    }
}
