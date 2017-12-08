namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes a question-answer relation for test quizzes
    /// </summary>
    public class UserAnswer
    {
        /// <summary>
        /// Gets or sets id of question
        /// </summary>
        public int IdQuestion { get; set; }

        /// <summary>
        /// Gets or sets id of chosen answer
        /// </summary>
        public int IdAnswer { get; set; }
    }
}
