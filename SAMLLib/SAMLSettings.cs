using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMLSPLib
{
    public class SAMLSettings
    {
        public static string certificate = "x.509 certificate comes here";
        public static string assertionConsumerServiceUrl = "http://example.org/samlassertionconsumer";    
        public static string issuer = "http://example.org/samlassertionconsumer";      
        public static string idp_sso_target_url = "https://targetidp.org/sso";
        public static string idp_logout_target_url = "https://targetidp.org/logout";

    }
}
