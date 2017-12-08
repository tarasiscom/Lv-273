﻿using Parsing.DataClasses;
using System.Collections.Generic;

namespace Parsing
{
    public interface ISaver
    {
        void SaveAll(List<University> universities, List<Direction> directions, List<Speciality> specialities, List<Subject> subjects, List<SpecSub> specsubs, List<District> districts);
    }
}
