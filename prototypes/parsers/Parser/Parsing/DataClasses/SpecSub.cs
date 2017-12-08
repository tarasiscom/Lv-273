using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsing.DataClasses
{
    public class SpecSub
    {
        public SpecSub(int id, int specialityId, int subjectId)
        {
            this.ID = id;
            this.SpecialityId = specialityId;
            this.SubjectId = subjectId;
        }

        public int ID { get; }

        public int SpecialityId { get; }

        public int SubjectId { get; }
    }
}
