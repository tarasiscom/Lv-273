using Microsoft.AspNetCore.Identity;

namespace EPA.MSSQL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
