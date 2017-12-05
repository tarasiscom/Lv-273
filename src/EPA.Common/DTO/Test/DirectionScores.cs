namespace EPA.Common.DTO
{
    /// <summary>
    /// Represent score of direction based on user answers
    /// </summary>
    public class DirectionScores
    {
        /// <summary>
        ///  Describes general directions 
        /// </summary>
        public GeneralDirection GeneralDir { get; set; }

        /// <summary>
        /// Score of direction
        /// </summary>
        public int Score { get; set; }
    }
}
