namespace EPA.Common.DTO
{
    /// <summary>
    /// Represents answer to the question
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// ID of the answer
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///  Text of the answer
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  Amount of points for the answer
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        ///  Reference to the question this answer is for
        /// </summary>
        public Question Question { get; set; }
    }
}
