using System;
using Microsoft.Extensions.Options;
using EPA.Common.Interfaces;

namespace EPA.MSSQL.Calculations
{
    public class RatingProvider
    {
        private static IOptions<ConstSettings> constValues;
        private const double KoefOfNumApplication = 0.01;

        public RatingProvider(IOptions<ConstSettings> constSettings)
        {
                        constValues = constSettings;
        }

        /// <summary>
        /// This method retrieves rating of Specialty
        /// </summary>
        public static double GetRating(int numApplication, int numEnrolled)
        {
            try
            {
                return (double)numApplication / numEnrolled + numApplication * KoefOfNumApplication;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }
    }
}
