using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class Travel : IFintness, IExhaustive<int>
    {
        static private Random _random = new Random();

        public int CountCities { get; private set; } = 4;

        private int[,] _matrix;

        private MyPair[] _pairs;

        private int[] _path;
        
        public Travel(int[,] matr, int count)
        {
            _matrix = matr;
            CountCities = count;

            _pairs = new MyPair[CountCities];
            _path = new int[CountCities];
            for (int i = 0; i < CountCities; ++i)
            {
                _pairs[i] = new MyPair();
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
            for (int i = 0; i < CountCities; ++i)
            {
                _pairs[i].Key = i;
                _pairs[i].Value = genom[i];
            }

            Array.Sort(_pairs);
            
            for (int i = 0; i < genom.Length; ++i)
            {
                //path[i] = genom[i] % CountCities;                 
                _path[i] = _pairs[i].Key;
            }            
            return _path;
        }

        public int[] GetSet()
        {
            var citieNums = new int[CountCities];
            for (int i = 0; i < CountCities; ++i)
            {
                citieNums[i] = i;
            }
            return citieNums;
        }

        public int OptimalKoef(int[] perm)
        {
            int len = GetPathLen(perm);
            return int.MaxValue - len;
        }
    }

    class MyPair : IComparable<MyPair>
    {
        public int Key { get; set; }
        public int Value { get; set; }

        public MyPair(int key, int value)
        {
            Key = key;
            Value = value;
        }

        public MyPair() { }

        public int CompareTo(MyPair other) => Value - other.Value;
    }
}
