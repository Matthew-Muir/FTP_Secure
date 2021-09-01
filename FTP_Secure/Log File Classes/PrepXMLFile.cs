using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace FTP_Secure
{
    public static class PrepXMLFile
    {
        public static XPathNavigator ReturnNav(string fullFilePath)
        {
            //remove xmlns
            XmlnsRemove.Remove(fullFilePath);

            XPathDocument xmlDoc = new XPathDocument(fullFilePath);

            XPathNavigator nav = xmlDoc.CreateNavigator();

            return nav;

        }

        public static XmlDocument ReturnXmlDoc(string fullFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fullFilePath);
            return doc;
        }
    }
}
