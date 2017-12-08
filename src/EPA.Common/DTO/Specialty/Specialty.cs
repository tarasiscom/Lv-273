using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes detailed information about specialty
    /// </summary>
    public class Specialty
    {
        /// <summary>
        ///  Gets or sets id of the specialty
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets name of the specialty
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets name of the university in which this specialty is taught
        /// </summary>
        public string University { get; set; }

        /// <summary>
        ///  Gets or sets name of the district in which the university is placed
        /// </summary>
        public string District { get; set; }

        /// <summary>
        ///  Gets or sets address of the university
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///  Gets or sets site of the university
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether checks if this specialty is favorite for current user
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        ///  Gets or sets subjects that are needed for passing ZNO
        /// </summary>
        public List<Subject> Subjects { get; set; }
    }
}
