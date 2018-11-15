using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// An interface to solve a task by genetic algorithm
    /// </summary>
    interface IFintness
    {
        /// <summary>
        /// The number of genes in the genom
        /// </summary>
        int Arity { get; }

        /// <summary>
        /// The fitness function
        /// </summary>
        /// <param name="genom"></param>
        /// <returns></returns>
        int Fit(int[] genom);
    }
}
