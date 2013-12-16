using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ninject;
using Player.Core;

namespace Player.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new InternalCodecsModule());
            kernel.Load("plugins/*.dll");
            var player = kernel.Get<Core.Player>();
            player.Play(new FileInfo(@"c:\music.mp3"));

            //IEnumerable<ICodec> codecs = kernel.GetAll<ICodec>();
            //foreach (ICodec codec in codecs)
            //{
            //    System.Console.WriteLine(codec.Name);
            //}
        }
    }
}
