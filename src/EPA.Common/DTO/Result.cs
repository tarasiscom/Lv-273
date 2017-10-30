using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  Represents result for passed test, with profdirection, and specialty recommendations
    /// </summary>
    public class Result
    {
        /// <summary>
        ///  Recommended profdirection
        /// </summary>
        public string ProfDirection { get; set; }

        /// <summary>
        ///  Specialty recommendations, based on profdirection 
        /// </summary>
        public List<Specialty> Specialties { get; set; }
    }
}
