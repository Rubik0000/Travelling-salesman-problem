using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ExhaustiveSearch<T>
    {
        /// <summary>Permutation generator</summary>
        private MinArea.IPermutations<T> _perm;

        /// <summary>
        /// 
        /// </summary>
        private IExhaustive<T> _exh;

        /// <summary>Time checker</summary>
        private ITimeCheck _time;

        public ExhaustiveSearch(
            MinArea.IPermutations<T> perm, 
            IExhaustive<T> exh,
            ITimeCheck time = null)
        {
            _perm = perm;
            _exh = exh;
            _time = time;
        }        

        /// <summary>
        /// Gets optimal permutation
        /// </summary>
        /// <returns></returns>
        public T[] GetOptimal()
        {            
            T[] result = null;
            int maxK = 0;

            _time?.Start();

            _perm.Generate(_exh.GetSet(), perm =>
            {
                int k = _exh.OptimalСoef(perm);
                if (k > maxK)
                {
                    maxK = k;
                    result = (T[])perm.Clone();
                }
            });

            _time?.Stop();

            return result;
        }
    }
}
