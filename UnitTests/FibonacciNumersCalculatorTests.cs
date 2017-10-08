using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using FibonacciSequence;

namespace UnitTests
{
    [TestClass]
    public class FibonacciNumersCalculatorTests
    {
        [TestMethod]
        public void canProduceCorrectFibonacciNthNumber()
        {
            Tuple<int, BigInteger>[] correctNthFibonacciNumbers = {
                Tuple.Create(1, (BigInteger)1),
                Tuple.Create(2, (BigInteger)1),
                Tuple.Create(3, (BigInteger)2),
                Tuple.Create(4, (BigInteger)3),
                Tuple.Create(5, (BigInteger)5),
                Tuple.Create(6, (BigInteger)8),
                Tuple.Create(7, (BigInteger)13),
                Tuple.Create(8, (BigInteger)21),
                Tuple.Create(9, (BigInteger)34),
                Tuple.Create(10, (BigInteger)55),
                Tuple.Create(99, BigInteger.Parse("218922995834555169026")),
                Tuple.Create(100, BigInteger.Parse("354224848179261915075")),
            };

            var calculator = new FibonacciSequenceCalculator();

            foreach (var nPair in correctNthFibonacciNumbers)
            {
                var result = calculator.CalculateNthNumber(nPair.Item1);
                Assert.AreEqual(nPair.Item2, result);
            }
        }

        [TestMethod]
        public void canHandleInvalidInput()
        {
            int[] sampleInvalidInputData = { int.MinValue, -1, 0, 101, int.MaxValue };
            var calculator = new FibonacciSequenceCalculator();

            foreach(var n in sampleInvalidInputData)
            {
                var result = calculator.CalculateNthNumber(n);
                Assert.AreEqual(-1, result);
            }
        }
    }
}
