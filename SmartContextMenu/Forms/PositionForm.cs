using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    partial class PositionForm : Form
    {
        public int WindowLeft { get; private set; }

        public int WindowTop { get; private set; }

        public PositionForm(LanguageManager manager, Window window)
        {
            InitializeComponent();
            InitializeControls(manager, window);
        }

        private void InitializeControls(LanguageManager manager, Window window)
        {
            lblLeft.Text = manager.GetString("lbl_left");
            lblTop.Text = manager.GetString("lbl_top");
            btnApply.Text = manager.GetString("align_btn_apply");
            Text = manager.GetString("align_form");

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