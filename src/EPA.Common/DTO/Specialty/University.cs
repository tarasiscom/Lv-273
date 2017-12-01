using System.Collections.Generic;

namespace EPA.Common.DTO
{
    public class University
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public District District { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public int Rating { get; set; }

        //public byte[] Logo { get; set; }

        public Logo_Universities Logo_Universities { get; set; }
    }
}
