using SAMLSPLib.RequestBuilders;
using SAMLSPLib.ResponseHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMLLib
{
    public class ExampleUsage
    {
        public string GenerateAuthRequest()
        {
            AuthRequestBuilder req = new AuthRequestBuilder();
           string base64EncodedStr=req.GetRequest(SAMLLib.Enum.RequestFormat.Base64);
           return base64EncodedStr;
        }

        public void HandlerAuthResponse(string base64SAMLResponse)
        {
            AuthResponseHandler samlResponse = new AuthResponseHandler();
            samlResponse.LoadXmlFromBase64(base64SAMLResponse);
            if (samlResponse.IsValid())
            {
                // do staff
                samlResponse.GetAssertionXMLDocString();
                samlResponse.GetNameID();
            }
            else
            {
                // invalid repsone logci handler here
            }

        }

        public string GenerateLogoutRequest(string nameID)
        {
            LogoutRequestBuilder req = new LogoutRequestBuilder();
            string base64EncodedStr = req.GetLogoutRequest(nameID,SAMLLib.Enum.RequestFormat.Base64);
            return base64EncodedStr;
        }

        public void HandlerLogoutResponse(string base64SAMLResponse)
        {
            LogoutResponseHandler samlResponse = new LogoutResponseHandler();
            samlResponse.LoadXmlFromBase64(base64SAMLResponse);
            if (samlResponse.IsValid())
            {
                // do staff
                samlResponse.GetAssertionXMLDocString();
                samlResponse.GetStatusCodeValue();
            }
            else
            {
                // invalid repsone logci handler here
            }

        }

    }
}
