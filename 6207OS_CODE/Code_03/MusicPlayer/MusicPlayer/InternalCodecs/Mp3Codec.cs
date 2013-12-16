using System;
using System.IO;

namespace Player.Core.InternalCodecs
{
    public class Mp3Codec : ICodec
    {
        public string Name
        {
            get { return "MP3 Audio"; }
        }

        public bool CanDecode(string extension)
        {
            return extension == ".mp3";
        }

        public Stream Decode(Stream inStream)
        {
            // Codec implementation ...
            throw new NotImplementedException();
        }

    }
}
