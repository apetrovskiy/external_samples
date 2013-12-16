using System.Linq;

namespace Samples.Encryption
{
    public class ReverseEncryptor : IEncryptor
    {
        public string Encrypt(string str)
        {
            var charArray = str.Reverse().ToArray();
            return new string(charArray);
        }
    }
}