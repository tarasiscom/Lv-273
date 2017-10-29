﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes general information of Speciality
    /// </summary>
    public class Specialty
    {
        /// <summary>
        ///  Name of the Specialty
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
    }
}
