using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using XmlToJson;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestClass]
    public class XmlToJsonConverterTests
    {
        [TestMethod]
        public void canConvertXmlDocument()
        {
            const string sampleXmlString = "<foo>bar</foo>";
            var doc = new XmlDocument();

            doc.LoadXml(sampleXmlString);

            var converter = new XmlToJsonConverter();

            var result = converter.Convert(doc);

            Assert.AreEqual(removeAllBlanks(@"{ ""foo"" : ""bar"" }"), removeAllBlanks(result));
        }

        [TestMethod]
        public void canConvertXmlString()
        {
            const string sampleXmlString = "<foo>bar</foo>";

            var converter = new XmlToJsonConverter();

            var result = converter.Convert(sampleXmlString);

            Assert.AreEqual(removeAllBlanks(@"{ ""foo"" : ""bar"" }"), removeAllBlanks(result));
        }

        [TestMethod]
        public void canConvertAcceptanceTestSample()
        {
            const string testSample = "<TRANS><HPAY><ID>103</ID><STATUS>3</STATUS><EXTRA><IS3DS>0</IS3DS><AUTH>031183</AUTH></EXTRA><INT_MSG/><MLABEL>501767XXXXXX6700</MLABEL><MTOKEN>project01</MTOKEN></HPAY></TRANS>";
            const string expectedResult = @"{
  ""TRANS"": {
    ""HPAY"": {
      ""ID"": ""103"",
      ""STATUS"": ""3"",
      ""EXTRA"": {
        ""IS3DS"": ""0"",
        ""AUTH"": ""031183""
      },
      ""INT_MSG"": null,
      ""MLABEL"": ""501767XXXXXX6700"",
      ""MTOKEN"": ""project01""
    }
  }
}";
            var converter = new XmlToJsonConverter();

            var result = converter.Convert(testSample);

            Assert.AreEqual(removeAllBlanks(expectedResult), removeAllBlanks(result));
        }

        [TestMethod]
        [ExpectedException(typeof(BadXmlException))]
        public void throwsBadXmlFormatExceptionOnBadXmlInput()
        {
            const string testSample = "<foo>hello</bar>";
            var converter = new XmlToJsonConverter();

            converter.Convert(testSample);

        }

        private string removeAllBlanks(string source)
        {
            return Regex.Replace(source, @"\s|\r|\n", string.Empty);
        }
    }
}
