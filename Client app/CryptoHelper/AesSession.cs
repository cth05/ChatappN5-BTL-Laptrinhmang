using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_app
{
    public class AesSession
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public static AesSession Create()
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();

                return new AesSession
                {
                    Key = aes.Key,
                    IV = aes.IV
                };
            }
        }
    }
    public static class AesCrypto
    {
        public static byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        public static byte[] Decrypt(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                }
            }
        }
    }
}
