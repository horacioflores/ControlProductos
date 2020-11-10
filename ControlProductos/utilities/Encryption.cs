using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ControlProductos.utilities
{
    public class Encryption
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public string Decrypt(string stringToDecrypt)
        {
            var inputByteArray = new byte[stringToDecrypt.Length + 1];
            var sEncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms,
                  des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray()).ToUpper();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                var SEncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt.ToUpper());
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms,
                  des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DecryptURL(string stringToDecrypt)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            string sEncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return "ERROR " + e.Message;
            }
        }

        public static string EncryptURL(string stringToEncrypt)
        {
            try
            {
                string sEncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return "ERROR " + e.Message;
            }
        }
    }
}