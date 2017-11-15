using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    /// <summary>
    /// This class contains information about list of Specialties that retrives from DB and it count
    /// </summary>
    public class Specialties
    {
        /// <summary>
        /// List of Specialties
        /// </summary>
        public IEnumerable<Specialty> List { get; set; }
        
        /// <summary>
        /// Count of Elements in list if Specialties
        /// </summary>
        public int Count { get; set; }
    }
}
