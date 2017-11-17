namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes information about count of all elements for pagiantion and count of elements for one page 
    /// </summary>
    public class Count
    {
        /// <summary>
        /// Count of all elements for request 
        /// </summary>
        public int AllElements { get; set; }

        /// <summary>
        /// Count of elements for one page 
        /// </summary>
        public int ForOnePage { get; set; }
    }
}
