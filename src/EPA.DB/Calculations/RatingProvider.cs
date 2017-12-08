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
        /// This method calculates specialty rating
        /// </summary>
        /// <param name="universityPosition">university`s rating</param>
        /// <param name="numApplication">Number of all applications</param>
        /// <param name="numEnrolled">Number of enrolled students</param>
        /// <returns>Speciality rating</returns>
        public double GetRating(int universityPosition, int numApplication, int numEnrolled)
        {
            if (numEnrolled != 0)
            {
                return -universityPosition + ((double)numApplication / numEnrolled) + (numApplication * this.koefOfNumApplication);
            }
            else
            {
                return 0;
            }
        }
    }
}
