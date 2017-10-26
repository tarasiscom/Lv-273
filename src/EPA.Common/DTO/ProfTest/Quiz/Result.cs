using System.Collections.Generic;

namespace EPA.Common.DTO.ProfTest.Quiz
{
    /// <summary>
    ///  Represents result for passed test, with profdirection, and specialties recommendations
    /// </summary>
    public class Result
    {
        /// <summary>
        ///  ProfDirection which is recommended
        /// </summary>
        public string ProfDirection { get; set; }
        /// <summary>
        ///  Specialties recommendations, based on profDirection 
        /// </summary>
        public List<Specialty> Specialties { get; set; }
    }
}
