using System.Windows.Forms;
using UIFactory.Core;

namespace UIFactory.Main
{
    public partial class Form1 : Form
    {
        public Form1(IUIFactory uiFactory)
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                panel.Controls.Add(uiFactory.GetCheckBox());
            }
            panel.Controls.Add(uiFactory.GetButton());
        }
    }
}
