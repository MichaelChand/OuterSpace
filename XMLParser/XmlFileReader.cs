using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLParser
{
    public class XmlFileReader
    {
        protected XmlDocument _xmlDocument;

        public XmlFileReader(string filename)
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(filename);
        }

        public void GetXMLData()
        {
            
        }

        public XmlDocument GetDocumentObject()
        {
            return _xmlDocument;
        }
    }
}
