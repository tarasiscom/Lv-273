using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes general information about professional directory, and list with specialties for person
    /// </summary>
    public interface ICommonProfTestResult 
    {
        ICommonProfDirection ProfDirection { get;}
        List<ICommonSpecialty> Profspecialties { get; }
    }
}
