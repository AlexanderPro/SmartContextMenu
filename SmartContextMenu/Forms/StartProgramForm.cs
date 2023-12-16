using System;
using System.IO;
using System.Windows.Forms;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    public partial class StartProgramForm : Form
    {
        private readonly LanguageManager _languageManager;
        public StartProgramMenuItem MenuItem { get; private set; }

        public StartProgramForm(LanguageManager manager, StartProgramMenuItem menuItem)
        {
            _languageManager = manager;
            InitializeComponent();
            InitializeControls(menuItem);
        }

        private void InitializeControls(StartProgramMenuItem menuItem)
        {
            lblTitle.Text = _languageManager.GetString("start_program_lbl_title");
            btnApply.Text = _languageManager.GetString("start_program_btn_apply");
            btnCancel.Text = _languageManager.GetString("start_program_btn_cancel");
            lblFileName.Text = _languageManager.GetString("start_program_lbl_file_name");
            lblArguments.Text = _languageManager.GetString("start_program_lbl_arguments");
            lblBegin.Text = _languageManager.GetString("start_program_lbl_begin");
            lblEnd.Text = _languageManager.GetString("start_program_lbl_end");
            chkShowWindow.Text = _languageManager.GetString("start_program_show_window");
            chkUseWindowWorkingDirectory.Text = _languageManager.GetString("start_program_use_window_working_directory");
            Text = _languageManager.GetString("start_program_form");
            if (menuItem != null)
            {
                txtTitle.Text = menuItem.Title;
                txtFileName.Text = menuItem.FileName;
                txtArguments.Text = menuItem.Arguments;
                txtBegin.Text = menuItem.BeginParameter;
                txtEnd.Text = menuItem.EndParameter;
                txtParameter.Text = $"{menuItem.BeginParameter}{_languageManager.GetString("start_program_parameter")}{menuItem.EndParameter}";
                chkShowWindow.Checked = menuItem.ShowWindow;
                chkUseWindowWorkingDirectory.Checked = menuItem.UseWindowWorkingDirectory;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void BeginParameterTextChanged(object sender, EventArgs e)
        {
            txtParameter.Text = $"{txtBegin.Text}{_languageManager.GetString("start_program_parameter")}{txtEnd.Text}";
        }

        private void EndParameterTextChanged(object sender, EventArgs e)
        {
            txtParameter.Text = $"{txtBegin.Text}{_languageManager.GetString("start_program_parameter")}{txtEnd.Text}";
        }

        private void ButtonBrowseFileClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = _languageManager.GetString("start_program_browse_file_filter")
            };

            if (File.Exists(txtFileName.Text))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(txtFileName.Text);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = dialog.FileName;
            }
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            MenuItem = new StartProgramMenuItem
            {
                Title = txtTitle.Text,
                FileName = txtFileName.Text,
                Arguments = txtArguments.Text,
                BeginParameter = txtBegin.Text,
                EndParameter = txtEnd.Text,
                ShowWindow = chkShowWindow.Checked,
                UseWindowWorkingDirectory = chkUseWindowWorkingDirectory.Checked,
            };
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
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
    }
}