using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.dto
{
    public interface ICommonSpecialty
    {
        int Id { get; set; }
        string Specialty { get; }
        string Faculty { get; }
        string University { get; }
        string District { get; }
        string City { get; }
        string Address { get; }
        string Site { get; }
    }
}
