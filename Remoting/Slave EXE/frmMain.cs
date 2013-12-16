using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Services;


namespace Slave
{
	public class frmMain : System.Windows.Forms.Form
	{
		// namespace Remote is located in Remoting.DLL
		private Remote.cTransfer            mi_Transfer   = null;
		private ObjRef                      mi_Service    = null;
		private TcpChannel                  mi_Channel    = null;
		private bool                        mb_WaitButton = false;
		private System.Windows.Forms.Timer  mi_EraseTextTimer;

		public frmMain()
		{
			InitializeComponent();
			checkBoxListen.Checked = true; // calls StartListen()

			mi_EraseTextTimer          = new System.Windows.Forms.Timer();
			mi_EraseTextTimer.Tick    += new EventHandler(OnTimerEraseText);
			mi_EraseTextTimer.Interval = 4000;
		}

		private void textBoxPort_TextChanged(object sender, EventArgs e)
		{
			checkBoxListen.Checked = false; // calls StopListen()
		}

		private void checkBoxListen_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBoxListen.Checked) StartListen();
			else                        StopListen();
		}

		public void StopListen()
		{
			if (mi_Service != null)
				RemotingServices.Unmarshal (mi_Service);

			if (mi_Transfer != null)
				RemotingServices.Disconnect(mi_Transfer);

			if (mi_Channel != null)
				ChannelServices.UnregisterChannel(mi_Channel);

			mi_Service  = null;
			mi_Transfer = null;
			mi_Channel  = null;
		}

		public void StartListen()
		{
			StopListen(); // if there is any channel still open --> close it

			try
			{
				int s32_Port = int.Parse(textBoxPort.Text);

				mi_Channel   = new TcpChannel(s32_Port);
				ChannelServices.RegisterChannel(mi_Channel);

				mi_Transfer  = new Remote.cTransfer();
				mi_Service = RemotingServices.Marshal(mi_Transfer, "TestService");

				// define the event which is triggered when the Master calls the CallSlave() function
				mi_Transfer.ev_SlaveCall += new Remote.cTransfer.del_SlaveCall(OnMasterEvent);
			}
			catch (Exception Ex)
			{
				MessageBox.Show(this, "Error starting listening:\n" + Ex.Message, "Slave");
				checkBoxListen.Checked = false; // calls StopListen()
			}			
		}

		Remote.kResponse OnMasterEvent(Remote.kAction k_Action)
		{
			Remote.kResponse k_Response = new Remote.kResponse();

			// If multiple masters try to connect at once
			if (mb_WaitButton)
			{
				k_Response.s_Result = "Sorry! Slave is currently busy.\r\nTry again later";
				return k_Response;
			}

			textBoxMessage.Text = string.Format("Message from [{0}]:\r\n{1}\r\n(Click Button \"Send\" to answer)", 
			                      k_Action.s_Computer, k_Action.s_Command);

			mi_EraseTextTimer.Stop();

			// wait until the user has clicked the "Send" button
			mb_WaitButton = true;
			while (mb_WaitButton)
			{
				Thread.Sleep(200);
			};

			k_Response.s_Result = textBoxResponse.Text;
			return k_Response;
		}

		private void btnRespond_Click(object sender, System.EventArgs e)
		{
			if (!mb_WaitButton)
				MessageBox.Show(this, "This button has no effect until the master has sent a message!", "Slave Error");

			mb_WaitButton = false;
			mi_EraseTextTimer.Start();
		}

		private void OnTimerEraseText(Object Object, EventArgs EventArgs) 
		{
			mi_EraseTextTimer.Stop();
			textBoxMessage.Text = "Waiting...";
		}

		#region Windows Form Designer generated code

		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxResponse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRespond;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxListen;
		private System.Windows.Forms.TextBox textBoxPort;



		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxListen = new System.Windows.Forms.CheckBox();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxResponse = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnRespond = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(240, 188);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 15;
			this.label4.Text = "ElmüSoft";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(80, 24);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(48, 20);
			this.textBoxPort.TabIndex = 14;
			this.textBoxPort.Text = "1500";
			this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(80, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 13;
			this.label3.Text = "TCP Port";
			// 
			// checkBoxListen
			// 
			this.checkBoxListen.Location = new System.Drawing.Point(16, 24);
			this.checkBoxListen.Name = "checkBoxListen";
			this.checkBoxListen.Size = new System.Drawing.Size(56, 24);
			this.checkBoxListen.TabIndex = 16;
			this.checkBoxListen.Text = "Listen";
			this.checkBoxListen.CheckedChanged += new System.EventHandler(this.checkBoxListen_CheckedChanged);
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.Location = new System.Drawing.Point(16, 72);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.Size = new System.Drawing.Size(220, 48);
			this.textBoxMessage.TabIndex = 19;
			this.textBoxMessage.Text = "Waiting...";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(220, 16);
			this.label1.TabIndex = 20;
			this.label1.Text = "Received message from Master";
			// 
			// textBoxResponse
			// 
			this.textBoxResponse.Location = new System.Drawing.Point(16, 144);
			this.textBoxResponse.Multiline = true;
			this.textBoxResponse.Name = "textBoxResponse";
			this.textBoxResponse.Size = new System.Drawing.Size(220, 48);
			this.textBoxResponse.TabIndex = 1;
			this.textBoxResponse.Text = "Hello Master !";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(220, 16);
			this.label2.TabIndex = 22;
			this.label2.Text = "Send response message to Master";
			// 
			// btnRespond
			// 
			this.btnRespond.Location = new System.Drawing.Point(244, 144);
			this.btnRespond.Name = "btnRespond";
			this.btnRespond.Size = new System.Drawing.Size(44, 24);
			this.btnRespond.TabIndex = 2;
			this.btnRespond.Text = "Send";
			this.btnRespond.Click += new System.EventHandler(this.btnRespond_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 207);
			this.Controls.Add(this.btnRespond);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxResponse);
			this.Controls.Add(this.textBoxMessage);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxListen);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(400, 250);
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Slave";
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
