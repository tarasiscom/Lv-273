using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// This interface describes methods that returns data for choose specialty feature
    /// </summary>
    public interface ISpecialtyProvider
    {
        IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection);
    }
}
