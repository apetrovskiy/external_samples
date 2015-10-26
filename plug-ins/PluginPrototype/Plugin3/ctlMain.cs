using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PluginInterface;

namespace Plugin3
{
	/// <summary>
	/// Summary description for ctlMain.
	/// </summary>
	public class ctlMain : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butSend;
		private System.Windows.Forms.TextBox txtFeedback;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlMain()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtFeedback = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butSend = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtFeedback);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(16, 56);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 208);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Host Feedback:";
			// 
			// txtFeedback
			// 
			this.txtFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFeedback.Location = new System.Drawing.Point(8, 16);
			this.txtFeedback.Multiline = true;
			this.txtFeedback.Name = "txtFeedback";
			this.txtFeedback.Size = new System.Drawing.Size(392, 184);
			this.txtFeedback.TabIndex = 0;
			this.txtFeedback.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "Type a Message to send as Feedback to the Plugin Host Application, then click the" +
				" button to send!";
			// 
			// butSend
			// 
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.BackColor = System.Drawing.SystemColors.Control;
			this.butSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSend.Location = new System.Drawing.Point(160, 272);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(120, 32);
			this.butSend.TabIndex = 2;
			this.butSend.Text = "Send Feedback";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// ctlMain
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "ctlMain";
			this.Size = new System.Drawing.Size(448, 320);
			this.Load += new System.EventHandler(this.ctlMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		IPluginHost myHost = null;
		IPlugin myPlugin = null;

		public IPluginHost PluginHost
		{
			get {return myHost;}
			set {myHost = value;}
		}
		public IPlugin Plugin
		{
			get {return myPlugin;}
			set {myPlugin = value;}
		}

		private void butSend_Click(object sender, System.EventArgs e)
		{
			myHost.Feedback(this.txtFeedback.Text, myPlugin);
		}

		private void ctlMain_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
