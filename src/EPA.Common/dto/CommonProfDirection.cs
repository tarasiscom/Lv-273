using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes information about professional direction 
    /// </summary>
    public interface ICommonProfDirection
    {
        int Id { get; }
        string ProfDirection { get; }
    }
}
