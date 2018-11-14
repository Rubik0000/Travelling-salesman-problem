using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class ExhaustiveSearch
    {
        private MinArea.IPermutations<int> _perm;
        private Travel _travel;

        public ExhaustiveSearch(MinArea.IPermutations<int> perm, Travel tr)
        {
            _perm = perm;
            _travel = tr;
        }        

        public int[] GetShortestPath()
        {
            var citiesNums = new int[_travel.CountCities];
            for (int i = 0; i < _travel.CountCities; ++i)
            {
                citiesNums[i] = i;
            }
            int[] shortesPath = null;
            int minLen = int.MaxValue;
            _perm.Generate(citiesNums, path =>
            {
                int len = _travel.GetPathLen(path);
                if (len < minLen)
                {
                    minLen = len;
                    shortesPath = (int[])path.Clone();
                }
            });
            return shortesPath;
        }
    }
}
