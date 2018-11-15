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
        private ITimeCheck _timeCheck;

        public int CountGenerations { get; set; }
        public int CountChildrenInGeneration { get; set; }
        public bool UseMutation { get; set; }
        public double MunationPercent { get; set; }

        public GeneticBase(IFintness fintness, ITimeCheck timeCheck = null)
        {
            _fintness = fintness;
            _genomLength = fintness.Arity;
            _countGens = (int) Math.Ceiling((double)_genomLength / 32);
            _timeCheck = timeCheck;
        }
       
        private void DoSelection()
        {
            for (int i = 0; i < CountChildrenInGeneration; ++i)
            {
                int ind1 = random.Next(CountChildrenInGeneration);
                int ind2 = random.Next(CountChildrenInGeneration);

                int fr1 = _fintness.Fit(_parentGenoms[ind1]);
                int fr2 = _fintness.Fit(_parentGenoms[ind2]);

                _childrenGenoms[i] = fr1 > fr2 ? (int[])_parentGenoms[ind1].Clone() :
                    (int[])_parentGenoms[ind2].Clone();
                
            }
        }

        private void CrossGenoms(int[] genom1, int[] genom2)
        {
            for (int i = 0; i < _countGens; ++i)
            {
                int mask = random.Next();
                int swapMask = (genom1[i] ^ genom2[i]) & mask;
                genom1[i] ^= swapMask;
                genom2[i] ^= swapMask;
            }
        }

        private void CrossGeneration()
        {
            for (int i = 0; i < CountChildrenInGeneration / 2; ++i)
            {
                int ind1 = i * 2;
                int ind2 = ind1 + 1;
                CrossGenoms(_childrenGenoms[ind1], _childrenGenoms[ind2]);
            }
        }

        private void MutateGenom(int[] genom)
        {
            int ind = random.Next(_countGens);
            int offset = random.Next(32);
            int mask = 1 << offset;
            genom[ind] ^= mask;
        }

        private void MutateGeneration()
        {
            foreach (var genom in _childrenGenoms)
            {
                if (random.NextDouble() <= MunationPercent)
                {
                    MutateGenom(genom);
                }
            }
        }

        private int[] GenerateRandomGenom()
        {
            var res = new int[_countGens];
            for (int i = 0; i < res.Length; ++i)
            {
                res[i] = random.Next();
            }
            return res;
        }

        private void CreateFirstGeneration()
        {
            for (int i = 0; i < _parentGenoms.Length; ++i)
            {
                _parentGenoms[i] = GenerateRandomGenom();
            }
        }

        public int[] Run()
        {
            _parentGenoms = new int[CountChildrenInGeneration][];
            _childrenGenoms = new int[CountChildrenInGeneration][];

            _timeCheck?.Start();

            CreateFirstGeneration();

            for (int currGen = 0; currGen < CountGenerations; ++currGen)
            {
                DoSelection();
                CrossGeneration();
                if (UseMutation)
                    MutateGeneration();

                var tmp = _parentGenoms;
                _parentGenoms = _childrenGenoms;
                _childrenGenoms = tmp;
            }
            int bestFitness = 0;
            int[] bestGenom = null;
            foreach (var genom in _parentGenoms)
            {
                int fitRes = _fintness.Fit(genom);
                if (fitRes > bestFitness)
                {
                    bestFitness = fitRes;
                    bestGenom = genom;
                }
            }

            _timeCheck?.Stop();

            return bestGenom;
        }
    }
}
