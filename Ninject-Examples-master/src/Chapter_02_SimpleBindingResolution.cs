using Ninject;
using NUnit.Core;
using NUnit.Framework;


namespace NinjectExamples
{
    [TestFixture]
    public class Chapter_02_SimpleBindingResolution
    {
        [Test]
        public void GetInterfaceImplimentation()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IWeapon>().To<Sword>();

            Assert.That(kernel.Get<IWeapon>(), Is.InstanceOf<Sword>());
        }

        [Test]
        public void GetAbstractImplimentation()
        {
            var kernel = new StandardKernel();
            kernel.Bind<HandWeapons>().To<Sword>();

            Assert.That(kernel.Get<HandWeapons>(), Is.InstanceOf<Sword>());
        }

        [Test]
        public void SelfBindingTypesDoNotNeedExplicitBindings()
        {
            var kernel = new StandardKernel();
            Assert.That(kernel.Get<Sword>(), Is.InstanceOf<Sword>());
        }

        [Test]
        public void CanCreateExplicitSelfBinds()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Sword>().ToSelf();
            Assert.That(kernel.Get<Sword>(), Is.InstanceOf<Sword>());
        }

        [Test]
        public void CanBindValueTypesToConstants()
        {
            var kernel = new StandardKernel();
            kernel.Bind<string>().ToConstant("Hello Dojo");
            Assert.That(kernel.Get<string>(), Is.EqualTo("Hello Dojo"));
        }

        public interface IWeapon{}  

        public abstract class HandWeapons {}

        public class Sword : HandWeapons, IWeapon {}

        
    }
}