using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_app
{
    internal static class RsaHelper
    {
        public static byte[] EncryptWithPublicKey(byte[] data, string publicKeyBase64)
        {
            // Decode XML public key
            string publicKeyXml = Encoding.UTF8.GetString(
                Convert.FromBase64String(publicKeyBase64)
            );

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(publicKeyXml);

                // false = PKCS#1 v1.5 (compatible .NET 4.8)
                return rsa.Encrypt(data, false);
            }
        }
    }
}
