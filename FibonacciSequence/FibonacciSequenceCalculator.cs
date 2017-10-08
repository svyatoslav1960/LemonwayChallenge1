using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciSequence
{
    public class FibonacciSequenceCalculator: IFibonacciSequenceCalculator
    {
        /// <summary>
        /// Simple implementation of the Fibonacci number calculation.
        /// </summary>
        /// <param name="n">sequential number in the Fibonacci series, for which a value must be calculated</param>
        /// <returns>calculated Fibonacci number, corresponding to n-th place in the Fibonacci sequence.</returns>
        /// <remarks>Non-recursive method is used, to void potential stack overflow</remarks>
        public BigInteger CalculateNthNumber(int n)
        {
            // boundary conditions as required.
            if (n < 1 || n > 100)
            {
                return -1;
            }

            if (n == 1)
            {
                return 1;
            }

            BigInteger fbNminus2 = 0, fbNminus1 = 1;

            for (var i = 2; i <= n; i++)
            {
                var fbCurrent = fbNminus2 + fbNminus1;
                fbNminus2 = fbNminus1;
                fbNminus1 = fbCurrent;
            }

            return fbNminus1;
        }
    }
}
