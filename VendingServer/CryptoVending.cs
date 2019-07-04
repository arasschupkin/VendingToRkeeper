using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Vending
{
    public static class CryptoVending
    {
        public static byte[] key = { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8 };
        public static byte[] vector = { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8 };

        public static string Decrypt(byte[] inData)
        {
            string message = string.Empty;

            try
            {

                byte[] data = new byte[inData.Length - 4];

                Array.Copy(inData, 4, data, 0, inData.Length - 4);
                

                //byte[] _mesage = { 0x56, 0x52, 0xCC, 0xA0, 0x89, 0x87, 0x12, 0xF8, 0xE3, 0x26, 0xD0, 0x60, 0x16, 0x3A, 0xE, 0xA0, 0xC3, 0xB, 0x8C, 0x9, 0x71, 0x2D, 0xE6, 0x11, 0x4D, 0xEB, 0x59, 0xFA, 0xC3, 0xF9, 0x7B, 0xF1, 0x69, 0xD2, 0x15, 0x5, 0x5D, 0x79, 0x7C, 0xEF };

                //byte[] _mesage = { 0xA, 0x26, 0x1B, 0x8C, 0x8E, 0xBA, 0x7D, 0x67 };

                //byte[] _mesage = { 0xD0, 0x7, 0xEC, 0x2F, 0x9E, 0x98, 0xD7, 0xB3 };


                //byte[] _mesage = { 0x40, 0xE0, 0x31, 0xAE, 0x5E, 0x40, 0x30, 0x9B, 0x6E, 0x2A, 0x78, 0xB7, 0xDC, 0x14, 0x49, 0xA8, 0x70, 0x22, 0x14, 0xE0, 0xF7, 0x3D, 0x86, 0xCA, 0x2F, 0x3E, 0x48, 0x76, 0x33, 0xD7, 0x16, 0xD2 };

                MemoryStream streamDecryptor = new MemoryStream();

                DES DecryptorDESProvider = new DESCryptoServiceProvider();
                DecryptorDESProvider.Padding = PaddingMode.None;
                DecryptorDESProvider.Mode = CipherMode.CBC;
                ICryptoTransform Decryptor = DecryptorDESProvider.CreateDecryptor(key, vector);
                CryptoStream cryptostream1 = new CryptoStream(streamDecryptor, Decryptor, CryptoStreamMode.Write);

                cryptostream1.Write(data, 0, data.Length);
                //cryptostream1.Write(inData, 0, inData.Length);

                cryptostream1.Flush();

                byte[] readbyte = streamDecryptor.ToArray();

                cryptostream1.Close();

                message = Encoding.Default.GetString(readbyte).Trim();

                int[] str = new int[] { 1, 2, 3, 4, 5, 6, 7 };

                for (int i = 0; i < str.Length; i++)
                {
                    string s = Convert.ToString((char)str[i]);
                    message = message.Replace(s, "");
                }
            }
            catch
            {
                message = string.Empty;
            }
            return message;

            //System.Windows.Forms.MessageBox.Show(ResiveMessage);

            /*
            if (data == null)
            data = new byte[4];


            bool Result = false;
            uint i = data[0];
            i = (i << 8) + data[1];
            i = (i << 8) + data[2];
            i = (i << 8) + data[3];
            Result = i == 0 ? false : true;

                */
        }

        public static byte[] Encrypt(string message)
        {

            byte[] bin = GetByte(message);

            DES EncryptorDESProvider = new DESCryptoServiceProvider();
            EncryptorDESProvider.Mode = CipherMode.CBC;
            EncryptorDESProvider.Padding = PaddingMode.None;            
            MemoryStream streamEncryptor = new MemoryStream();
            MemoryStream streamOut = new MemoryStream();

            ICryptoTransform Encryptor = EncryptorDESProvider.CreateEncryptor(key, vector);
            CryptoStream cryptostream2 = new CryptoStream(streamEncryptor, Encryptor, CryptoStreamMode.Write);
                       
            cryptostream2.Write(bin, 0, bin.Length);
            cryptostream2.Flush();

            byte[] outbyte = streamEncryptor.ToArray();

            cryptostream2.Close();

            return outbyte;

        }

        private static byte[] GetByte(string message)
        {
            byte[] inData = Encoding.Default.GetBytes(message);
            int size = inData.Length / 8 * 8 == inData.Length ? inData.Length : (inData.Length / 8 + 1) * 8;
            byte[] result = new byte[size];
            
            Array.Copy(inData, result, inData.Length);

            for (int i = inData.Length; i < result.Length; i++)            
                result[i] = (byte)(result.Length - inData.Length);           

            return result;

        }

    }
}
