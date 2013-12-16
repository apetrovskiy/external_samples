using System;
using System.Collections.Generic;
using System.IO;

namespace Player.Core
{
    public class Player
    {
        private readonly IEnumerable<ICodec> codecs;

        public Player(IEnumerable<ICodec> codecs)
        {
            this.codecs = codecs;
        }

        public void Play(FileInfo fileInfo)
        {
            ICodec supportingCodec = FindCodec(fileInfo.Extension);

            using (var rawStream = fileInfo.OpenRead())
            {
                var decodedStream = supportingCodec.Decode(rawStream);
                PlayStream(decodedStream);
            }
        }

        private ICodec FindCodec(string extension)
        {
            foreach (ICodec codec in codecs)
            {
                if (codec.CanDecode(extension))
                {
                    return codec;
                }
            }
            throw new Exception("File type not supported.");
        }

        private void PlayStream(Stream stream)
        {
            // Playing decoded stream
        }
    }
}
