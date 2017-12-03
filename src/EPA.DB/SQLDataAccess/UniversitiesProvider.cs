using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class contains methods for obtaining specialties data from database
    /// </summary>
    public class UniversitiesProvider : IUniversitiesProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constValues;

        public UniversitiesProvider(EpaContext context, IOptions<ConstSettings> constValues)
        {
            this.context = context;
            this.constValues = constValues;
        }

        /// <summary>
        /// This method retrieves collection of all subjects from database
        /// </summary>
        /// <returns>Collection of subjects</returns>
        public IEnumerable<Common.DTO.University> GetTopUniversities()
        {
            IQueryable<University> universities = this.context.Universities.OrderBy(x => x.Rating).Take(5).Select(x => x.ToCommon());
            return universities;
        }

        public IEnumerable<byte[]> GetLogoById(int id)
        {
            var data = from i in this.context.Logo_Universities
                       where i.Id == id
                       select i.ToCommon().Logo;
            return data;
        }
    }
}