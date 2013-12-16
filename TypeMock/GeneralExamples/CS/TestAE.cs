/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 10/19/2013
 * Time: 12:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using System.Windows.Automation;

//namespace Typemock.Examples.CSharp
namespace Typemock.Examples.CSharp.ControllingProperties
{
    /// <summary>
    /// Description of TestAE.
    /// </summary>
    [TestClass, Isolated] //(DesignMode.Pragmatic)]
    public class TestAE
    {
        public TestAE()
        {
        }
        
        [TestMethod]
        public void FakeAE()
        {
            //var fakeDependency = new Dependency();
           
            //Isolate.WhenCalled(() => fakeDependency.Number).WillReturn(5);
            
            Isolate.WhenCalled(() => System.Windows.Automation.AutomationElement.RootElement).WillReturn(System.Windows.Automation.AutomationElement.RootElement);

            var classUnderTest = new ClassUnderTest();
            //var result = classUnderTest.SimpleCalculation(2, fakeDependency);
            var result = classUnderTest.SimpleCalculation(2, System.Windows.Automation.AutomationElement.RootElement.Current.NativeWindowHandle);

            Assert.AreEqual(3, result);
        }
    }
}
