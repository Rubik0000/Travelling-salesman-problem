using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class ExhaustiveSearch<T>
    {
        private MinArea.IPermutations<T> _perm;
        private IExhaustive<T> _exh;
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

        public T[] GetOptimal()
        {
            //var citiesNums = new int[_travel.CountCities];
            //for (int i = 0; i < _travel.CountCities; ++i)
            //{
            //    citiesNums[i] = i;
            //}
            T[] result = null;
            int maxK = 0;

            _time?.Start();

            _perm.Generate(_exh.GetSet(), perm =>
            {
                int k = _exh.OptimalKoef(perm);
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
