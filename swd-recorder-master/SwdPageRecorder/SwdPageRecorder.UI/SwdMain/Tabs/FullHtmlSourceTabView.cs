using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SwdPageRecorder.UI
{
    public partial class FullHtmlSourceTabView : UserControl, IView
    {
        private FullHtmlSourceTabPresenter presenter;
        public FullHtmlSourceTabView()
        {
            InitializeComponent();

            this.presenter = Presenters.PageObjectSourceCodePresenter;
            presenter.InitWithView(this);

            txtHtmlPageSource.Language = Language.HTML;
        }

        private void btnGetHtmlSource_Click(object sender, EventArgs e)
        {
            presenter.DisplayHtmlPageSource();
        }

        internal void FillHtmlCodeBox(string htmlText)
        {
            txtHtmlPageSource.Clear();
            txtHtmlPageSource.Text = htmlText;
        }

        private void txtHtmlPageSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                txtHtmlPageSource.SelectAll();
            }
        }

        private void btnSendDataUri_Click(object sender, EventArgs e)
        {
            presenter.SendContentToBrowser(txtHtmlPageSource.Text);
        }

        private void btnTidyHtml_Click(object sender, EventArgs e)
        {
            string htmlContent = txtHtmlPageSource.Text;
            presenter.TidyHtml(htmlContent);
        }
    }
}
