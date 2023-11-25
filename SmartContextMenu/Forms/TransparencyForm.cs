using System;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class TransparencyForm : Form
    {
        public int WindowTransparency { get; private set; }

        public TransparencyForm(ApplicationSettings settings, Window window)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeControls(resourceManager, window);
        }

        private void InitializeControls(ResourceManager resourceManager, Window window)
        {
            btnApply.Text = resourceManager.GetString("trans_btn_apply");
            Text = resourceManager.GetString("trans_form");
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
