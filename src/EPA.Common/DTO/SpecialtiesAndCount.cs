using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    public class SpecialtiesAndCount
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Specialty> ListSpecialties { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int CountOfAllElements { get; set; }
    }
}
