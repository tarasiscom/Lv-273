using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace EPA.MSSQL.Models
{
    public class User : IdentityUser
    {
        public List<User_Specialty> UserSpecialty { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public List<TestResult> TestResult { get; set; }

        public EPA.Common.DTO.UserProvider.UserPersonalInfo ToPersonalInfo()
        {
            return Mapper.Map<Common.DTO.UserProvider.UserPersonalInfo>(this);
        }
    }
}
