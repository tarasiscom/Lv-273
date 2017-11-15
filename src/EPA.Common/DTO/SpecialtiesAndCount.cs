using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    public class SpecialtiesAndCount
    {
        /// <summary>
        /// Limited list of specialties
        /// </summary>
        public IEnumerable<Specialty> ListSpecialties { get; set; }

        /// <summary>
        /// Count of all specialities
        /// </summary>
        public int CountOfAllElements { get; set; }
    }
}
