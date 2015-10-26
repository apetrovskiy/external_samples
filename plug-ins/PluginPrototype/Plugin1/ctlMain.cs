using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Plugin1
{
	/// <summary>
	/// Summary description for ctlMain.
	/// </summary>
	public class ctlMain : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.OpenFileDialog cdbOpen;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.LinkLabel butOpenFile;
		private System.Windows.Forms.LinkLabel butSaveFile;
		private System.Windows.Forms.SaveFileDialog cdbSave;
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
			this.cdbOpen = new System.Windows.Forms.OpenFileDialog();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.butOpenFile = new System.Windows.Forms.LinkLabel();
			this.butSaveFile = new System.Windows.Forms.LinkLabel();
			this.cdbSave = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// cdbOpen
			// 
			this.cdbOpen.Filter = "Text Files|*.txt";
			this.cdbOpen.Title = "Open...";
			// 
			// txtFile
			// 
			this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFile.Location = new System.Drawing.Point(8, 16);
			this.txtFile.Multiline = true;
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(456, 328);
			this.txtFile.TabIndex = 0;
			this.txtFile.Text = "";
			// 
			// butOpenFile
			// 
			this.butOpenFile.ActiveLinkColor = System.Drawing.Color.Blue;
			this.butOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOpenFile.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.butOpenFile.Location = new System.Drawing.Point(16, 352);
			this.butOpenFile.Name = "butOpenFile";
			this.butOpenFile.Size = new System.Drawing.Size(112, 16);
			this.butOpenFile.TabIndex = 1;
			this.butOpenFile.TabStop = true;
			this.butOpenFile.Text = "Open File...";
			this.butOpenFile.VisitedLinkColor = System.Drawing.Color.Blue;
			this.butOpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.butOpenFile_LinkClicked);
			// 
			// butSaveFile
			// 
			this.butSaveFile.ActiveLinkColor = System.Drawing.Color.Blue;
			this.butSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSaveFile.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.butSaveFile.Location = new System.Drawing.Point(392, 352);
			this.butSaveFile.Name = "butSaveFile";
			this.butSaveFile.Size = new System.Drawing.Size(72, 16);
			this.butSaveFile.TabIndex = 2;
			this.butSaveFile.TabStop = true;
			this.butSaveFile.Text = "Save File...";
			this.butSaveFile.VisitedLinkColor = System.Drawing.Color.Blue;
			this.butSaveFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.butSaveFile_LinkClicked);
			// 
			// cdbSave
			// 
			this.cdbSave.Filter = "Text Files|*.txt";
			this.cdbSave.Title = "Save...";
			// 
			// ctlMain
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.butSaveFile);
			this.Controls.Add(this.butOpenFile);
			this.Controls.Add(this.txtFile);
			this.Name = "ctlMain";
			this.Size = new System.Drawing.Size(488, 376);
			this.Load += new System.EventHandler(this.ctlMain_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void butOpenFile_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			
			if (cdbOpen.ShowDialog() == DialogResult.OK)
			{
				this.txtFile.Clear();
				
				System.IO.TextReader tr = File.OpenText(cdbOpen.FileName);
				
				this.txtFile.Text = tr.ReadToEnd();
				
				tr.Close();
				tr = null;				
			}
		}

		private void butSaveFile_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (System.IO.File.Exists(cdbOpen.FileName))
			{
				cdbSave.FileName = cdbOpen.FileName;
			}
			
			if (cdbSave.ShowDialog() == DialogResult.OK)
			{
				StreamWriter sw = new FileInfo(cdbSave.FileName).CreateText();
				sw.Write(this.txtFile.Text);
				sw.Flush();
				sw.Close();
				sw = null;
			}
		}

		private void ctlMain_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
