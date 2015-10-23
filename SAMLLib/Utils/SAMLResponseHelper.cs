using SAMLSPLib;
using SAMLSPLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAMLLib.Utils
{
    public class SAMLResponseHelper
    {
        private XmlDocument xmlDoc;

        private CertificateHelper certificateHelper;

        public SAMLResponseHelper()
        {
            certificateHelper = new CertificateHelper();
            certificateHelper.LoadCertificate(SAMLSettings.certificate);
        }

        public string GetAssertionXMLDocString()
        {

            XmlNode xmlNode = xmlDoc.GetElementsByTagName("saml2:Assertion")[0];

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                xmlNode.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
        public void LoadXml(string xml)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.XmlResolver = null;
            xmlDoc.LoadXml(xml);
        }

        public void LoadXmlFromBase64(string response)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            LoadXml(enc.GetString(Convert.FromBase64String(response)));
        }

        public bool IsValid()
        {
            bool status = false;

            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            XmlNodeList nodeList = xmlDoc.SelectNodes("//ds:Signature", manager);

            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.LoadXml((XmlElement)nodeList[0]);
            status = signedXml.CheckSignature(certificateHelper.cert, true);
            if (!status)
                return false;
            return status;

        }

        public XmlDocument GetXML()
        {
            return xmlDoc;
        }
    }
}
