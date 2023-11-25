using System;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class SizeForm : Form
    {
        public int WindowLeft { get; private set; }

        public int WindowTop { get; private set; }

        public int WindowWidth { get; private set; }

        public int WindowHeight { get; private set; }

        public SizeForm(ApplicationSettings settings, Window window)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeControls(resourceManager, window);
        }

        private void InitializeControls(ResourceManager resourceManager, Window window)
        {
            lblLeft.Text = resourceManager.GetString("lbl_size_form_left");
            lblTop.Text = resourceManager.GetString("lbl_size_form_top");
            lblWidth.Text = resourceManager.GetString("lbl_size_form_width");
            lblHeight.Text = resourceManager.GetString("lbl_size_form_height");
            btnApply.Text = resourceManager.GetString("size_btn_apply");
            Text = resourceManager.GetString("size_form");

            var left = window.Size.Left;
            var top = window.Size.Top;
            var width = window.Size.Width;
            var height = window.Size.Height;

            WindowLeft = left;
            WindowTop = top;
            WindowWidth = width;
            WindowHeight = height;

            txtLeft.Text = left.ToString();
            txtTop.Text = top.ToString();
            txtWidth.Text = width.ToString();
            txtHeight.Text = height.ToString();

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

            if (!int.TryParse(txtWidth.Text, out var width))
            {
                txtWidth.SelectAll();
                txtWidth.Focus();
                return;
            }

            if (!int.TryParse(txtHeight.Text, out var height))
            {
                txtHeight.SelectAll();
                txtHeight.Focus();
                return;
            }

            WindowLeft = left;
            WindowTop = top;
            WindowWidth = width;
            WindowHeight = height;

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
