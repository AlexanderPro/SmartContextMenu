using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    partial class TransparencyForm : Form
    {
        public int WindowTransparency { get; private set; }

        public TransparencyForm(LanguageManager manager, Window window)
        {
            InitializeComponent();
            InitializeControls(manager, window);
        }

        private void InitializeControls(LanguageManager manager, Window window)
        {
            btnApply.Text = manager.GetString("trans_btn_apply");
            Text = manager.GetString("trans_form");
            numericTransparency.Value = window.Transparency;
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            WindowTransparency = (int)numericTransparency.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ButtonApplyClick(sender, e);
            }

            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
