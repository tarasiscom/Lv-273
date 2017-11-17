using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class DirectionInfo
    {   
        /// <summary>
        ///  ID of the general direction
        /// </summary>
        public int GeneralDirection { get; set; }
        
        /// <summary>
        ///  Number of page
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// District's Id
        /// </summary>
        public int District { get; set; }
    }
}
