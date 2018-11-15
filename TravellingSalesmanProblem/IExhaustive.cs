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
        /// <summary>
        /// Gets set over which the algorithm will work
        /// </summary>
        /// <returns></returns>
        T[] GetSet();

        /// <summary>
        /// If the permutation is more optimal than the function
        /// must return the higher сoefficient
        /// 
        /// The сoefficient must be non-negative
        /// </summary>
        /// <param name="perm"></param>
        /// <returns>The сoefficient of optimality</returns>
        int OptimalСoef(T[] perm);
    }
}
