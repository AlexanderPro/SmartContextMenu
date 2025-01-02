using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SmartContextMenu.Settings;
using SmartContextMenu.Hooks;
using SmartContextMenu.Extensions;


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
            lblKey1.Text = _languageManager.GetString("start_program_lbl_key1");
            lblKey2.Text = _languageManager.GetString("start_program_lbl_key2");
            lblKey3.Text = _languageManager.GetString("start_program_lbl_key3");
            chkShowWindow.Text = _languageManager.GetString("start_program_show_window");
            chkUseWindowWorkingDirectory.Text = _languageManager.GetString("start_program_use_window_working_directory");
            Text = _languageManager.GetString("start_program_form");

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();

            cmbKey3.ValueMember = "Id";
            cmbKey3.DisplayMember = "Text";
            cmbKey3.DataSource = EnumExtensions.AsEnumerable<VirtualKey>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();

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
                cmbKey1.SelectedValue = menuItem.Key1;
                cmbKey2.SelectedValue = menuItem.Key2;
                cmbKey3.SelectedValue = menuItem.Key3;
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
                Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue,
                Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue,
                Key3 = (VirtualKey)cmbKey3.SelectedValue
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