using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToJson
{
    public class BadXmlException: Exception
    {
        public BadXmlException() { }

        public BadXmlException(string message): base(message) {   }

        public BadXmlException(string message, Exception innerException): base(message, innerException) { }
    }
}
