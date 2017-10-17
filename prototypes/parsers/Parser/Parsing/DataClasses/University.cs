namespace Parsing.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class University
    {
        public University(int id, int dictrictID, string name, string adress, string site)
        {
            this.ID = id;
            this.DistrictID = DistrictID;
            this.Name = name;
            this.Adress = adress;
            this.Site = site;
        }

        public int ID { get; set; }

        public int DistrictID { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Site { get; set; }

        public string Points { get; set; }
    }
}
