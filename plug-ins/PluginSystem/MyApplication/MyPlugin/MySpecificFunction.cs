using System.Windows.Forms;
using Plugin;

namespace MyPlugin
{
    public class MySpecificFunction : MyFunctionInterface
    {
        public MySpecificFunction()
        {
        }
        public void doSomething()
        {
            MessageBox.Show("Hello World!");
        }
    }
}
