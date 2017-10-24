namespace Parsing.DataClasses
{
    public class University
    {
        public University(int id, string district, string name,  string adress, string site)
        {
            this.ID = id;
            //this.DistrictID = DistrictID;
            this.District = district;
            this.Name = name;
           // this.City = city;
            this.Adress = adress;
            this.Site = site;
        }

        public int ID { get; set; }

        public int DistrictID { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string Site { get; set; }

        public string Points { get; set; }
    }
}
