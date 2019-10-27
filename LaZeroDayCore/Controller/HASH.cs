using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LaZeroDayCore.Controller
{
    public class HASH
    {
        public class Algorithms
        {
            public static readonly HashAlgorithm MD5 = new MD5CryptoServiceProvider();
            public static readonly HashAlgorithm SHA1 = new SHA1Managed();
            public static readonly HashAlgorithm SHA256 = new SHA256Managed();
            public static readonly HashAlgorithm SHA384 = new SHA384Managed();
            public static readonly HashAlgorithm SHA512 = new SHA512Managed();
            public static readonly HashAlgorithm RIPEMD160 = new RIPEMD160Managed();
        }
        //
        public static byte[] byte_array(Stream p_input, HashAlgorithm algorithm)
        {
            return algorithm.ComputeHash(p_input);
        }
        public static String Base64(Stream p_input, HashAlgorithm algorithm)
        {
            return Convert.ToBase64String(algorithm.ComputeHash(p_input));
        }
        public static String hex(Stream p_input, HashAlgorithm algorithm)
        {
            return BitConverter.ToString(algorithm.ComputeHash(p_input)).Replace("-", "");
        }
        //
        public static byte[] byte_array(byte[] p_input, HashAlgorithm algorithm)
        {
            return algorithm.ComputeHash(p_input);
        }
        public static String Base64(byte[] p_input, HashAlgorithm algorithm)
        {
            return Convert.ToBase64String(algorithm.ComputeHash(p_input));
        }
        public static String hex(byte[] p_input, HashAlgorithm algorithm)
        {
            return BitConverter.ToString(algorithm.ComputeHash(p_input)).Replace("-", "");
        }
        //
        //
        public static byte[] byte_array(String p_input, HashAlgorithm algorithm)
        {
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(p_input));
        }
        public static String Base64(String p_input, HashAlgorithm algorithm)
        {
            return Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(p_input)));
        }
        public static String hex(String p_input, HashAlgorithm algorithm)
        {
            return BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(p_input))).Replace("-", "");
        }
        //
    }
}
