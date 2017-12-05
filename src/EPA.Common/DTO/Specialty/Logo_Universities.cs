using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes university
    /// </summary>
    public class Logo_Universities
    {
           /// <summary>
        ///  Name of the university
        /// </summary>
        public int Id { get; set; }

        public byte[] Logo { get; set; }

        public Logo_Universities(int id, byte[] logo)
        {
            this.Id = id;
            this.Logo = logo;
        }
        public Logo_Universities() { }
    }
}
