using Ninject.Modules;

namespace UIFactory.Faked
{
    public class FakedFactoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Core.IUIFactory>().To<FakedUIFactory>();
            Bind<Core.ButtonBase>().To<FakedButton>();
            Bind<Core.CheckBoxBase>().To<FakedCheckBox>();
        }
    }
}
