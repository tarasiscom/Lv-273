using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes question to the test
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets Id of the question
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets text of the question
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets collection of answers related to this question
        /// </summary>
        public List<Answer> Answers { get; set; }
    }
}
