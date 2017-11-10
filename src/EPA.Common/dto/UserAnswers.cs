using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    /// Describes answer that choose user for question
    /// </summary>
    class UserAnswers
    {
        /// <summary>
        /// ID of question
        /// </summary>
        public int IDQuestion { get; set; }
        /// <summary>
        /// ID of choosen answer
        /// </summary>
        public int IDAnswer { get; set; }
    }
}
