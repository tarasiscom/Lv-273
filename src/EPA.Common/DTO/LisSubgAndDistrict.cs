using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes information that need for request to DB - GetSpecialtiesBySubjects
    /// </summary>
    public class ListSubjectsAndDistrict
    {
        /// <summary>
        ///District Id 
        /// </summary>
        public int District { get; set; }
        
        /// <summary>
        /// List of Subjects
        /// </summary>
        public List<int> ListSubjects { get; set; }
        
        /// <summary>
        /// Count of elements that will be output 
        /// </summary>
        public int countOfElementsOnPage { get; set; }
        
        /// <summary>
        /// Pagination page
        /// </summary>
        public int page { get; set; }
    }
}
