using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Host
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel statusBarPanel;
		private System.Windows.Forms.TreeView tvwPlugins;
		private System.Windows.Forms.Panel pnlPlugin;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblPluginName;
		private System.Windows.Forms.Label lblPluginVersion;
		private System.Windows.Forms.Label lblPluginAuthor;
		private System.Windows.Forms.Label lblPluginDesc;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Application.EnableVisualStyles();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
			this.tvwPlugins = new System.Windows.Forms.TreeView();
			this.pnlPlugin = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblPluginDesc = new System.Windows.Forms.Label();
			this.lblPluginAuthor = new System.Windows.Forms.Label();
			this.lblPluginVersion = new System.Windows.Forms.Label();
			this.lblPluginName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Exit";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 326);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.statusBarPanel});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(567, 24);
			this.statusBar.TabIndex = 1;
			// 
			// statusBarPanel
			// 
			this.statusBarPanel.Width = 10000;
			// 
			// tvwPlugins
			// 
			this.tvwPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tvwPlugins.FullRowSelect = true;
			this.tvwPlugins.ImageIndex = -1;
			this.tvwPlugins.Location = new System.Drawing.Point(8, 8);
			this.tvwPlugins.Name = "tvwPlugins";
			this.tvwPlugins.SelectedImageIndex = -1;
			this.tvwPlugins.ShowLines = false;
			this.tvwPlugins.ShowPlusMinus = false;
			this.tvwPlugins.ShowRootLines = false;
			this.tvwPlugins.Size = new System.Drawing.Size(184, 171);
			this.tvwPlugins.TabIndex = 2;
			this.tvwPlugins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwPlugins_AfterSelect);
			// 
			// pnlPlugin
			// 
			this.pnlPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlugin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPlugin.Location = new System.Drawing.Point(200, -8);
			this.pnlPlugin.Name = "pnlPlugin";
			this.pnlPlugin.Size = new System.Drawing.Size(383, 339);
			this.pnlPlugin.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.lblPluginDesc);
			this.groupBox1.Controls.Add(this.lblPluginAuthor);
			this.groupBox1.Controls.Add(this.lblPluginVersion);
			this.groupBox1.Controls.Add(this.lblPluginName);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(8, 187);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(184, 136);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Plugin Information:";
			// 
			// lblPluginDesc
			// 
			this.lblPluginDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPluginDesc.Location = new System.Drawing.Point(8, 64);
			this.lblPluginDesc.Name = "lblPluginDesc";
			this.lblPluginDesc.Size = new System.Drawing.Size(168, 64);
			this.lblPluginDesc.TabIndex = 3;
			this.lblPluginDesc.Text = "   Plugin Description Goes Here... Test One Two Three, This is a Test...";
			// 
			// lblPluginAuthor
			// 
			this.lblPluginAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPluginAuthor.Location = new System.Drawing.Point(8, 48);
			this.lblPluginAuthor.Name = "lblPluginAuthor";
			this.lblPluginAuthor.Size = new System.Drawing.Size(168, 16);
			this.lblPluginAuthor.TabIndex = 2;
			this.lblPluginAuthor.Text = "By: <Author\'s Name>";
			// 
			// lblPluginVersion
			// 
			this.lblPluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPluginVersion.Location = new System.Drawing.Point(8, 32);
			this.lblPluginVersion.Name = "lblPluginVersion";
			this.lblPluginVersion.Size = new System.Drawing.Size(168, 16);
			this.lblPluginVersion.TabIndex = 1;
			this.lblPluginVersion.Text = "(<Version>)";
			// 
			// lblPluginName
			// 
			this.lblPluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPluginName.Location = new System.Drawing.Point(8, 16);
			this.lblPluginName.Name = "lblPluginName";
			this.lblPluginName.Size = new System.Drawing.Size(168, 16);
			this.lblPluginName.TabIndex = 0;
			this.lblPluginName.Text = "<Plugin Name Here>";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(567, 350);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.tvwPlugins);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pnlPlugin);
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(575, 400);
			this.Name = "frmMain";
			this.Text = "Plugins";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			//Call the find plugins routine, to search in our Plugins Folder
			Global.Plugins.FindPlugins(Application.StartupPath + @"\Plugins");
			
			//Add each plugin to the treeview
			foreach (Types.AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
			{
				TreeNode newNode = new TreeNode(pluginOn.Instance.Name);
				this.tvwPlugins.Nodes.Add(newNode);
				newNode = null;
			}
		}

		private void tvwPlugins_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			//Make sure there's a selected Plugin
			if (this.tvwPlugins.SelectedNode != null)
			{
				//Get the selected Plugin
				Types.AvailablePlugin selectedPlugin = Global.Plugins.AvailablePlugins.Find(tvwPlugins.SelectedNode.Text.ToString());
				
				if (selectedPlugin != null)
				{
					//Again, if the plugin is found, do some work...
					
					
					//This part adds the plugin's info to the 'Plugin Information:' Frame
					this.lblPluginName.Text = selectedPlugin.Instance.Name;
					this.lblPluginVersion.Text = "(" + selectedPlugin.Instance.Version + ")";					
					this.lblPluginAuthor.Text = "By: " + selectedPlugin.Instance.Author;
					this.lblPluginDesc.Text = selectedPlugin.Instance.Description;
					
					//Clear the current panel of any other plugin controls... 
					//Note: this only affects visuals.. doesn't close the instance of the plugin
					this.pnlPlugin.Controls.Clear();
					
					//Set the dockstyle of the plugin to fill, to fill up the space provided
					selectedPlugin.Instance.MainInterface.Dock = DockStyle.Fill;
					
					//Finally, add the usercontrol to the panel... Tadah!
					this.pnlPlugin.Controls.Add(selectedPlugin.Instance.MainInterface);
					
				}
			
			}
		}
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Close(); //User clicked File > Exit
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Lets call the close for all the plugins before we truly exit!
			Global.Plugins.ClosePlugins();
		}
	}
}
