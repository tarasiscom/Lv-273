using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  Represents question to test
    /// </summary>
    public class Question
    {

        /// <summary>
        /// ID of the question
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Text of the question
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Collection of answers related to this question
        /// </summary>
        public List<Answer> Answers { get; set; }
        /// <summary>
        /// Refference to the test
        /// </summary>
        public TestInfo TestListID { get; set; }
    }
}
