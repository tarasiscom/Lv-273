using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes general information of Speciality
    /// </summary>
    public class Specialty
    {
        int Id { get; set; }
        string SpecialtyName { get; set; }
        string Faculty { get; set; }
        string University { get; set; }
        string District { get; set; }
        string City { get; set; }
        string Address { get; set; }
        string Site { get; set; }
    }
}
