using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    interface IExhaustive<T>
    {
        T[] GetSet();

        int OptimalKoef(T[] perm);
    }
}
