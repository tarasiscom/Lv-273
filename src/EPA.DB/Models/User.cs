using Microsoft.AspNetCore.Identity;

namespace EPA.MSSQL.Models
{
    public class User : IdentityUser
    {
        public List<User_Specialty> UserSpecialty { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public District District { get; set; }
    }
}
