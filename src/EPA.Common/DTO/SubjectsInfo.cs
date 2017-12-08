using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes subject data draws that are related to specialties
    /// </summary>
    public class SubjectsInfo
    {
        /// <summary>
        /// Gets or sets list of subjects
        /// </summary>
        public List<int> ListSubjects { get; set; }

        /// <summary>
        /// Gets or sets page at which user is currently on
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets district
        /// </summary>
        public int District { get; set; }
    }
}