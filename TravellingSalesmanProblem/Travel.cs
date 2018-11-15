using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// The travel salesman task
    /// </summary>
    class Travel : IFintness, IExhaustive<int>
    {
        static private readonly int BITS_PER_CITY = 32;
        static private Random _random = new Random();

        /// <summary>The count of the cities</summary>
        public int CountCities { get; private set; } = 4;

        /// <summary>The matrix of adjacency</summary>
        private int[,] _matrix;

        /// <summary>
        /// The pair with key as the number of the city
        /// and value as founded genom
        /// </summary>
        private Pair[] _pairs;

        /// <summary>The current path</summary>
        private int[] _path;
        
        public Travel(int[,] matr, int count)
        {
            _matrix = matr;
            CountCities = count;

            _pairs = new Pair[CountCities];
            _path = new int[CountCities];
            for (int i = 0; i < CountCities; ++i)
            {
                _pairs[i] = new Pair();
            }
        }

        static public Travel CreateRandomCities(int maxCount)
        {
            var matr = new int[maxCount, maxCount];
            for (int i = 0; i < maxCount; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    matr[i, j] = matr[j, i] = _random.Next(1, byte.MaxValue);
                }
                matr[i, i] = 0;
            }
            return new Travel(matr, maxCount);
        }

        /// <summary>The size of the genom in bits</summary>
        public int Arity => CountCities * BITS_PER_CITY;

        /// <summary>
        /// Get the path length by the number of the cities
        /// </summary>
        /// <param name="citiesNums"></param>
        /// <returns>
        /// The length or -1 if there are the same cities in path
        /// </returns>
        public int GetPathLen(int[] citiesNums)
        {
            bool[] visited = new bool[CountCities];
            int len = 0;
            for (int i = 0; i < citiesNums.Length - 1; ++i)
            {
                if (visited[citiesNums[i]])
                    return -1;

                visited[citiesNums[i]] = true;
                len += _matrix[citiesNums[i], citiesNums[i + 1]];
            }
            if (visited[citiesNums[citiesNums.Length - 1]])
                return -1;

            len += _matrix[citiesNums[citiesNums.Length - 1], citiesNums[0]];
            return len;
        }

        /// <summary>
        /// The fitness function
        /// </summary>
        /// <param name="genom"></param>
        /// <returns>
        /// If the formed path is shorter then the result is higher
        /// </returns>
        public int Fit(int[] genom)
        {
            var path = FormPath(genom);
            int len = GetPathLen(path);
            return len == -1 ? 0 : int.MaxValue - len;
        }

        /// <summary>
        /// Forms a path by a given genome
        /// </summary>
        /// <param name="genom"></param>
        /// <returns></returns>
        public int[] FormPath(int[] genom)
        {
            // for every city set the appropriate gene in natural order
            for (int i = 0; i < CountCities; ++i)
            {
                _pairs[i].Key = i;
                _pairs[i].Value = genom[i];
            }

            // sort pairs by the value of the genes
            Array.Sort(_pairs);
            
            // make a path by the next rule:
            // if the value of the gene of the city is less then
            // the city is going earler in the order
            for (int i = 0; i < genom.Length; ++i)
            {
                //path[i] = genom[i] % CountCities;                 
                _path[i] = _pairs[i].Key;
            }            
            return _path;
        }

        /// <summary>
        /// Gets the set of the numbers of the cities
        /// </summary>
        /// <returns></returns>
        public int[] GetSet()
        {
            var citieNums = new int[CountCities];
            for (int i = 0; i < CountCities; ++i)
            {
                citieNums[i] = i;
            }
            return citieNums;
        }

        /// <summary>
        /// Gets koefficient for permutation algorithms
        /// </summary>
        /// <param name="perm"></param>
        /// <returns>
        /// If the formed path is shorter then the result is higher
        /// </returns>
        public int OptimalKoef(int[] perm)
        {
            int len = GetPathLen(perm);
            return int.MaxValue - len;
        }
    }    
}
