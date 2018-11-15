using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    /// <summary>
    /// The pair with mutable key and value
    /// </summary>
    internal class Pair : IComparable<Pair>
    {
        public int Key { get; set; }
        public int Value { get; set; }

        public Pair(int key, int value)
        {
            Key = key;
            Value = value;
        }

        public Pair() { }

        /// <summary>
        /// Compares by values
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Pair other) => Value - other.Value;
    }
}
