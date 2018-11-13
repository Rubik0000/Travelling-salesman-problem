﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    interface IFintness
    {
        int Arity { get; }

        long fit(long[] genom);
    }
}
