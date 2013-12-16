﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBehave.VS2010.Plugin.Editor.Glyphs.Views
{
    /// <summary>
    /// Interaction logic for RunOrDebugView.xaml
    /// </summary>
    public partial class RunOrDebugView : Popup, IRunOrDebugView
    {
        public RunOrDebugView()
        {
            InitializeComponent();
        }

        public bool IsMouseOverPopup { get { return this.IsMouseOver; }}
        public void Deselect()
        {
//            this.commandListBox.SelectedItem = null;
//            this.commandListBox.SelectedIndex = -1;
            this.commandListBox.SelectedValue = null;
        }
    }
}
