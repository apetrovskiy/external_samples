using System.Drawing;
using System.Windows.Forms;

namespace TestStack.White.Utility
{
    public class MonitorForm : Form
    {
        public MonitorForm()
        {
            Size = new Size(1, 1);
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            return base.ProcessKeyMessage(ref m);
        }
    }
}