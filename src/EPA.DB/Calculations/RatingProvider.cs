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
        private readonly double koefOfNumApplication;

        public RatingProvider(double koefOfNumApplication)
        {
            this.koefOfNumApplication = koefOfNumApplication;
        }

        /// <summary>
        /// This method  calculating speciality rating
        /// </summary>
        /// <param name="universityPosition">Position in university`s rating</param>
        /// <param name="numApplication">Number of all entrants applications</param>
        /// <param name="numEnrolled">Number of enrolled students</param>
        /// <returns>Speciality rating</returns>
        public double GetRating(int universityPosition, int numApplication, int numEnrolled)
        {
            if (numEnrolled != 0)
            {
                return -universityPosition + (double)numApplication / numEnrolled + numApplication * koefOfNumApplication;
            }
            else
            {
                return 0;
            }
        }
    }
}
