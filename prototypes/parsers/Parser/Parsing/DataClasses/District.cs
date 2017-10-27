namespace Parsing.DataClasses
{
    public class District
    {
        public District(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}
