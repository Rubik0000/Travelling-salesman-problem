using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MinArea
{
    /// <summary>
    /// The help class
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Swaps two values
        /// </summary>
        /// <typeparam name="T">Any type of the values</typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }        
    }
}
