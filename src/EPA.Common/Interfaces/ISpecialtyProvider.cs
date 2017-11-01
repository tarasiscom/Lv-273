using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    public interface ISpecialtyProvider
    {
        IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection);
    }
}
