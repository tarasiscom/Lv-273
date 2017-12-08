namespace EPA.Common.DTO
{
    /// <summary>
    ///  Describes detailed information for the tests
    /// </summary>
    public class TestInfo : Test
    {
        /// <summary>
        ///  Gets or sets description for the test
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets approximate amount of time one spends on a test
        /// </summary>
        public int ApproximateTime { get; set; }

        /// <summary>
        ///  Gets or sets an amount of questions that test has
        /// </summary>
        public int QuestionsCount { get; set; }
    }
}
