using SAMLSPLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAMLSPLib.RequestBuilders
{
    public class LogoutRequestBuilder
    {
        public string GetLogoutRequest(string nameID, SAMLLib.Enum.RequestFormat format)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;

                using (XmlWriter xw = XmlWriter.Create(sw, xws))
                {

                    xw.WriteStartElement("samlp", "LogoutRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("xmlns", "saml", null, "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteAttributeString("ID", "_" + System.Guid.NewGuid().ToString());
                    xw.WriteAttributeString("Version", "2.0");
                    xw.WriteAttributeString("IssueInstant", DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    xw.WriteAttributeString("Destination", SAMLSettings.idp_logout_target_url);

                    xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteString(SAMLSettings.issuer);
                    xw.WriteEndElement();

                    xw.WriteStartElement("saml", "NameID", null);
                    xw.WriteAttributeString("SPNameQualifier", SAMLSettings.issuer);

                    xw.WriteString(nameID);
                    xw.WriteEndElement();

                    xw.WriteEndElement();
                }
                if (format == SAMLLib.Enum.RequestFormat.Base64)
                {
                    Base64Encoder encoder = new Base64Encoder();
                    return encoder.GetBase64EncodeStr(sw.ToString());
                }
                return  sw.ToString();
            }
        }
    }
}
