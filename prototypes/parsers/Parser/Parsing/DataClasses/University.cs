namespace Parsing.DataClasses
{
    public class University
    {
        public University(int id, int districtID, string name,  string adress, string site)
        {
            this.ID = id;
            this.DistrictID = districtID;
            this.Name = name;
            this.Adress = adress;
            this.Site = site;
        }

        public int ID { get; }

        public int DistrictID { get; }

        public string District { get; }

        public string City { get; }

        public string Name { get; }

        public string Adress { get;}

        public string Site { get; }

        public string Points { get; }
    }
}
