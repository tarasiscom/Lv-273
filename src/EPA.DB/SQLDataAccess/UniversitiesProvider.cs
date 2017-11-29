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
    public class UniversitiesProvider : IUniversitiesProdiver
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
        public IEnumerable<Common.DTO.UniversityInfo> GetTopUniversities()
        {
            IQueryable<UniversityInfo> universities = this.context.Universities.OrderBy(x => x.Rating).Take(5).Select(x => x.ToCommon());
            return universities;
        }

        //public byte[] imageToByteArray(Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    return ms.ToArray();
        //}

        //public Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}
    }
}