namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes score of direction based on user answers
    /// </summary>
    public class DirectionScores
    {
        /// <summary>
        ///  Gets or sets general direction
        /// </summary>
        public GeneralDirection GeneralDir { get; set; }

        /// <summary>
        /// Gets or sets score for the direction
        /// </summary>
        public int Score { get; set; }
    }
}
