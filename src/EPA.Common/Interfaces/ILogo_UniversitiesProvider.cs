﻿using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// Interface that contains methods for calculating scores of test quiz and getting result
    /// </summary>
    public interface ILogo_UniversitiesProvider
    {
        /// <summary>
        /// Calculates scores for each direction depends on user answers
        /// </summary>
        /// <returns>list of directions with scores</returns>
        List<int> GetId();

    }
}