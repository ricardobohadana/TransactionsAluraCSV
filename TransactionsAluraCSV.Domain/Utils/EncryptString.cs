using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TransactionsAluraCSV.Domain.Utils
{

    public class EncryptString
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            foreach(byte b in result)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(b.ToString("X2"));
            }

            return strBuilder.ToString();
        }
    }

}
