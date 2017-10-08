using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFibonacciSequenceCalculator
    {
        BigInteger CalculateNthNumber(int n);
    }
}
