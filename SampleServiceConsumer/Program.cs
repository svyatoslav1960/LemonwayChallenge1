using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var svc = new SampleService.SampleService();

                var serviceResult = svc.Fibonacci(10);

                Console.WriteLine("Web service call result: {0}", serviceResult);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected exception while calling the sample web service: {0}", ex);
            }
        }
    }
}
