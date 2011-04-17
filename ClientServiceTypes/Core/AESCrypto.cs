using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ClientServiceTypes.Core
{
    public class AESCrypto
    {
        public static byte[] Encrypt(byte[] input, string passwd, byte[] salt)
        {
            var pdb = new Rfc2898DeriveBytes(passwd, salt);
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        public static byte[] Decrypt(byte[] input, string passwd ,byte[] salt)
        {
            var pdb = new Rfc2898DeriveBytes(passwd, salt);
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}
