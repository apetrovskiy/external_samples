using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using VastAbyss.Controls.WinForms.General;

namespace VastAbyss
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private RunAsControl rac;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// 
			// rac
			//
			this.rac = new RunAsControl();
			this.rac.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rac.Location = new System.Drawing.Point(0, 0);
			this.rac.Name = "rac";
			this.rac.Size = new System.Drawing.Size(264, 158);
			this.rac.TabIndex = 0;
			this.rac.ProcessStarted += new ProcessStartedEventHandler(this.rac_ProcessStarted);
			this.rac.ProcessFailed += new ProcessFailedEventHandler(this.rac_ProcessFailed);
			this.rac.ProcessEnded += new ProcessEndedEventHandler(this.rac_ProcessEnded);
			this.rac.ProcessAccessFailed += new ProcessAccessFailedEventHandler(rac_ProcessAccessFailed);
			this.Controls.Add(this.rac);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(264, 158);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(270, 190);
			this.Name = "Form1";
			this.Text = "RunAs...";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new Form1());
		}

		private void rac_ProcessEnded(int process)
		{
			MessageBox.Show(this, "Process " + process.ToString() + " ended.", "Process Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void rac_ProcessFailed(string error)
		{
			MessageBox.Show(this, error, "Process Failed to Start",MessageBoxButtons.OK, MessageBoxIcon.Error); 
		}

		private void rac_ProcessStarted(int process)
		{
			MessageBox.Show(this, "Process " + process.ToString() + " started successfully.", "Process Started", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void rac_ProcessAccessFailed(string error)
		{
			MessageBox.Show(this, error, "Process Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
