namespace Parsing.DataClasses
{
    class Speciality
    {
        public Speciality(int id, int facID, string name)
        {
            this.ID = id;
            this.FacultyID = facID;
            this.Name = name;
 
        }

        public int ID { get; set; }

        public int FacultyID { get; set; }

        //public int DirectionID { get; set; }

        public string Name { get; set; }

        //public string MinPoints { get; set; }

        //public string AvgPoints { get; set; }

        //public int BudgetPlaces { get; set; }

        //public int ContractPlaces { get; set; }
    }
}
