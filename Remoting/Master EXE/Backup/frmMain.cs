using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Services;

namespace Master
{
	public class frmMain : System.Windows.Forms.Form
	{
		// namespace Remote is located in Remoting.DLL
		private Remote.cTransfer            mi_Transfer = null;
		private System.Windows.Forms.Timer  mi_EraseTextTimer;

		public frmMain()
		{
			InitializeComponent();

			mi_EraseTextTimer          = new System.Windows.Forms.Timer();
			mi_EraseTextTimer.Tick    += new EventHandler(OnTimerEraseText);
			mi_EraseTextTimer.Interval = 4000;
		}

		private void OnBtnSendClick(object sender, System.EventArgs e)
		{
			Remote.kAction k_Action = new Remote.kAction();
			k_Action.s_Command  = textBoxMessage.Text;
			k_Action.s_Computer = Environment.MachineName;

			this.Cursor = Cursors.WaitCursor;

			string s_URL = string.Format("tcp://{0}:{1}/TestService", textBoxComputer.Text, textBoxPort.Text);

			try
			{
				mi_Transfer = (Remote.cTransfer) Activator.GetObject(typeof(Remote.cTransfer), s_URL);

				// triggers the event mi_Transfer.ev_SlaveCall in the Slave
				Remote.kResponse k_Response = mi_Transfer.CallSlave(k_Action);

				textBoxAnswer.Text = "Answer from Slave:\r\n" + k_Response.s_Result;
			}
			catch (Exception Ex)
			{
				MessageBox.Show(this, "Error sending message to Slave:\n" + Ex.Message, "Master Error");
			}

			this.Cursor = Cursors.Arrow;

			mi_EraseTextTimer.Start();
		}

		private void OnTimerEraseText(Object Object, EventArgs EventArgs) 
		{
			mi_EraseTextTimer.Stop();
			textBoxAnswer.Text = "Waiting...";
		}

		#region Windows Form Designer generated code

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxComputer;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.TextBox textBoxAnswer;
		private System.Windows.Forms.Label label6;
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxComputer = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxAnswer = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Send message to Slave";
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Location = new System.Drawing.Point(16, 76);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(220, 48);
			this.textBoxMessage.TabIndex = 1;
			this.textBoxMessage.Text = "Hello Slave !";
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(244, 76);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(44, 24);
			this.btnSend.TabIndex = 2;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.OnBtnSendClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Slave Computer name or IP";
			// 
			// textBoxComputer
			// 
			this.textBoxComputer.Location = new System.Drawing.Point(16, 24);
			this.textBoxComputer.Name = "textBoxComputer";
			this.textBoxComputer.Size = new System.Drawing.Size(136, 20);
			this.textBoxComputer.TabIndex = 4;
			this.textBoxComputer.Text = "localhost";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(180, 24);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(56, 20);
			this.textBoxPort.TabIndex = 6;
			this.textBoxPort.Text = "1500";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(180, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "TCP Port";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(240, 188);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "ElmüSoft";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textBoxAnswer
			// 
			this.textBoxAnswer.Location = new System.Drawing.Point(16, 148);
			this.textBoxAnswer.Multiline = true;
			this.textBoxAnswer.Name = "textBoxAnswer";
			this.textBoxAnswer.Size = new System.Drawing.Size(220, 48);
			this.textBoxAnswer.TabIndex = 12;
			this.textBoxAnswer.Text = "Waiting...";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 132);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(192, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "Answer message from Slave";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 207);
			this.Controls.Add(this.textBoxAnswer);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.textBoxComputer);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(50, 250);
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Master";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		#endregion
	}
}
