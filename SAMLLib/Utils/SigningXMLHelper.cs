using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SAMLSPLib.Utils
{
    public class SigningXMLHelper
    {

        public string SignXml(XmlDocument Document, X509Certificate2 cert)
        {
            SignedXml signedXml = new SignedXml(Document);
            signedXml.SigningKey = cert.PrivateKey;
            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.            
            XmlDsigEnvelopedSignatureTransform env =
               new XmlDsigEnvelopedSignatureTransform(true);
            reference.AddTransform(env);

            //canonicalize
            XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
            reference.AddTransform(c14t);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
          
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            Document.DocumentElement.AppendChild(
                Document.ImportNode(xmlDigitalSignature, true));
            return Document.OuterXml;
        }
        
    }
}
