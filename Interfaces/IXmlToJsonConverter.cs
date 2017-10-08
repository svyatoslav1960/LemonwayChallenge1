using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IXmlToJsonConverter
    {
        string Convert(string sourceXmlString);
        string Convert(System.Xml.XmlNode sourceNode);
    }
}
