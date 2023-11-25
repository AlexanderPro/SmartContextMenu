using System;
using System.Data;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using SmartContextMenu.Settings;
using SmartContextMenu.Hooks;
using SmartContextMenu.Extensions;

namespace SmartContextMenu.Forms
{
    partial class SizeSettingsForm : Form
    {
        public WindowSizeMenuItem MenuItem { get; private set; }

        public SizeSettingsForm(ApplicationSettings settings, WindowSizeMenuItem menuItem)
        {
            MenuItem = menuItem;
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeComponent();
            InitializeControls(resourceManager, menuItem);
        }

        private void InitializeControls(ResourceManager resourceManager, WindowSizeMenuItem menuItem)
        {
            lblTitle.Text = resourceManager.GetString("lbl_window_size_title");
            lblLeft.Text = resourceManager.GetString("lbl_window_size_left");
            lblTop.Text = resourceManager.GetString("lbl_window_size_top");
            lblWidth.Text = resourceManager.GetString("lbl_window_size_width");
            lblHeight.Text = resourceManager.GetString("lbl_window_size_height");
            lblKey1.Text = resourceManager.GetString("lbl_window_size_key1");
            lblKey2.Text = resourceManager.GetString("lbl_window_size_key2");
            lblKey3.Text = resourceManager.GetString("lbl_window_size_key3");
            btnApply.Text = resourceManager.GetString("window_size_btn_apply");
            btnCancel.Text = resourceManager.GetString("window_size_btn_cancel");
            Text = resourceManager.GetString("window_size_form");

            txtTitle.Text = menuItem.Title;
            txtLeft.Text = menuItem.Left == null ? string.Empty : menuItem.Left.Value.ToString();
            txtTop.Text = menuItem.Top == null ? string.Empty : menuItem.Top.Value.ToString();
            txtWidth.Text = menuItem.Width.ToString();
            txtHeight.Text = menuItem.Height.ToString();

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.SelectAll();
            txtTitle.Focus();
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.SelectAll();
                txtTitle.Focus();
                return;
            }

            var menuItem = new WindowSizeMenuItem();
            menuItem.Title = txtTitle.Text;
            menuItem.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            menuItem.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            menuItem.Key3 = (VirtualKey)cmbKey3.SelectedValue;

            if (int.TryParse(txtWidth.Text, out var width))
            {
                menuItem.Width = width;
            }
            else
            {
                txtWidth.SelectAll();
                txtWidth.Focus();
                return;
            }

            if (int.TryParse(txtHeight.Text, out var height))
            {
                menuItem.Height = height;
            }
            else
            {
                txtHeight.SelectAll();
                txtHeight.Focus();
                return;
            }

            if (int.TryParse(txtLeft.Text, out var left))
            {
                menuItem.Left = left;
            }

            if (int.TryParse(txtTop.Text, out var top))
            {
                menuItem.Top = top;
            }

            MenuItem = menuItem;
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
