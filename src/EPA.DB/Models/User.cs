using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace EPA.MSSQL.Models
{
    public class User: IdentityUser
    {
        public List<User_Specialty> UserSpecialty { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public List<District> District { get; set; }
    }
}
