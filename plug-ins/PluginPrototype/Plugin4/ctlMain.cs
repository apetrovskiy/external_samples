using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PluginInterface;

namespace Plugin4
{
	/// <summary>
	/// Summary description for ctlMain.
	/// </summary>
	public class Plugin4 : System.Windows.Forms.UserControl, IPlugin
	{
		private System.Windows.Forms.Button butSend;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFeedback;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Plugin4()
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
			this.butSend = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtFeedback = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butSend
			// 
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.BackColor = System.Drawing.SystemColors.Control;
			this.butSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butSend.Location = new System.Drawing.Point(168, 336);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(120, 32);
			this.butSend.TabIndex = 5;
			this.butSend.Text = "Send Feedback";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 40);
			this.label1.TabIndex = 4;
			this.label1.Text = "Type a Message to send as Feedback to the Plugin Host Application, then click the" +
				" button to send!";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtFeedback);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 280);
			this.groupBox1.TabIndex = 3;
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
			this.txtFeedback.Size = new System.Drawing.Size(448, 256);
			this.txtFeedback.TabIndex = 0;
			this.txtFeedback.Text = "";
			// 
			// Plugin4
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "Plugin4";
			this.Size = new System.Drawing.Size(488, 392);
			this.Load += new System.EventHandler(this.ctlMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ctlMain_Load(object sender, System.EventArgs e)
		{
		
		}
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "Plugin4";
		string myPluginAuthor = "Jonathan Dick";
		string myPluginDescription = "This Combines the User Control and Plugin Class into one Class";
		string myPluginVersion = "1.0.0";
		
        
		void PluginInterface.IPlugin.Dispose()
		{
			// TODO:  Add ctlMain.PluginInterface.IPlugin.Dispose implementation
		}

		public string Description
		{
			get
			{
				// TODO:  Add ctlMain.Description getter implementation
				return myPluginDescription;
			}
		}

		public string Author
		{
			get
			{
				// TODO:  Add ctlMain.Author getter implementation
				return myPluginAuthor;
			}
		}

		public IPluginHost Host
		{
			get
			{
				// TODO:  Add ctlMain.Host getter implementation
				return myPluginHost;
			}
			set
			{
				myPluginHost = value;
			}
		}

		public void Initialize()
		{
			// TODO:  Add ctlMain.Initialize implementation
		}

		private void butSend_Click(object sender, System.EventArgs e)
		{
			this.Host.Feedback(this.txtFeedback.Text, this);
		}

			/*
		public string Name
		{
			get
			{
				// TODO:  Add ctlMain.Name getter implementation
				return myPluginName;
			}
		}
		*/
		public UserControl MainInterface
		{
			get
			{
				// TODO:  Add ctlMain.MainInterface getter implementation
				return this;
			}
		}

		public string Version
		{
			get
			{
				// TODO:  Add ctlMain.Version getter implementation
				return myPluginVersion;
			}
		}

		#endregion
	}
}
