using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.Login
{
    public class SHA256Hash
    {
        public static string PasswordHash(string password, string salt)
        {
            System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create();
            byte[] preHash = System.Text.Encoding.UTF32.GetBytes(password + salt);
            byte[] hash = sha.ComputeHash(preHash);
            string stringy = System.Convert.ToBase64String(hash);
            return stringy;
        }
    }
}
