using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.DTO
{
    public class ChangePassword
    {
        /// <summary>
        /// Property which contain old user password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Property which contain new user password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
