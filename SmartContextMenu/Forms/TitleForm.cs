using System;
using System.Windows.Forms;

namespace SmartContextMenu.Forms
{
    partial class TitleForm : Form
    {
        public string Title
        {
            get
            {
                return txtTitle.Text;
            }
            set
            {
                txtTitle.Text = value;
            }
        }

        public TitleForm(LanguageManager manager)
        {
            InitializeComponent();
            InitializeControls(manager);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtTitle.Focus();
        }

        private void InitializeControls(LanguageManager manager)
        {
            btnApply.Text = manager.GetString("change_title_btn_apply");
            btnCancel.Text = manager.GetString("change_title_btn_cancel");
            Text = manager.GetString("change_title_form");            
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                ButtonCancelClick(sender, e);
            }
        }
    }
}