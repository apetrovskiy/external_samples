using System.IO;

namespace Player.Core
{
    public interface ICodec
    {
        string Name { get; }

        bool CanDecode(string extension);

        Stream Decode(Stream inStream);
    }
}
