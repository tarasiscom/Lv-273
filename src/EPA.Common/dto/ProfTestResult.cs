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
        ProfDirection ProfDirection { get; set; }
        List<Specialty> Profspecialties { get; set; }
    }
}
