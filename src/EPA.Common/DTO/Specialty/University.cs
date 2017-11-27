using System.Collections.Generic;

namespace EPA.Common.DTO
{
    /// <summary>
    ///  This class describes university
    /// </summary>
    public class University
    {
        /// <summary>
        ///  ID of the university
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Name of the university
        /// </summary>
        public string Name { get; set; }

        public District District { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public int Rating { get; set; }

        public List<Specialty> Specialties { get; set; }

        public byte[] Logo { get; set; }
    }
}
