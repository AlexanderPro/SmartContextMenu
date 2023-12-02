using System;
using System.Windows.Forms;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class ParameterForm : Form
    {
        public string ParameterValue { get; private set; }

        public ParameterForm(ApplicationSettings settings, string parameter)
        {
            InitializeComponent();
            var languageManager = new LanguageManager(settings.LanguageName);
            InitializeControls(languageManager, parameter);
        }

        private void InitializeControls(LanguageManager languageManager, string parameter)
        {
            lblParameter.Text = parameter;
            btnApply.Text = languageManager.GetString("parameter_btn_apply");
            Text = languageManager.GetString("parameter_form");
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            ParameterValue = txtParameterValue.Text;
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