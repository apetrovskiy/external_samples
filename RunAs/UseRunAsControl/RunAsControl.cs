using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using VastAbyss;

namespace VastAbyss.Controls.WinForms.General
{

	/// <summary>
	/// Error is the Win32Exception.Message thrown.
	/// </summary>
	public delegate void ProcessFailedEventHandler(string error);
	/// <summary>
	/// Process is the ID of the process that exited.
	/// </summary>
	public delegate void ProcessEndedEventHandler(int process);
	/// <summary>
	/// Process is the ID of the process that started.
	/// </summary>
	public delegate void ProcessStartedEventHandler(int process);
	/// <summary>
	/// Error is the Win32Exception.Message thrown.
	/// </summary>
	public delegate void ProcessAccessFailedEventHandler(string error);

	/// <summary>
	/// Control that makes use of VastAbyss.RunAs.StartProcess() to create a process
	///  under different user credentials.
	/// </summary>
	public class RunAsControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RunAsControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		/// <summary>
		/// ProcessFailed is fired if an exception is thrown when attempting to start the
		///  process.
		/// </summary>
		public event ProcessFailedEventHandler ProcessFailed;
		/// <summary>
		/// ProcessEnded is fired where the started process exits.
		/// </summary>
		public event ProcessEndedEventHandler ProcessEnded;
		/// <summary>
		/// ProcessStarted is fired if the process successfully starts.
		/// </summary>
		public event ProcessStartedEventHandler ProcessStarted;
		/// <summary>
		/// ProcessAccessFailed is fired if the current user does not have permission to
		///  access the new process.
		/// </summary>
		public event ProcessAccessFailedEventHandler ProcessAccessFailed;

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.textBox4);
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 160);
			this.panel1.TabIndex = 0;
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(8, 104);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(64, 23);
			this.button2.TabIndex = 29;
			this.button2.Text = "Command...";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(72, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(184, 20);
			this.textBox1.TabIndex = 26;
			this.textBox1.Text = "";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(72, 104);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(184, 20);
			this.textBox4.TabIndex = 30;
			this.textBox4.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(72, 72);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(184, 20);
			this.textBox3.TabIndex = 28;
			this.textBox3.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(72, 40);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(184, 20);
			this.textBox2.TabIndex = 27;
			this.textBox2.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 34;
			this.label3.Text = "Password";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 33;
			this.label2.Text = "Domain";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 31;
			this.label1.Text = "User Name";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(72, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 24);
			this.button1.TabIndex = 32;
			this.button1.Text = "Run Command";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// RunAsControl
			// 
			this.Controls.Add(this.panel1);
			this.Name = "RunAsControl";
			this.Size = new System.Drawing.Size(264, 160);
			this.Load += new System.EventHandler(this.RunAsControl_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Diagnostics.Process proc = RunAs.StartProcess(textBox1.Text,
					textBox2.Text, textBox3.Text, textBox4.Text);
				afterProcessStart(ref proc);
			}
			catch (System.ComponentModel.Win32Exception w32e)
			{
				ProcessFailed(w32e.Message);
			}
		}

		private void afterProcessStart(ref System.Diagnostics.Process proc)
		{
			ProcessStarted(proc.Id);
			try
			{
				proc.EnableRaisingEvents = true;
				proc.Exited += new EventHandler(m_process_Exited);
			}
			catch (Exception e)
			{
				ProcessAccessFailed(e.Message);
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog m_openfile = new OpenFileDialog();
			if (m_openfile.ShowDialog(this) == DialogResult.OK)
			{
				textBox4.Text = m_openfile.FileName;
			}
			m_openfile.Dispose();
		}

		private void m_process_Exited(object sender, EventArgs e)
		{
			ProcessEnded(((System.Diagnostics.Process)sender).Id);
		}

		private void RunAsControl_Load(object sender, System.EventArgs e)
		{
			textBox1.Text = System.Environment.UserName;
			textBox2.Text = System.Environment.UserDomainName;
			textBox3.PasswordChar = '\u25CF';
		}
	}
}
