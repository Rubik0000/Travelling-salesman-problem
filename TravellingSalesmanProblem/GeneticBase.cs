using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// The genetic algorithm
    /// </summary>
    class GeneticBase
    {
        static private readonly int BIT_PER_GEN = 32;

        /// <summary>The fitness function</summary>
        private IFintness _fintness;

        /// <summary>The length of the genom in  bits</summary>
        private int _genomLength;

        /// <summary>
        /// The count gens per genom
        /// A one gene has 'BIT_PER_GEN' bits
        /// </summary>
        private int _countGens;

        /// <summary>The parent</summary>
        private int[][] _parentGenoms;

        /// <summary>The children</summary>
        private int[][] _childrenGenoms;

        /// <summary>Random generator</summary>
        private Random random = new Random();

        /// <summary>Time checker</summary>
        private ITimeCheck _timeCheck;

        /// <summary>The count of generation due to working algorithm</summary>
        public int CountGenerations { get; set; }

        /// <summary>The count of children in every generation</summary>
        public int CountEntitiesInGeneration { get; set; }

        /// <summary>Whether mutations will be used</summary>
        public bool UseMutation { get; set; }

        /// <summary>The frequency of mutations</summary>
        public double MunationPercent { get; set; }

        public GeneticBase(IFintness fintness, ITimeCheck timeCheck = null)
        {
            _fintness = fintness;
            _genomLength = fintness.Arity;
            _countGens = (int) Math.Ceiling((double)_genomLength / BIT_PER_GEN);
            _timeCheck = timeCheck;
        }

        /// <summary>
        /// The tournament selection
        /// </summary>
        private void DoSelection()
        {
            // for every children
            for (int i = 0; i < CountEntitiesInGeneration; ++i)
            {
                // choose two random parents
                int ind1 = random.Next(CountEntitiesInGeneration);
                int ind2 = random.Next(CountEntitiesInGeneration);

                // take the value of the fitnes finction
                int fr1 = _fintness.Fit(_parentGenoms[ind1]);
                int fr2 = _fintness.Fit(_parentGenoms[ind2]);

                // set to the current children the selected parent
                // with the best value of the fitness function
                _childrenGenoms[i] = fr1 > fr2 ? (int[])_parentGenoms[ind1].Clone() :
                    (int[])_parentGenoms[ind2].Clone();                
            }
        }

        /// <summary>
        /// Crosses two genoms
        /// </summary>
        /// <param name="genom1"></param>
        /// <param name="genom2"></param>
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

        /// <summary>
        /// Crosses children generation
        /// </summary>
        private void CrossGeneration()
        {
            for (int i = 0; i < CountEntitiesInGeneration / 2; ++i)
            {
                int ind1 = i * 2;
                int ind2 = ind1 + 1;
                CrossGenoms(_childrenGenoms[ind1], _childrenGenoms[ind2]);
            }
        }

        /// <summary>
        /// Mutate genom randomly
        /// </summary>
        /// <param name="genom"></param>
        private void MutateGenom(int[] genom)
        {
            // change one bit in genom
            int ind = random.Next(_countGens);
            int offset = random.Next(BIT_PER_GEN);
            int mask = 1 << offset;
            genom[ind] ^= mask;
        }

        /// <summary>
        /// Mutates children generation
        /// </summary>
        private void MutateGeneration()
        {
            foreach (var genom in _childrenGenoms)
            {
                if (random.NextDouble() <= MunationPercent)
                    MutateGenom(genom);
                
            }
        }

        /// <summary>
        /// Creates random genom
        /// </summary>
        /// <returns></returns>
        private int[] GenerateRandomGenom()
        {
            var res = new int[_countGens];
            for (int i = 0; i < res.Length; ++i)
            {
                res[i] = random.Next();
            }
            return res;
        }

        /// <summary>
        /// Creates the first generation of parents
        /// </summary>
        private void CreateFirstGeneration()
        {
            for (int i = 0; i < _parentGenoms.Length; ++i)
            {
                _parentGenoms[i] = GenerateRandomGenom();
            }
        }

        /// <summary>
        /// Starts the algorithm
        /// </summary>
        /// <returns>The best genom</returns>
        public int[] Run()
        {
            _parentGenoms = new int[CountEntitiesInGeneration][];
            _childrenGenoms = new int[CountEntitiesInGeneration][];

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

            // choose the best genom
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
