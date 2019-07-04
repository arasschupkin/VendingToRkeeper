using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace DbHelper
{
    static class Crypto
    {
        private static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("Sodexo23");
        public static string Encrypt(string originalString)
        {
            try
            {
                if (String.IsNullOrEmpty(originalString))
                {
                    throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Decrypt(string cryptedString)
        {
            try
            {
                if (String.IsNullOrEmpty(cryptedString))
                {
                    throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
                }
                //byte[] _cryptedString =  ASCIIEncoding.ASCII.GetBytes(cryptedString);


                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                //MemoryStream memoryStream = new MemoryStream(_cryptedString);
                //MemoryStream memoryStream = new MemoryStream(Convert.ToByte(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
            catch { return cryptedString; }
        }
    }
}
