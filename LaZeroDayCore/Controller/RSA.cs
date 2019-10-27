using System;
using System.Security.Cryptography;
using System.Text;

namespace LaZeroDayCore.Controller
{
    public class RSA
    {
        #region RSA
        private static RSACryptoServiceProvider mRSACryptoServiceProvider;
        public static String key_private = "<RSAKeyValue><Modulus>vSQ/7n9/bWFy3aGb+VqLTF+S1xSbq1mm2Jq0OCQBRovNjbqATYxZ1wr0Y2lVuZ6dHEitNgxN5Pmvs7aqy8a2eBfewEed0jAGTH2YOJL89q433JBB/TFc7rlt94x5/yfsKnkYsA355HKvgcm6V13sDGp/w5lL8N2l0RjkEHtVMx0=</Modulus><Exponent>AQAB</Exponent><P>67TDngoHNrnFvO0Ix21/+vJMBlhAmbXFwmvzdJfrxERsbb2FxcELzLLRnXp/6RB7fox431pWVxVNkblui6kwyQ==</P><Q>zW0mXB+wvlOMKM0AeyhKb9i+7AAJJzU7a5T9f5tyj5/fdLRJO6u9tt0GX60IG5pJheYGHKPFBp8Z9awUQJ+NtQ==</Q><DP>EZrjzYcUONsxUMYnlLWyDG1SjlrDIHf6OGZVvcynGHa/3oZ8WmPr3LMygt+mdCL+Blq5oH0f28tEivDdt+LAOQ==</DP><DQ>Xc51EOZryE3QmCUdDiBU9YQtkwllJKFbVAJCTE5I3ysrWFinhHcld+awbWpPZINtBZcHvXZPl3l52kTNLsFtbQ==</DQ><InverseQ>Gst3CP9qd7hLKvt7WtN5Qj9J0MN1E9eopNU9g0TcpWyXCwbM2Cb7NpmSUKXNve+GbdsGr5biPlW+EktHFOxagA==</InverseQ><D>irkCluc2S8A0Yi982Xc6Qaq7ZTp9LkDT6XwWADMQKHCzM2b9YWM2JU2ojx0bvceCzdLUD5cYrn8fCf1I6ap1+v1jHP6zz+U+5XTrheI/uaw+LMPYOz6WoLYpIbQf4SeCGhR+kwMEnyORIpWje6aoBTjpNFD3XEa5hkcq/bnMteE=</D></RSAKeyValue>";
        public static String key_public = "<RSAKeyValue><Modulus>vSQ/7n9/bWFy3aGb+VqLTF+S1xSbq1mm2Jq0OCQBRovNjbqATYxZ1wr0Y2lVuZ6dHEitNgxN5Pmvs7aqy8a2eBfewEed0jAGTH2YOJL89q433JBB/TFc7rlt94x5/yfsKnkYsA355HKvgcm6V13sDGp/w5lL8N2l0RjkEHtVMx0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        ///////////////////////////////////////////////////////////////////////////////////
        public static void RSAGenerator(int p_sizeKey)
        {
            mRSACryptoServiceProvider = new RSACryptoServiceProvider(p_sizeKey);
            key_private = mRSACryptoServiceProvider.ToXmlString(true);
            key_public = mRSACryptoServiceProvider.ToXmlString(false);
        }
        public static void RSAGenerator(String p_container_name)
        {
            CspParameters cspParams = new CspParameters(1);
            cspParams.KeyContainerName = p_container_name;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            mRSACryptoServiceProvider = new RSACryptoServiceProvider(cspParams);
            key_private = mRSACryptoServiceProvider.ToXmlString(true);
            key_public = mRSACryptoServiceProvider.ToXmlString(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////
        public static string encryption(String p_data)
        {
            var testData = Encoding.UTF8.GetBytes(p_data);
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(key_public.ToString());
                    var encryptedData = rsa.Encrypt(testData, true);
                    return Convert.ToBase64String(encryptedData);
                }
                catch { }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return "";
        }
        ///////////////////////////////////////////////////////////////////////////////////
        public static string decryption(String p_data)
        {
            var testData = Encoding.UTF8.GetBytes(p_data);
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    var base64Encrypted = p_data;
                    rsa.FromXmlString(key_private);
                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    return (Encoding.UTF8.GetString(decryptedBytes)).ToString();
                }
                catch { }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return "";
        }
        #endregion
    }
}
