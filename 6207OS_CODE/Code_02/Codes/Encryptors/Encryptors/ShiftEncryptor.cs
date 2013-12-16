using System.Linq;

namespace Samples.Encryption
{
    public class ShiftEncryptor : IEncryptor
    {
        public string Encrypt(string str)
        {
            var charArray = str.Select(c => (char)(c + 1)).ToArray();
            return new string(charArray);
        }
    }
}