namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes information about count of all elements for pagiantion and count of elements for one page
    /// </summary>
    public class Count
    {
        /// <summary>
        /// Gets or sets count of all elements in request
        /// </summary>
        public int AllElements { get; set; }

        /// <summary>
        /// Gets or sets count of elements that should be displayed at a single page
        /// </summary>
        public int ForOnePage { get; set; }
    }
}
