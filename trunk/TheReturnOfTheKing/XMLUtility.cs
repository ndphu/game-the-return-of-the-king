using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TheReturnOfTheKing
{
    public class XMLUtility
    {
        XmlDocument _doc;

        public XMLUtility(string fileName)
        {
            _doc = new XmlDocument();
            _doc.Load(fileName);
        }

        public string GetElementValue(string elementName)
        {
            try
            {
                return ((XmlElement)_doc.SelectSingleNode(@"//" + elementName)).InnerText;
            }
            catch
            {
                return null;
            }
        }

    }
}
