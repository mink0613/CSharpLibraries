using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonLibrary
{
    public class Encryption
    {
        /// <summary>
        /// A key used for encryption / decryption
        /// </summary>
        private static string _encryptionKey;

        /// <summary>
        /// Use Rfc2898DeriveBytes to encrypt clear text
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns>Encrypted cipher text</returns>
        public static string Encrypt(string clearText)
        {
            if (_encryptionKey == null)
            {
                _encryptionKey = UID.Generate();
            }

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            string cipherText = string.Empty;

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Use Rfc2898DeriveBytes to decrypt cipher text
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>Decrypted clear text</returns>
        public static string Decrypt(string cipherText)
        {
            if (_encryptionKey == null)
            {
                _encryptionKey = UID.Generate();
            }

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string clearText = string.Empty;

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    clearText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return clearText;
        }
    }
}