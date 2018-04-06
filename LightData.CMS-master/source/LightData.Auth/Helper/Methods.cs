using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightData.Auth.Helper
{
    public static class Methods
    {
        public static string Encode(string password)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(password ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}