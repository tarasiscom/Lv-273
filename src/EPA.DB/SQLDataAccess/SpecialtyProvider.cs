using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    class SpecialtyProvider:ISpecialtyProvider
    {
        private readonly EpaContext context;

        public SpecialtyProvider(EpaContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<Common.DTO.Specialty> GetSpecialtyBySubjects(List<Common.DTO.Subject> listOfSubjects)
        {
            throw new System.NotImplementedException();
        }
    }
}
