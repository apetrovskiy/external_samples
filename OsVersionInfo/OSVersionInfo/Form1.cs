using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JCS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(String.Empty);

            sb.AppendLine("Operation System Information");
            sb.AppendLine("----------------------------");
            sb.AppendLine(String.Format("Name = {0}", OSVersionInfo.Name));
            sb.AppendLine(String.Format("Edition = {0}", OSVersionInfo.Edition));
            if (OSVersionInfo.ServicePack!=string.Empty)
                sb.AppendLine(String.Format("Service Pack = {0}", OSVersionInfo.ServicePack));
            else
                sb.AppendLine("Service Pack = None");
            sb.AppendLine(String.Format("Version = {0}", OSVersionInfo.VersionString));
            sb.AppendLine(String.Format("ProcessorBits = {0}", OSVersionInfo.ProcessorBits));
            sb.AppendLine(String.Format("OSBits = {0}", OSVersionInfo.OSBits));
            sb.AppendLine(String.Format("ProgramBits = {0}", OSVersionInfo.ProgramBits));
            
            textBox1.Text = sb.ToString();
        }
    }
}
