namespace Parsing.DataClasses
{
    public class Direction
    {
        public Direction(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}
