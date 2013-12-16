using System;
using System.IO;

namespace Player.Core.InternalCodecs
{
    public class WmaCodec : ICodec
    {
        public string Name
        {
            get { return "Windows Media Auido"; }
        }

        public bool CanDecode(string extension)
        {
            return extension == ".wma";
        }

        public Stream Decode(Stream inStream)
        {
            // Codec implementation ...
            throw new NotImplementedException();
        }

    }
}
