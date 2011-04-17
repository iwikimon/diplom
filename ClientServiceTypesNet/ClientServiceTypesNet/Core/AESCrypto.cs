using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ClientServiceTypes.Core
{
    public class AESCrypto
    {
        private static AESCrypto crypt = new AESCrypto();
        private Aes aes;
        private string aesPasswd = "asdjashkfjashdfkjas";

        private byte[] aesSalt = new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 };

        public static AESCrypto AES
        {
            get { return crypt; }
        }

        private AESCrypto()
        {
            var pdb = new Rfc2898DeriveBytes(aesPasswd, aesSalt);
            aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
        }

        public byte[] Encrypt(byte[] input, string passwd, byte[] salt)
        {
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        public byte[] Decrypt(byte[] input, string passwd, byte[] salt)
        {
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}
