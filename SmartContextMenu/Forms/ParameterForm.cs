using System;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class ParameterForm : Form
    {
        public string ParameterValue { get; private set; }

        public ParameterForm(ApplicationSettings settings, string parameter)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeControls(resourceManager, parameter);
        }

        private void InitializeControls(ResourceManager resourceManager, string parameter)
        {
            lblParameter.Text = parameter;
            btnApply.Text = resourceManager.GetString("parameter_btn_apply");
            Text = resourceManager.GetString("parameter_form");
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