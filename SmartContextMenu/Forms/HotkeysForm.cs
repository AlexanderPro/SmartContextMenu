using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartContextMenu.Hooks;
using SmartContextMenu.Extensions;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    public partial class HotkeysForm : Form
    {
        public KeyboardShortcut Shortcut { get; private set; }

        public HotkeysForm(LanguageManager manager, KeyboardShortcut shortcut)
        {
            InitializeComponent();
            Shortcut = shortcut;
            InitializeControls(manager, shortcut);
        }

        private void InitializeControls(LanguageManager manager, KeyboardShortcut shortcut)
        {
            Text = manager.GetString("hotkeys_form");
            btnApply.Text = manager.GetString("hotkeys_btn_apply");
            btnCancel.Text = manager.GetString("hotkeys_btn_cancel");
            lblKey1.Text = manager.GetString("hotkeys_lbl_key1");
            lblKey2.Text = manager.GetString("hotkeys_lbl_key2");
            lblKey3.Text = manager.GetString("hotkeys_lbl_key3");

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey1.SelectedValue = shortcut.Key1;

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey2.SelectedValue = shortcut.Key2;

            cmbKey3.ValueMember = "Id";
            cmbKey3.DisplayMember = "Text";
            cmbKey3.DataSource = EnumExtensions.AsEnumerable<VirtualKey>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey3.SelectedValue = shortcut.Key3;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            Shortcut = new KeyboardShortcut();
            Shortcut.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            Shortcut.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            Shortcut.Key3 = (VirtualKey)cmbKey3.SelectedValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e) => Close();

        private void KeyDownClick(object sender, KeyEventArgs e)
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
