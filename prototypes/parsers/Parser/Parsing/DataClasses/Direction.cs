namespace Parsing.DataClasses
{
    class Direction
    {
        public Direction(int id, int idUniv, string name)
        {
            this.ID = id;
            this.UniversityID = idUniv;
            this.Name = name;
        }

        public int ID { get; set; }

        public int UniversityID { get; set; }

        public string Name { get; set; }
    }
}
