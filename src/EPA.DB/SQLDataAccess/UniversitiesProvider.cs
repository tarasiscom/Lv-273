using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class contains methods for obtaining universities data from database
    /// </summary>
    public class UniversitiesProvider : IUniversitiesProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constSettings;

        public UniversitiesProvider(EpaContext context, IOptions<ConstSettings> constSettings)
        {
            this.context = context;
            this.constSettings = constSettings;
        }

        /// <summary>
        /// This method retrieves collection of top universities from database
        /// </summary>
        /// <returns>Collection of top universities</returns>
        public IEnumerable<Common.DTO.University> GetTopUniversities()
        {
            IQueryable<University> universities = this.context.Universities.
                OrderBy(x => x.Rating).
                Take(this.constSettings.Value.TopUniversities).
                Select(x => x.ToCommon());
            return universities;
        }

        /// <summary>
        /// This method retrieves Logo of University by column LogoId in the table Universities
        /// </summary>
        /// <param name="id">Id from table Logo_Universities</param>
        /// <returns>logo of University</returns>
        public IEnumerable<byte[]> GetLogoById(int id)
        {
            var data = from i in this.context.Logo_Universities
                       where i.Id == id
                       select i.ToCommon().Logo;
            return data;
        }
    }
}