using System;
using System.Reflection;
using System.Windows.Forms;
using Plugin;

namespace MyApplication
{
    public partial class MainClass : Form
    {
        public MainClass()
        {
            InitializeComponent();
        }
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Plugin Files|*.dll";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                Assembly.LoadFrom(ofd.FileName);
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type t in a.GetTypes())
                    {
                        if (t.GetInterface("MyFunctionInterface") != null)
                        {
                            try
                            {
                                MyFunctionInterface pluginclass = Activator.CreateInstance(t) as MyFunctionInterface;
                                pluginclass.doSomething();
                                //return;
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}
