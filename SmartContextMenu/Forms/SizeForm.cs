using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    partial class SizeForm : Form
    {
        public int WindowLeft { get; private set; }

        public int WindowTop { get; private set; }

        public int WindowWidth { get; private set; }

        public int WindowHeight { get; private set; }

        public SizeForm(LanguageManager manager, Window window)
        {
            InitializeComponent();
            InitializeControls(manager, window);
        }

        private void InitializeControls(LanguageManager manager, Window window)
        {
            lblLeft.Text = manager.GetString("lbl_size_form_left");
            lblTop.Text = manager.GetString("lbl_size_form_top");
            lblWidth.Text = manager.GetString("lbl_size_form_width");
            lblHeight.Text = manager.GetString("lbl_size_form_height");
            btnApply.Text = manager.GetString("size_btn_apply");
            Text = manager.GetString("size_form");

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
