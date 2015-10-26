using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Plugin2
{
	/// <summary>
	/// Summary description for ctlMain.
	/// </summary>
	public class ctlMain : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button butMain;
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
			this.butMain = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butMain
			// 
			this.butMain.BackColor = System.Drawing.Color.Gainsboro;
			this.butMain.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.butMain.Location = new System.Drawing.Point(264, 224);
			this.butMain.Name = "butMain";
			this.butMain.Size = new System.Drawing.Size(88, 32);
			this.butMain.TabIndex = 0;
			this.butMain.Text = "&Click Me!";
			this.butMain.Click += new System.EventHandler(this.butMain_Click);
			// 
			// ctlMain
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.butMain);
			this.Name = "ctlMain";
			this.Size = new System.Drawing.Size(664, 512);
			this.Resize += new System.EventHandler(this.ctlMain_Resize);
			this.ResumeLayout(false);

		}
		#endregion
		int newH = 0;
		int newV = 0;
			
		Random rndH = new Random(20);
		Random rndV = new Random(20);
		
		private void MoveButton()
		{
			newH = rndH.Next(this.Width - 40);
			
			
			newV = rndV.Next(this.Height - 40);
			
			this.butMain.Top = newV;
			this.butMain.Left = newH;
		}
		
		private void butMain_Click(object sender, System.EventArgs e)
		{
			this.MoveButton();			
		}

		private void ctlMain_Resize(object sender, System.EventArgs e)
		{
			this.MoveButton();
		}
	}
}
