namespace Parsing.DataClasses
{
    public class Speciality
    {
        public Speciality(int id, int directionID, int universityID, string name)
        {
            this.ID = id;
            this.DirectionID = directionID;
            this.UniversityID = universityID;
            this.Name = name;
 
        }

        public int ID { get; set; }

        public int UniversityID { get; set; }

        public int DirectionID { get; set; }

        public string Name { get; set; }

        //public string MinPoints { get; set; }

        //public string AvgPoints { get; set; }

        //public int BudgetPlaces { get; set; }

        //public int ContractPlaces { get; set; }
    }
}
