using Ninject.Modules;
using Player.Core.InternalCodecs;

namespace Player.Core
{
public class InternalCodecsModule : NinjectModule
{
    public override void Load()
    {
        Bind<ICodec>().To<Mp3Codec>();
        Bind<ICodec>().To<WmaCodec>();
    }
}
}
