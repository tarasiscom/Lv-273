namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes information that need for request to DB - GetSpecialtiesByDirection
    /// </summary>
    public class DirectionInfo
    {
        /// <summary>
        /// ID of the general direction
        /// </summary>
        public int GeneralDirection { get; set; }

        /// <summary>
        ///  Number of page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// District's Id
        /// </summary>
        public int District { get; set; }
    }
}
