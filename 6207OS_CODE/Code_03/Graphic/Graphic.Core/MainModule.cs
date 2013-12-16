using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Graphic.Core
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IShapeFactory>().ToFactory();
        }
    }
}
