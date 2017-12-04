using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EPA.MSSQL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
