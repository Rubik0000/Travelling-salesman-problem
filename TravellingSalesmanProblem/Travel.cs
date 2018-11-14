using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class Travel : IFintness
    {
        static private Random _random = new Random();

        public int CountCities { get; private set; } = 4;

        private int[,] _matrix;


        public Travel(int[,] matr, int count)
        {
            _matrix = matr;
            CountCities = count;
            //_matrix = new int[,]
            //{
            //    {0, 5, 7, 9},
            //    {5, 0, 6, 2},
            //    {7, 6, 0, 4},
            //    {9, 2, 4, 0}
            //};
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

        public int Arity => CountCities * 32;

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

        public int Fit(int[] genom)
        {
            var path = FormPath(genom);
            int len = GetPathLen(path);
            return len == -1 ? 0 : int.MaxValue - len;
        }

        public int[] FormPath(int[] genom)
        {            
            int[] path = new int[CountCities];
            for (int i = 0; i < genom.Length; ++i)
            {                
                path[i] = genom[i] % CountCities;                 
            }
            
            return path;
        }
    }
}
