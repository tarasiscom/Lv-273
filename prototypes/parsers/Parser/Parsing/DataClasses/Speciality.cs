namespace Parsing.DataClasses
{
    public class Speciality
    {
        public Speciality(int id, int directionID, int universityID, string name, int applicationsAmount, int enrolledAmount)
        {
            this.ID = id;
            this.DirectionID = directionID;
            this.UniversityID = universityID;
            this.Name = name;
            this.ApplicationsAmount = applicationsAmount;
            this.EnrolledAmount = enrolledAmount;
        }

        public int ID { get; }

        public int UniversityID { get; }

        public int DirectionID { get; }

        public string Name { get; }

        public int ApplicationsAmount { get; }

        public int EnrolledAmount { get;  }
    }
}
