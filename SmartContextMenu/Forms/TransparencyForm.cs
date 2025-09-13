using System;
using System.Windows.Forms;
using SmartContextMenu.Native;

namespace SmartContextMenu.Forms
{
    partial class TransparencyForm : Form
    {
        private Window _window;

        public int WindowTransparency { get; private set; }

        public TransparencyForm(LanguageManager manager, Window window)
        {
            InitializeComponent();
            InitializeControls(manager, window);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_SYSCOMMAND)
            {
                var lowOrder = m.WParam.ToInt64() & 0x0000FFFF;
                if (lowOrder == Constants.SC_CLOSE)
                {
                    ButtonCancelClick(this, EventArgs.Empty);
                }
            }

            base.WndProc(ref m);
        }


        private void InitializeControls(LanguageManager manager, Window window)
        {
            _window = window;
            btnApply.Text = manager.GetString("trans_btn_apply");
            Text = manager.GetString("trans_form");
            WindowTransparency = window.Transparency;
            numericTransparency.Value = WindowTransparency;
            DialogResult = DialogResult.Cancel;
            numericTransparency.TextChanged += NumericTransparencyValueChanged;
        }

        private void NumericTransparencyValueChanged(object sender, EventArgs e) => ChangeTransparency();

        private void NumericTransparencyKeyDown(object sender, KeyEventArgs e) => ChangeTransparency();

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            WindowTransparency = (int)numericTransparency.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            if (WindowTransparency != _window.Transparency)
            {
                _window.SetTransparency(WindowTransparency);
            }
            DialogResult = DialogResult.Cancel;
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
                ButtonCancelClick(sender, e);
            }
        }

        private void ChangeTransparency()
        {
            _window.SetTransparency((int)numericTransparency.Value);
        }
    }
}
