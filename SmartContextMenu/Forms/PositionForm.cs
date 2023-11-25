using System;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class PositionForm : Form
    {
        public int WindowLeft { get; private set; }

        public int WindowTop { get; private set; }

        public PositionForm(ApplicationSettings settings, Window window)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeControls(resourceManager, window);
        }

        private void InitializeControls(ResourceManager resourceManager, Window window)
        {
            lblLeft.Text = resourceManager.GetString("lbl_left");
            lblTop.Text = resourceManager.GetString("lbl_top");
            btnApply.Text = resourceManager.GetString("align_btn_apply");
            Text = resourceManager.GetString("align_form");

            var left = window.Size.Left;
            var top = window.Size.Top;

            WindowLeft = left;
            WindowTop = top;

            txtLeft.Text = left.ToString();
            txtTop.Text = top.ToString();

            DialogResult = DialogResult.Cancel;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (!int.TryParse(txtLeft.Text, out var left))
            {
                txtLeft.SelectAll();
                txtLeft.Focus();
                return;
            }

            if (!int.TryParse(txtTop.Text, out var top))
            {
                txtTop.SelectAll();
                txtTop.Focus();
                return;
            }

            WindowLeft = left;
            WindowTop = top;

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