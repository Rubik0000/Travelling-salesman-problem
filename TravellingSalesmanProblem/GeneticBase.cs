using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class GeneticBase
    {
        private IFintness _fintness;
        private int _genomLength;
        private int _countGens;
        private long[][] _parentGenoms;
        private long[][] _childrenGenoms;
        private Random random = new Random();

        public int CountGeneration { get; set; }
        public int CountChildrenInGeneration { get; set; }
        public bool UseMutation { get; set; }
        public double MunationPercent { get; set; }

        public GeneticBase(IFintness fintness)
        {
            _fintness = fintness;
        }

        private void DoSelection()
        {
            for (int i = 0; i < CountChildrenInGeneration; ++i)
            {
                int ind1 = random.Next(CountChildrenInGeneration);
                int ind2 = random.Next(CountChildrenInGeneration);

                long fr1 = _fintness.fit(_parentGenoms[ind1]);
                long fr2 = _fintness.fit(_parentGenoms[ind2]);

                _childrenGenoms[i] = fr1 > fr2 ? (long[])_parentGenoms[ind1].Clone() :
                    (long[])_parentGenoms[ind2].Clone();
                
            }
        }
    }
}
