﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// An interface to note the time
    /// </summary>
    interface ITimeCheck
    {
        /// <summary>
        /// Starts the count
        /// </summary>
        void Start();

        void Restart();

        void Stop();

        /// <summary>Gets the time in miliseconds</summary>
        /// <returns></returns>
        long GetTime();
    }
}
