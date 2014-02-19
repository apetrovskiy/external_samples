﻿namespace SwdPageRecorder.UI
{
    partial class SwdMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwdMainView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnBrowser_Go = new System.Windows.Forms.Button();
            this.txtBrowserUrl = new System.Windows.Forms.TextBox();
            this.pnlLoadingBar = new System.Windows.Forms.Panel();
            this.lblLoadingInProgress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.browserSettingsTab1 = new SwdPageRecorder.UI.BrowserSettingsTabView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpSwitchTo = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlFrames = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlWindows = new System.Windows.Forms.ComboBox();
            this.grpVisualSearch = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartVisualSearch = new System.Windows.Forms.Button();
            this.txtVisualSearchResult = new System.Windows.Forms.TextBox();
            this.selectorsEditView = new SwdPageRecorder.UI.SelectorsEditView();
            this.htmlDomTesterView1 = new SwdPageRecorder.UI.HtmlDomTesterView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pageObjectSourceCodeView1 = new SwdPageRecorder.UI.PageObjectSourceCodeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.fullHtmlSourceTabView1 = new SwdPageRecorder.UI.FullHtmlSourceTabView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.javaScriptEditorView1 = new SwdPageRecorder.UI.JavaScriptEditorView();
            this.pageObjectDefinitionView = new SwdPageRecorder.UI.PageObjectDefinitionView();
            this.groupBox1.SuspendLayout();
            this.pnlLoadingBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grpSwitchTo.SuspendLayout();
            this.grpVisualSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.btnBrowser_Go);
            this.groupBox1.Controls.Add(this.txtBrowserUrl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(990, 39);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Browser";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(803, 23);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(181, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Visit project\'s home: SWD-Tools.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnBrowser_Go
            // 
            this.btnBrowser_Go.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser_Go.Location = new System.Drawing.Point(650, 13);
            this.btnBrowser_Go.Name = "btnBrowser_Go";
            this.btnBrowser_Go.Size = new System.Drawing.Size(46, 23);
            this.btnBrowser_Go.TabIndex = 1;
            this.btnBrowser_Go.Text = "Go >";
            this.btnBrowser_Go.UseVisualStyleBackColor = true;
            this.btnBrowser_Go.Click += new System.EventHandler(this.btnBrowser_Go_Click);
            // 
            // txtBrowserUrl
            // 
            this.txtBrowserUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBrowserUrl.Location = new System.Drawing.Point(6, 13);
            this.txtBrowserUrl.Name = "txtBrowserUrl";
            this.txtBrowserUrl.Size = new System.Drawing.Size(637, 20);
            this.txtBrowserUrl.TabIndex = 0;
            this.txtBrowserUrl.Text = "http://swd-tools.com";
            this.txtBrowserUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBrowserUrl_KeyUp);
            // 
            // pnlLoadingBar
            // 
            this.pnlLoadingBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLoadingBar.BackColor = System.Drawing.Color.Black;
            this.pnlLoadingBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLoadingBar.Controls.Add(this.lblLoadingInProgress);
            this.pnlLoadingBar.Controls.Add(this.pictureBox1);
            this.pnlLoadingBar.Location = new System.Drawing.Point(874, 2);
            this.pnlLoadingBar.Name = "pnlLoadingBar";
            this.pnlLoadingBar.Size = new System.Drawing.Size(139, 22);
            this.pnlLoadingBar.TabIndex = 3;
            this.pnlLoadingBar.Visible = false;
            // 
            // lblLoadingInProgress
            // 
            this.lblLoadingInProgress.AutoSize = true;
            this.lblLoadingInProgress.BackColor = System.Drawing.Color.Black;
            this.lblLoadingInProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoadingInProgress.ForeColor = System.Drawing.Color.White;
            this.lblLoadingInProgress.Location = new System.Drawing.Point(24, 2);
            this.lblLoadingInProgress.Name = "lblLoadingInProgress";
            this.lblLoadingInProgress.Size = new System.Drawing.Size(98, 16);
            this.lblLoadingInProgress.TabIndex = 1;
            this.lblLoadingInProgress.Text = "In progress...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SwdPageRecorder.UI.Properties.Resources.loading_indicator;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(12, 57);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pageObjectDefinitionView);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 430);
            this.splitContainer1.SplitterDistance = 748;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(748, 430);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.browserSettingsTab1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(740, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Browser Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // browserSettingsTab1
            // 
            this.browserSettingsTab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browserSettingsTab1.Location = new System.Drawing.Point(6, 6);
            this.browserSettingsTab1.Name = "browserSettingsTab1";
            this.browserSettingsTab1.Size = new System.Drawing.Size(728, 392);
            this.browserSettingsTab1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpSwitchTo);
            this.tabPage2.Controls.Add(this.grpVisualSearch);
            this.tabPage2.Controls.Add(this.selectorsEditView);
            this.tabPage2.Controls.Add(this.htmlDomTesterView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(740, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Locators";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpSwitchTo
            // 
            this.grpSwitchTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSwitchTo.Controls.Add(this.btnRefresh);
            this.grpSwitchTo.Controls.Add(this.label4);
            this.grpSwitchTo.Controls.Add(this.ddlFrames);
            this.grpSwitchTo.Controls.Add(this.label3);
            this.grpSwitchTo.Controls.Add(this.ddlWindows);
            this.grpSwitchTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpSwitchTo.Location = new System.Drawing.Point(418, 184);
            this.grpSwitchTo.Name = "grpSwitchTo";
            this.grpSwitchTo.Size = new System.Drawing.Size(315, 76);
            this.grpSwitchTo.TabIndex = 5;
            this.grpSwitchTo.TabStop = false;
            this.grpSwitchTo.Text = "SwitchTo (Search Context)";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(259, 44);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(54, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Frame:";
            // 
            // ddlFrames
            // 
            this.ddlFrames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFrames.FormattingEnabled = true;
            this.ddlFrames.Location = new System.Drawing.Point(61, 45);
            this.ddlFrames.Name = "ddlFrames";
            this.ddlFrames.Size = new System.Drawing.Size(192, 21);
            this.ddlFrames.TabIndex = 2;
            this.ddlFrames.SelectedIndexChanged += new System.EventHandler(this.ddlFrames_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Window:";
            // 
            // ddlWindows
            // 
            this.ddlWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWindows.FormattingEnabled = true;
            this.ddlWindows.Location = new System.Drawing.Point(61, 18);
            this.ddlWindows.Name = "ddlWindows";
            this.ddlWindows.Size = new System.Drawing.Size(248, 21);
            this.ddlWindows.TabIndex = 0;
            this.ddlWindows.SelectedIndexChanged += new System.EventHandler(this.ddlWindows_SelectedIndexChanged);
            // 
            // grpVisualSearch
            // 
            this.grpVisualSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpVisualSearch.Controls.Add(this.pictureBox2);
            this.grpVisualSearch.Controls.Add(this.label2);
            this.grpVisualSearch.Controls.Add(this.label1);
            this.grpVisualSearch.Controls.Add(this.btnStartVisualSearch);
            this.grpVisualSearch.Controls.Add(this.txtVisualSearchResult);
            this.grpVisualSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpVisualSearch.Location = new System.Drawing.Point(6, 184);
            this.grpVisualSearch.Name = "grpVisualSearch";
            this.grpVisualSearch.Size = new System.Drawing.Size(406, 76);
            this.grpVisualSearch.TabIndex = 4;
            this.grpVisualSearch.TabStop = false;
            this.grpVisualSearch.Text = "WebBrowser Web Element Explorer";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Use Ctrl + RightClick to invoke WebElement properties form";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tip: In the Web-Browser: Use Ctrl to highlight elements. ";
            // 
            // btnStartVisualSearch
            // 
            this.btnStartVisualSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartVisualSearch.Location = new System.Drawing.Point(325, 18);
            this.btnStartVisualSearch.Name = "btnStartVisualSearch";
            this.btnStartVisualSearch.Size = new System.Drawing.Size(75, 23);
            this.btnStartVisualSearch.TabIndex = 1;
            this.btnStartVisualSearch.Text = "Start";
            this.btnStartVisualSearch.UseVisualStyleBackColor = true;
            this.btnStartVisualSearch.Click += new System.EventHandler(this.btnStartVisualSearch_Click);
            // 
            // txtVisualSearchResult
            // 
            this.txtVisualSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVisualSearchResult.Location = new System.Drawing.Point(6, 20);
            this.txtVisualSearchResult.Name = "txtVisualSearchResult";
            this.txtVisualSearchResult.Size = new System.Drawing.Size(313, 20);
            this.txtVisualSearchResult.TabIndex = 0;
            // 
            // selectorsEditView
            // 
            this.selectorsEditView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectorsEditView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectorsEditView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectorsEditView.Location = new System.Drawing.Point(3, 2);
            this.selectorsEditView.Name = "selectorsEditView";
            this.selectorsEditView.Size = new System.Drawing.Size(731, 179);
            this.selectorsEditView.TabIndex = 6;
            // 
            // htmlDomTesterView1
            // 
            this.htmlDomTesterView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlDomTesterView1.Location = new System.Drawing.Point(6, 260);
            this.htmlDomTesterView1.Name = "htmlDomTesterView1";
            this.htmlDomTesterView1.Size = new System.Drawing.Size(727, 145);
            this.htmlDomTesterView1.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pageObjectSourceCodeView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(740, 404);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Source Code";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pageObjectSourceCodeView1
            // 
            this.pageObjectSourceCodeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageObjectSourceCodeView1.Location = new System.Drawing.Point(2, 0);
            this.pageObjectSourceCodeView1.Name = "pageObjectSourceCodeView1";
            this.pageObjectSourceCodeView1.Size = new System.Drawing.Size(732, 398);
            this.pageObjectSourceCodeView1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.fullHtmlSourceTabView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(740, 404);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "HTML Source";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // fullHtmlSourceTabView1
            // 
            this.fullHtmlSourceTabView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullHtmlSourceTabView1.Location = new System.Drawing.Point(6, 6);
            this.fullHtmlSourceTabView1.Name = "fullHtmlSourceTabView1";
            this.fullHtmlSourceTabView1.Size = new System.Drawing.Size(728, 392);
            this.fullHtmlSourceTabView1.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.javaScriptEditorView1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(740, 404);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "JavaScript Snippets";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // javaScriptEditorView1
            // 
            this.javaScriptEditorView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.javaScriptEditorView1.Location = new System.Drawing.Point(2, 3);
            this.javaScriptEditorView1.Name = "javaScriptEditorView1";
            this.javaScriptEditorView1.Size = new System.Drawing.Size(732, 395);
            this.javaScriptEditorView1.TabIndex = 0;
            // 
            // pageObjectDefinitionView
            // 
            this.pageObjectDefinitionView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageObjectDefinitionView.Location = new System.Drawing.Point(3, 3);
            this.pageObjectDefinitionView.Name = "pageObjectDefinitionView";
            this.pageObjectDefinitionView.Size = new System.Drawing.Size(244, 420);
            this.pageObjectDefinitionView.TabIndex = 2;
            // 
            // SwdMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 492);
            this.Controls.Add(this.pnlLoadingBar);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SwdMainView";
            this.Text = "SWD Page Recorder v0.1 beta 03 (January 11, 2014)";
            this.Shown += new System.EventHandler(this.SwdMainView_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlLoadingBar.ResumeLayout(false);
            this.pnlLoadingBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.grpSwitchTo.ResumeLayout(false);
            this.grpSwitchTo.PerformLayout();
            this.grpVisualSearch.ResumeLayout(false);
            this.grpVisualSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.TextBox txtBrowserUrl;
        public System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.GroupBox grpVisualSearch;
        public System.Windows.Forms.Button btnStartVisualSearch;
        public System.Windows.Forms.TextBox txtVisualSearchResult;
        public System.Windows.Forms.Button btnBrowser_Go;
        public PageObjectDefinitionView pageObjectDefinitionView;
        private PageObjectSourceCodeView pageObjectSourceCodeView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public BrowserSettingsTabView browserSettingsTab1;
        public HtmlDomTesterView htmlDomTesterView1;
        public FullHtmlSourceTabView fullHtmlSourceTabView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        public SelectorsEditView selectorsEditView;
        private System.Windows.Forms.Panel pnlLoadingBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLoadingInProgress;
        public System.Windows.Forms.GroupBox grpSwitchTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlWindows;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ddlFrames;
        private System.Windows.Forms.TabPage tabPage5;
        private JavaScriptEditorView javaScriptEditorView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox pictureBox2;
        

    }
}

