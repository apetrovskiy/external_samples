using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;

namespace UIFactory.Real
{
    public class RealFactoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Core.IUIFactory>().To<RealUIFactory>();
            Bind<Core.ButtonBase>().To<RealButton>();
            Bind<Core.CheckBoxBase>().To<RealCheckBox>();
        }
    }
}
