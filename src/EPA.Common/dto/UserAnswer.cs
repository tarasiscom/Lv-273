namespace EPA.Common.DTO
{
    /// <summary>
    /// Represent question answer what user choose
    /// </summary>
    public class UserAnswer
    {
        /// <summary>
        /// Id of question
        /// </summary>
        public int IdQuestion { get; set; }

        /// <summary>
        /// Id of chosen answer
        /// </summary>
        public int IdAnswer { get; set; }
    }
}
