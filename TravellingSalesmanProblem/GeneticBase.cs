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
        private int[][] _parentGenoms;
        private int[][] _childrenGenoms;
        private Random random = new Random();

        public int CountGeneration { get; set; }
        public int CountChildrenInGeneration { get; set; }
        public bool UseMutation { get; set; }
        public double MunationPercent { get; set; }

        public GeneticBase(IFintness fintness)
        {
            _fintness = fintness;
            _genomLength = fintness.Arity;
            _countGens = (int) Math.Ceiling((double)_genomLength / 32);
        }

        private void DoSelection()
        {
            for (int i = 0; i < CountChildrenInGeneration; ++i)
            {
                int ind1 = random.Next(CountChildrenInGeneration);
                int ind2 = random.Next(CountChildrenInGeneration);

                int fr1 = _fintness.fit(_parentGenoms[ind1]);
                int fr2 = _fintness.fit(_parentGenoms[ind2]);

                _childrenGenoms[i] = fr1 > fr2 ? (int[])_parentGenoms[ind1].Clone() :
                    (int[])_parentGenoms[ind2].Clone();
                
            }
        }

        private void Cross(int[] genom1, int[] genom2)
        {
            for (int i = 0; i < _countGens; ++i)
            {
                int mask = random.Next();
                int swapMask = (genom1[i] ^ genom2[i]) & mask;
                genom1[i] ^= swapMask;
                genom2[i] ^= swapMask;
            }
        }

        private void Crossing()
        {
            for (int i = 0; i < CountChildrenInGeneration / 2; ++i)
            {
                int ind1 = i * 2;
                int ind2 = ind1 + 1;
                Cross(_childrenGenoms[ind1], _childrenGenoms[ind2]);
            }
        }
    }
}
