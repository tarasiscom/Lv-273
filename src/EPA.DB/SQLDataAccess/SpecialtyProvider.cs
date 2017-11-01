using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPA.MSSQL.Models;
using EPA.Common.Interfaces;

namespace EPA.MSSQL.SQLDataAccess
{
    public class SpecialtyProvider : ISpecialtyProvider
    {
        private readonly EpaContext context;

        public SpecialtyProvider(EpaContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirection(int idDirection)
        {
            var y = this.context.Specialties.Where(x => x.Direction.GeneralDirection.Id == idDirection).Select(x => x.ToCommon());
            return y;
        }

    }
}
