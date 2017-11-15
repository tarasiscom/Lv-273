using System;

namespace EPA.MSSQL.Calculations
{
    public class RatingProvider
    {
        public static double GetRating(int numApplication, int numEnrolled)
        {
            const double koefOfNumApplication = 0.01;
            try
            {
                return (double)numApplication / numEnrolled + numApplication * koefOfNumApplication;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }
    }
}
