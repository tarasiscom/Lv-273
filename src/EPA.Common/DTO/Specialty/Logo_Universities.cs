using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes university's logo
    /// </summary>
    public class Logo_Universities
    {
        public Logo_Universities(int id, byte[] logo)
        {
            this.Id = id;
            this.Logo = logo;
        }

        public Logo_Universities()
        {
        }

        /// <summary>
        ///  Gets or sets Id of the university
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets logo of the university
        /// </summary>
        public byte[] Logo { get; set; }
    }
}
