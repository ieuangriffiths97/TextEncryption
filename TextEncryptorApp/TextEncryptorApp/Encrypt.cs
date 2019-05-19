using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextEncryptorApp
{
    class Encrypt
    {
        private const string initVector = "pemgail9uzpgzl88";
        private const int KeySize = 256;

        //encrypt
        public static string EncryptString(string PlainText, string Password)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            PasswordDeriveBytes passwordDB = new PasswordDeriveBytes(Password, null);

            byte[] KeyBytes = passwordDB.GetBytes(KeySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(KeyBytes, initVectorBytes);

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(PlainTextBytes, 0, PlainTextBytes.Length);
            cs.FlushFinalBlock();
            byte[] cipherTextBytes = ms.ToArray();

            ms.Close();
            cs.Close();

            return Convert.ToBase64String(cipherTextBytes);

        }

        //decrypt

        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(KeySize/8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream ms = new MemoryStream(cipherTextBytes);
            CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cs.Read(plainTextBytes, 0, plainTextBytes.Length);
            ms.Close();
            cs.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

    }
}
