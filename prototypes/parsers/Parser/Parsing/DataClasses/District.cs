namespace Parsing.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text; 

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
