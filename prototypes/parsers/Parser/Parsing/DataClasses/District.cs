namespace Parsing.DataClasses
{
    public class District
    {
        public District(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; private set; }

        public string Name { get; private set; }
    }
}
