using System;
using System.Windows.Forms;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class TransparencyForm : Form
    {
        public int WindowTransparency { get; private set; }

        public TransparencyForm(ApplicationSettings settings, Window window)
        {
            InitializeComponent();
            var languageManager = new LanguageManager(settings.LanguageName);
            InitializeControls(languageManager, window);
        }

        private void InitializeControls(LanguageManager languageManager, Window window)
        {
            btnApply.Text = languageManager.GetString("trans_btn_apply");
            Text = languageManager.GetString("trans_form");
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
