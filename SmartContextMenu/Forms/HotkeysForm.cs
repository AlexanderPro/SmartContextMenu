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
        public Settings.MenuItem MenuItem { get; private set; }

        public HotkeysForm(ApplicationSettings settings, Settings.MenuItem menuItem)
        {
            InitializeComponent();
            MenuItem = menuItem;
            var languageManager = new LanguageManager(settings.LanguageName);
            InitializeControls(languageManager, menuItem);
        }

        private void InitializeControls(LanguageManager languageManager, Settings.MenuItem menuItem)
        {
            Text = languageManager.GetString("hotkeys_form");
            btnApply.Text = languageManager.GetString("hotkeys_btn_apply");
            btnCancel.Text = languageManager.GetString("hotkeys_btn_cancel");
            lblKey1.Text = languageManager.GetString("hotkeys_lbl_key1");
            lblKey2.Text = languageManager.GetString("hotkeys_lbl_key2");
            lblKey3.Text = languageManager.GetString("hotkeys_lbl_key3");

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey1.SelectedValue = menuItem.Key1;

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey2.SelectedValue = menuItem.Key2;

            cmbKey3.ValueMember = "Id";
            cmbKey3.DisplayMember = "Text";
            cmbKey3.DataSource = EnumExtensions.AsEnumerable<VirtualKey>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey3.SelectedValue = menuItem.Key3;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var menuItem = new Settings.MenuItem();
            menuItem.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            menuItem.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            menuItem.Key3 = (VirtualKey)cmbKey3.SelectedValue;
            menuItem.Name = MenuItem.Name;
            MenuItem = menuItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
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
                Close();
            }
        }
    }
}
