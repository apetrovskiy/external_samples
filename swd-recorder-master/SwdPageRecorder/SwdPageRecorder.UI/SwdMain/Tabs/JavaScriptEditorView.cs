using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SwdPageRecorder.UI
{
    public partial class JavaScriptEditorView : UserControl, IView
    {
        private JavascriptEditorTabPresenter presenter;
        public JavaScriptEditorView()
        {
            InitializeComponent();
            this.presenter = Presenters.JavascriptEditorTabPresenter;
            presenter.InitWithView(this);
        }
    }
}
