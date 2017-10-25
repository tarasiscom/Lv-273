using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    public interface ILastSyncProvider
    {
        CommonDate GetDate();
    }
}