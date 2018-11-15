using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// An interface to solve a task by exhaustive search
    /// </summary>
    interface IExhaustive<T>
    {
        T[] GetSet();

        int OptimalKoef(T[] perm);
    }
}
