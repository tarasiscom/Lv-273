using System;
using Microsoft.Extensions.Options;
using EPA.Common.Interfaces;

namespace EPA.MSSQL.Calculations
{
    /// <summary>
    /// This class contains methods for calculating specialities rating
    /// </summary>
    public class RatingProvider
    {
        private static IOptions<ConstSettings> constValues;
        private const double KoefOfNumApplication = 0.01;

        public RatingProvider(IOptions<ConstSettings> constSettings)
        {
                        constValues = constSettings;
        }

        /// <summary>
        /// This method  calculating speciality rating
        /// </summary>
        /// <param name="numApplication">Amount of all entrants applications</param>
        /// <param name="numEnrolled">Amount of enrolled students</param>
        /// <returns>Speciality rating</returns>
        public static double GetRating(int numApplication, int numEnrolled)
        {
            if(numEnrolled != 0)
            { 
                return (double)numApplication / numEnrolled + numApplication * KoefOfNumApplication;
            }
            else
            { 
                return 0;
            }
        }
    }
}
