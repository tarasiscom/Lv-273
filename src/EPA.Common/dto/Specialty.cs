using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes general information of speciality
    /// </summary>
    public class Specialty
    {
        /// <summary>
        ///  Name of the specialty
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Name of the university in which this specialty is taught
        /// </summary>
        public string University { get; set; }

        /// <summary>
        ///  Name of the district in which the university is placed
        /// </summary>
        public string District { get; set; }

        /// <summary>
        ///  Address of the university
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///  Site of the university
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        ///  Subjects that are needed for ZNO 
        /// </summary>
        public List<Subject> Subjects { get; set; }
    }
}
