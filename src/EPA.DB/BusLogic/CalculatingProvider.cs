using System;

namespace EPA.MSSQL.BusLogic
{
    public class CalculatingProvider
    {
        public static double GetRating(int NumApplication, int NumEnrolled)
        {
            const double koefApp = 0.01;
            try
            {
                return NumApplication / (double)NumEnrolled + NumApplication * koefApp;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }
    }
}
