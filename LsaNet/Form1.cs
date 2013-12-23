using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Security.Principal; //contains WindowsImpersonationContext

namespace LogonDemo
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnAddPriv;
		private System.Windows.Forms.TextBox txtAccountName;
		private System.Windows.Forms.ComboBox cmbPrivilege;
		private System.Windows.Forms.LinkLabel lblDocLink;
		private System.Windows.Forms.Button btnImpersonate;
		private System.Windows.Forms.Button btnImpUndo;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>Cpntains the current Impersonation Context, if it has changed</summary>
		private WindowsImpersonationContext impContext = null;

		public Form1()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnAddPriv = new System.Windows.Forms.Button();
			this.txtAccountName = new System.Windows.Forms.TextBox();
			this.cmbPrivilege = new System.Windows.Forms.ComboBox();
			this.lblDocLink = new System.Windows.Forms.LinkLabel();
			this.btnImpersonate = new System.Windows.Forms.Button();
			this.btnImpUndo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnAddPriv
			// 
			this.btnAddPriv.Location = new System.Drawing.Point(24, 104);
			this.btnAddPriv.Name = "btnAddPriv";
			this.btnAddPriv.Size = new System.Drawing.Size(176, 23);
			this.btnAddPriv.TabIndex = 0;
			this.btnAddPriv.Text = "Add Privilege to Administrator";
			this.btnAddPriv.Click += new System.EventHandler(this.btnAddPriv_Click);
			// 
			// txtAccountName
			// 
			this.txtAccountName.Location = new System.Drawing.Point(24, 16);
			this.txtAccountName.Name = "txtAccountName";
			this.txtAccountName.Size = new System.Drawing.Size(280, 20);
			this.txtAccountName.TabIndex = 1;
			this.txtAccountName.Text = "Administrator";
			this.txtAccountName.TextChanged += new System.EventHandler(this.txtAccountName_TextChanged);
			// 
			// cmbPrivilege
			// 
			this.cmbPrivilege.Items.AddRange(new object[] {
															  "SeServiceLogonRight",
															  "SeTcbName",
															  "SeSystemtimePrivilege",
															  "SeDebugName"});
			this.cmbPrivilege.Location = new System.Drawing.Point(24, 48);
			this.cmbPrivilege.Name = "cmbPrivilege";
			this.cmbPrivilege.Size = new System.Drawing.Size(280, 21);
			this.cmbPrivilege.TabIndex = 2;
			this.cmbPrivilege.Text = "SeServiceLogonRight";
			// 
			// lblDocLink
			// 
			this.lblDocLink.LinkArea = new System.Windows.Forms.LinkArea(38, 17);
			this.lblDocLink.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.lblDocLink.Location = new System.Drawing.Point(24, 80);
			this.lblDocLink.Name = "lblDocLink";
			this.lblDocLink.Size = new System.Drawing.Size(296, 24);
			this.lblDocLink.TabIndex = 3;
			this.lblDocLink.TabStop = true;
			this.lblDocLink.Text = "For all privileges see WinNT.h or the SDK documentation";
			this.lblDocLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDocLink_LinkClicked);
			// 
			// btnImpersonate
			// 
			this.btnImpersonate.Location = new System.Drawing.Point(24, 152);
			this.btnImpersonate.Name = "btnImpersonate";
			this.btnImpersonate.Size = new System.Drawing.Size(176, 23);
			this.btnImpersonate.TabIndex = 0;
			this.btnImpersonate.Text = "Impersonate Administrator";
			this.btnImpersonate.Click += new System.EventHandler(this.btnImpersonate_Click);
			// 
			// btnImpUndo
			// 
			this.btnImpUndo.Enabled = false;
			this.btnImpUndo.Location = new System.Drawing.Point(24, 184);
			this.btnImpUndo.Name = "btnImpUndo";
			this.btnImpUndo.Size = new System.Drawing.Size(176, 23);
			this.btnImpUndo.TabIndex = 0;
			this.btnImpUndo.Text = "Be yourself";
			this.btnImpUndo.Click += new System.EventHandler(this.btnImpUndo_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(384, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblDocLink,
																		  this.cmbPrivilege,
																		  this.txtAccountName,
																		  this.btnAddPriv,
																		  this.btnImpersonate,
																		  this.btnImpUndo});
			this.Name = "Form1";
			this.Text = "Using LSA Functions";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			//LsaUtility.SetRight("Tester", "SeServiceLogonRight");
			//context.Undo();
		}

		private void btnAddPriv_Click(object sender, System.EventArgs e) {
			if(txtAccountName.Text != String.Empty){
				long result = LsaUtility.SetRight(txtAccountName.Text, cmbPrivilege.Text);
				if(result == 0){
					MessageBox.Show("Privilege added");
				}else{
				    MessageBox.Show("Privilege not added: +winErrorCode: " + result.ToString());
				}
			}
		}

		private void lblDocLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("ms-help://MS.VSCC/MS.MSDNVS.1031/security/accctrl_96lv.htm");
		}

		private void btnImpersonate_Click(object sender, System.EventArgs e) {
			if(txtAccountName.Text != String.Empty){
				InputBox dlg = new InputBox(txtAccountName.Text);
				if( dlg.ShowDialog(this) == DialogResult.OK){
					impContext = LogonUtility.ImpersonateUser(
						txtAccountName.Text,
						dlg.Password);
					btnImpUndo.Enabled = true;
					btnImpersonate.Enabled = false;
				}
			}
		}

		private void btnImpUndo_Click(object sender, System.EventArgs e) {
			impContext.Undo();
			btnImpUndo.Enabled = false;
			btnImpersonate.Enabled = true;
		}

		private void txtAccountName_TextChanged(object sender, System.EventArgs e) {
			btnAddPriv.Text = "AddPrivilege to "+txtAccountName.Text;
			btnImpersonate.Text = "Impersonate "+txtAccountName.Text;
		}
	}
}
