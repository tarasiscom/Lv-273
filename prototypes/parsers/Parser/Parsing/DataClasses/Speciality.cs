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

        public int ID { get; }

        public int UniversityID { get; }

        public int DirectionID { get; }

        public string Name { get; }
    }
}
