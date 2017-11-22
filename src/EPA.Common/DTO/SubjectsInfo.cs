using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes information that need for request to DB - GetSpecialtiesBySubjects
    /// </summary>
    public class SubjectsInfo
    {
        /// <summary>
        /// List of Subjects
        /// </summary>
        public List<int> ListSubjects { get; set; }

        /// <summary>
        /// Pagination page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///District Id 
        /// </summary>
        public int District { get; set; }
    }
}