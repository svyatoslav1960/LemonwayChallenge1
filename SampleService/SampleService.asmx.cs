using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using SampleService.Factories;
using log4net;

namespace SampleService
{
    /// <summary>
    /// Summary description for SampleService
    /// </summary>
    [WebService(Namespace = SampleService.webLemonNameSpace)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SampleService : System.Web.Services.WebService
    {
        internal const string webLemonNameSpace = "http://weblemon.challenge.org/";

        protected ILog logger = LogManager.GetLogger(typeof(SampleService));

        /// <summary>
        /// calculate n-th element of the fibonacci sequence
        /// </summary>
        /// <param name="sequentialNumber">the element number to calculate</param>
        /// <returns>n-th element value as string for compatibility with SOAP</returns>
        [WebMethod]
        public string Fibonacci(int sequentialNumber)
        {
            try
            {
                var fibonacciCalculator = ProvidersFactory.CreateFibonacciSequenceCalculator();
                var result = fibonacciCalculator.CalculateNthNumber(sequentialNumber);
                return result.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Error processing Fibonacci request", ex);
                throw CreateSoapException(ex);
            }
        }

        /// <summary>
        /// Converts XML to JSON representation
        /// </summary>
        /// <param name="sourceXmlString">XML formatted string</param>
        /// <returns>JSON representation of the source XML, or "Bad Xml format" string, if the input string does not contain a valid XML</returns>
        [WebMethod]
        public string XmlToJson(string sourceXmlString)
        {
            try
            {
                var xmlToJsonConverter = ProvidersFactory.CreateXmlToJsonConverter();

                var result = xmlToJsonConverter.Convert(sourceXmlString);

                return result;
            }
            catch (XmlToJson.BadXmlException)
            {
                return "Bad Xml format";
            }
            catch(Exception ex)
            {
                logger.Error("Error processing XmlToJson request", ex);
                throw CreateSoapException(ex);
            }
        }

        private SoapException CreateSoapException(Exception ex)
        {
            var doc = new XmlDocument();
            var node = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);
            var details = doc.CreateNode(XmlNodeType.Element, "ExceptionInfo", webLemonNameSpace);
            var exceptionDetails = doc.CreateNode(XmlNodeType.Text, "ExceptionMessage", webLemonNameSpace);
            exceptionDetails.Value = ex.Message;

            details.AppendChild(exceptionDetails);
            node.AppendChild(details);

            var soapException = new SoapException(
                "Unexpected fault occurred",
                SoapException.ClientFaultCode,
                Context.Request.Url.AbsoluteUri,
                node);
            return soapException;
        }
    }
}
