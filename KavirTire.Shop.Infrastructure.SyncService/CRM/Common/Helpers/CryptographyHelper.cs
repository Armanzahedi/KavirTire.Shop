using System;
using System.Security.Cryptography;
using System.Text;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers
{
    public class CryptographyHelper
    {
        private readonly byte[] key = { 249, 39, 134, 59, 191, 205, 154, 155, 52, 201, 182, 79, 235, 220, 162, 127, 131, 6, 205, 251, 166, 137, 119, 117 };

        public string EncryptString(string inputString)
        {
            string encryptedString = string.Empty;

            if (!string.IsNullOrEmpty(inputString))
            {
                using (TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider())
                {
                    cryptoProvider.Key = key;
                    cryptoProvider.Mode = CipherMode.ECB;
                    cryptoProvider.Padding = PaddingMode.PKCS7;

                    byte[] inputStringByte = Encoding.UTF8.GetBytes(inputString);

                    var encryptor = cryptoProvider.CreateEncryptor();
                    byte[] encryptedByte = encryptor.TransformFinalBlock(inputStringByte, 0, inputStringByte.Length);

                    encryptedString = Convert.ToBase64String(encryptedByte);

                }
            }
            return encryptedString;
        }

        public string DecryptString(string inputString)
        {
            string decryptString = string.Empty;
            if (!string.IsNullOrEmpty(inputString))
            {
                using (var cryptoProvider = new TripleDESCryptoServiceProvider())
                {
                    cryptoProvider.Key = key;
                    cryptoProvider.Mode = CipherMode.ECB;
                    cryptoProvider.Padding = PaddingMode.PKCS7;

                    byte[] inputStringByte = Convert.FromBase64String(inputString);

                    var decryptor = cryptoProvider.CreateDecryptor();
                    byte[] decryptedByte = decryptor.TransformFinalBlock(inputStringByte, 0, inputStringByte.Length);

                    decryptString = Encoding.UTF8.GetString(decryptedByte);

                }
            }
            return decryptString;
        }
    }
}
