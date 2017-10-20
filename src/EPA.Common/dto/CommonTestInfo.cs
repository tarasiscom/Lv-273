using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    /// <summary>
    ///  This interface describes general information of ProfTest
    /// </summary>
    public interface ICommonTestInfo
    {
        int Id { get; }
        string Name { get; }
    }
}
