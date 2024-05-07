using System.Security.Cryptography;
using System.Text;
namespace MaidManagementSolutions.Web.Halper
{
    public class CryptoHelper
    {
        /// <summary>
        /// take any string and encrypt it using MD5Hash then
        /// return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as string</returns>
        public static string CalculateMD5Hash(string input)
        {
            try
            {


                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// take any string and encrypt it using SHA512 then
        /// return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetSHA512HashData(string data)
        {
            //create new instance of md5
            SHA512 sha512 = SHA512.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha512.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("X2"));
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}
