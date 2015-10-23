using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMLSPLib.Utils
{
    public class Base64Encoder
    {
        public string GetBase64EncodeStr(string rawStr)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(rawStr);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
