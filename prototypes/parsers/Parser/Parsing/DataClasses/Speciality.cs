namespace Parsing.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Speciality
    {
        public Speciality()
        {
 
        }

        public int ID { get; set; }

        public int FacultyID { get; set; }

        public int DirectionID { get; set; }

        public string Name { get; set; }

        public string MinPoints { get; set; }

        public string AvgPoints { get; set; }

        public int BudgetPlaces { get; set; }

        public int ContractPlaces { get; set; }
    }
}
