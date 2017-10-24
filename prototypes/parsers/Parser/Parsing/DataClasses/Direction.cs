namespace Parsing.DataClasses
{
    public class Direction
    {
        public Direction(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; set; }

        public string Name { get; set; }
    }
}
