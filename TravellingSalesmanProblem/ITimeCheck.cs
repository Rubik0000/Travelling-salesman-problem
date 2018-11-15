using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    interface ITimeCheck
    {
        void Start();

        void Restart();

        void Stop();

        long GetTime();
    }
}
