using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes information about university
    /// </summary>
    public class University
    {
        /// <summary>
        ///  Gets or sets Id of the university
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets name of the university
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets district of the university
        /// </summary>
        public District District { get; set; }

        /// <summary>
        ///  Gets or sets address of the university
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///  Gets or sets site of the university
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        ///  Gets or sets rating of the university
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        ///  Gets or sets logo of the university
        /// </summary>
        public int? LogoId { get; set; }
    }
}
