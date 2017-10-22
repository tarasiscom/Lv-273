using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes general information of ProfTest
    /// </summary>
    public interface ICommonProfDirection
    {
        int Id { get; }
        string Profdirection { get; }
    }
}
