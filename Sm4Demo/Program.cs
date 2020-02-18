using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Linq;
using System.Text;

namespace Sm4Demo
{
    class Program
    {
        static void Main()
        { 
            byte[] plaintext = Encoding.ASCII.GetBytes("Hello World");
            byte[] keyBytes = Encoding.ASCII.GetBytes("0123456789ABCDEF");
            byte[] iv = new byte[16];

            // 加密
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/CBC/PKCS7Padding");
            inCipher.Init(true, key);
            inCipher.ProcessBytes(plaintext);
            byte[] cipher = inCipher.DoFinal();
            Console.WriteLine("加密后的密文(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));

            // 解密
            inCipher.Reset();
            inCipher.Init(false, key);
            inCipher.ProcessBytes(cipher);
            byte[] bin = inCipher.DoFinal();
            string ans = Encoding.UTF8.GetString(bin);

            Console.WriteLine("解密明文内容:  {0}\t是否匹配: {1}", ans, Enumerable.SequenceEqual(plaintext, bin));

            Console.ReadLine();
        }
    }
}
