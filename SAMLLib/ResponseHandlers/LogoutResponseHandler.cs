using SAMLLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAMLSPLib.ResponseHandlers
{
    public class LogoutResponseHandler
    {
        private XmlDocument xmlDoc;
        private SAMLResponseHelper responserHelper;

        public LogoutResponseHandler()
        {
            responserHelper = new SAMLResponseHelper();

        }
        public string GetAssertionXMLDocString()
        {
            return responserHelper.GetAssertionXMLDocString();
        }
        public void LoadXml(string xml)
        {
            responserHelper.LoadXml(xml);
        }

        public void LoadXmlFromBase64(string response)
        {
            responserHelper.LoadXmlFromBase64(response);
        }

        public bool IsValid()
        {
            return responserHelper.IsValid();
        }

        public string GetStatusCodeValue()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp2", "urn:oasis:names:tc:SAML:2.0:protocol");
            XmlNode node = responserHelper.GetXML().SelectSingleNode("/samlp2:LogoutResponse/samlp:Status/samlp:StatusCode", manager);
            return node.Attributes["Value"].Value;
        }
    }
}
