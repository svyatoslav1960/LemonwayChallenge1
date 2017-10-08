using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlToJson
{
    public class XmlToJsonConverter: IXmlToJsonConverter
    {
        public string Convert(string sourceXmlString)
        {
            var doc = new XmlDocument();
            doc.XmlResolver = null; // prevent DTD processing to defend against XML External Entity attack
            try
            {
                doc.LoadXml(sourceXmlString);
            }
            catch (Exception e)
            {
                throw new BadXmlException("Bad Xml format", e);
            }

            return Convert(doc);
        }

        public string Convert(XmlNode sourceNode)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeXmlNode(sourceNode, Newtonsoft.Json.Formatting.Indented);
            return result;
        }
    }
}
