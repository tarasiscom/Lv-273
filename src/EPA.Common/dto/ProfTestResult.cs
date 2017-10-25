using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This class describes general information about professional directory, and list with specialties for person
    /// </summary>
    public class ProfTestResult 
    {
        public string ProfDirection { get; set; }
        public List<Specialty> ProfSpecialties { get; set; }
    }
}
