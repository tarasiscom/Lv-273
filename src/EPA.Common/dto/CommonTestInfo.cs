using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public interface CommonTestInfo
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
/*
namespace EPA.Common.dto
{
    public class CommonTestInfo
    {
        public virtual int Id { get; set; }
        public virtual string  Name { get; set; }
    }
}
*/
