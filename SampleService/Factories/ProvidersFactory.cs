using FibonacciSequence;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XmlToJson;

namespace SampleService.Factories
{
    /// <summary>
    /// Class factory. Together with Interfaces project, it facilitates Dependency Injection pattern upon the project growth.
    /// </summary>
    public class ProvidersFactory
    {
        public static IFibonacciSequenceCalculator CreateFibonacciSequenceCalculator()
        {
            return new FibonacciSequenceCalculator();
        }

        public static IXmlToJsonConverter CreateXmlToJsonConverter()
        {
            return new XmlToJsonConverter();
        }
    }
}