using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using SmartContextMenu.Extensions;
using SmartContextMenu.Settings;
using SmartContextMenu.Controls;
using SmartContextMenu.Hooks;

namespace SmartContextMenu.Forms
{
    public partial class ApplicationSettingsForm : Form
    {
        private ApplicationSettings _settings;
        private LanguageManager _languageManager;

        public event EventHandler<EventArgs<ApplicationSettings>> OkClick;

        public ApplicationSettingsForm(ApplicationSettings settings)
        {
            _settings = settings;
            _languageManager = new LanguageManager(_settings.LanguageName);
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            tabpGeneral.Text = _languageManager.GetString("tab_settings_general");
            tabpMenuStart.Text = _languageManager.GetString("tab_settings_menu_start");
            tabpMenuSize.Text = _languageManager.GetString("tab_settings_menu_size");
            tabpMenuDimmer.Text = _languageManager.GetString("tab_settings_menu_dimmer");
            tabpMenu.Text = _languageManager.GetString("tab_settings_menu");
            grpbMouseHotkeys.Text = _languageManager.GetString("grpb_hotkeys");
            grpbLanguage.Text = _languageManager.GetString("grpb_language");
            grpbStartProgram.Text = _languageManager.GetString("grpb_start_program");
            grpbWindowSize.Text = _languageManager.GetString("grpb_window_size");
            grpbSizer.Text = _languageManager.GetString("grpb_sizer");
            grpbDisplay.Text = _languageManager.GetString("grpb_display");
            grpbDimmerColor.Text = _languageManager.GetString("grpb_dimmer_color");
            grpbDimmerTransparency.Text = _languageManager.GetString("grpb_dimmer_transparency");
            chkEnableHighDPI.Text = _languageManager.GetString("chk_enable_high_dpi");
            clmStartProgramTitle.HeaderText = _languageManager.GetString("clm_start_program_title");
            clmStartProgramPath.HeaderText = _languageManager.GetString("clm_start_program_path");
            clmStartProgramArguments.HeaderText = _languageManager.GetString("clm_start_program_arguments");
            clmStartProgramEdit.ToolTipText = _languageManager.GetString("clm_start_program_edit");
            clmStartProgramDelete.ToolTipText = _languageManager.GetString("clm_start_program_delete");
            clmWindowSizeTitle.HeaderText = _languageManager.GetString("clm_window_size_title");
            clmWindowSizeLeft.HeaderText = _languageManager.GetString("clm_window_size_left");
            clmWindowSizeTop.HeaderText = _languageManager.GetString("clm_window_size_top");
            clmWindowSizeWidth.HeaderText = _languageManager.GetString("clm_window_size_width");
            clmWindowSizeHeight.HeaderText = _languageManager.GetString("clm_window_size_height");
            clmWindowSizeEdit.ToolTipText = _languageManager.GetString("clm_window_size_edit");
            clmWindowSizeDelete.ToolTipText = _languageManager.GetString("clm_window_size_delete");
            clmnMenuItemName.HeaderText = _languageManager.GetString("clm_hotkeys_name");
            clmnHotkeys.HeaderText = _languageManager.GetString("clm_hotkeys_keys");
            lblKey1.Text = _languageManager.GetString("lbl_key1");
            lblKey2.Text = _languageManager.GetString("lbl_key2");
            lblKey3.Text = _languageManager.GetString("lbl_key3");
            lblKey4.Text = _languageManager.GetString("lbl_key4");
            lblMouseButton.Text = _languageManager.GetString("lbl_mouse_button");
            toolTipAddProcessName.SetToolTip(btnAddStartProgram, _languageManager.GetString("btn_add_start_program"));
            toolTipAddProcessName.SetToolTip(btnStartProgramDown, _languageManager.GetString("btn_start_program_down"));
            toolTipAddProcessName.SetToolTip(btnStartProgramUp, _languageManager.GetString("btn_start_program_up"));
            toolTipAddProcessName.SetToolTip(btnAddWindowSize, _languageManager.GetString("btn_add_window_size"));
            toolTipAddProcessName.SetToolTip(btnWindowSizeDown, _languageManager.GetString("btn_window_size_down"));
            toolTipAddProcessName.SetToolTip(btnWindowSizeUp, _languageManager.GetString("btn_window_size_up"));
            toolTipAddProcessName.SetToolTip(btnMenuItemDown, _languageManager.GetString("btn_menu_item_down"));
            toolTipAddProcessName.SetToolTip(btnMenuItemUp, _languageManager.GetString("btn_menu_item_up"));
            btnApply.Text = _languageManager.GetString("settings_btn_apply");
            btnCancel.Text = _languageManager.GetString("settings_btn_cancel");
            Text = _languageManager.GetString("settings_form");

            txtDimmerColor.Text = _settings.Dimmer.Color;
            trackbDimmerTransparency.Value = _settings.Dimmer.Transparency;
            lblTransparencyValue.Text = $"{_settings.Dimmer.Transparency}%";

            cmbKey1.ValueMember = "Id";
            cmbKey1.DisplayMember = "Text";
            cmbKey1.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey1.SelectedValue = _settings.Key1;

            cmbKey2.ValueMember = "Id";
            cmbKey2.DisplayMember = "Text";
            cmbKey2.DataSource = EnumExtensions.AsEnumerable<VirtualKeyModifier>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey2.SelectedValue = _settings.Key2;

            cmbKey3.ValueMember = "Id";
            cmbKey3.DisplayMember = "Text";
            cmbKey3.DataSource = EnumExtensions.AsEnumerable<VirtualKey>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey3.SelectedValue = _settings.Key3;

            cmbKey4.ValueMember = "Id";
            cmbKey4.DisplayMember = "Text";
            cmbKey4.DataSource = EnumExtensions.AsEnumerable<VirtualKey>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbKey4.SelectedValue = _settings.Key4;

            cmbMouseButton.ValueMember = "Id";
            cmbMouseButton.DisplayMember = "Text";
            cmbMouseButton.DataSource = EnumExtensions.AsEnumerable<MouseButton>().Select(x => new { Id = x, Text = x.GetDescription() }).Where(x => !string.IsNullOrEmpty(x.Text)).ToList();
            cmbMouseButton.SelectedValue = _settings.MouseButton;

            listBoxLanguage.DisplayMember = "Text";
            listBoxLanguage.ValueMember = "Value";

            var languageItems = new[] {
                new { Text = "English", Value = "en" },
                new { Text = "Deutsch", Value = "de" },
                new { Text = "Français", Value = "fr" },
                new { Text = "Italiano", Value = "it" },
                new { Text = "Magyar", Value = "hu" },
                new { Text = "Español", Value = "es" },
                new { Text = "Português", Value = "pt" },
                new { Text = "Русский", Value = "ru" },
                new { Text = "Српски", Value = "sr" },
                new { Text = "Slovenščina", Value = "sl" },
                new { Text = "עִברִית", Value = "he" },
                new { Text = "简体中文", Value = "zh_cn" },
                new { Text = "繁體中文", Value = "zh_tw"},
                new { Text = "日本語", Value = "ja" },
                new { Text = "한국어", Value = "ko" }
            };

            listBoxLanguage.DataSource = languageItems;
            listBoxLanguage.SelectedValue = _settings.LanguageName;

            cmbSizer.Items.Add(_languageManager.GetString("sizer_window_with_margins"));
            cmbSizer.Items.Add(_languageManager.GetString("sizer_window_without_margins"));
            cmbSizer.Items.Add(_languageManager.GetString("sizer_window_client_area"));
            cmbSizer.SelectedIndex = (int)_settings.Sizer;
            chkEnableHighDPI.Checked = _settings.EnableHighDPI;

            var items = new List<Settings.MenuItem>();
            foreach(var item in _settings.MenuItems.Items)
            {
                items.Add((Settings.MenuItem)item.Clone());
            }

            FillGridViewByItems(gvHotkeys, items);
            FillGridViewByWindowSizeItems(gvWindowSize, _settings.MenuItems.WindowSizeItems);
            FillGridViewByStartProgramItems(gvStartProgram, _settings.MenuItems.StartProgramItems);
        }

        private void GridViewStartProgramCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (e.ColumnIndex == 3 && !row.ReadOnly && row.Tag is StartProgramMenuItem menuItem)
                {
                    var dialog = new StartProgramForm(_languageManager, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.FileName;
                        row.Cells[2].Value = dialog.MenuItem.Arguments;
                        row.Tag = dialog.MenuItem;
                    }
                }

                if (e.ColumnIndex == 4)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void GridViewWindowSizeCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (e.ColumnIndex == 6 && !row.ReadOnly && row.Tag is WindowSizeMenuItem menuItem)
                {
                    var dialog = new SizeSettingsForm(_languageManager, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                        row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                        row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                        row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                        row.Cells[5].Value = dialog.MenuItem.ToString();

                        menuItem.Title = dialog.MenuItem.Title;
                        menuItem.Left = dialog.MenuItem.Left;
                        menuItem.Top = dialog.MenuItem.Top;
                        menuItem.Width = dialog.MenuItem.Width;
                        menuItem.Height = dialog.MenuItem.Height;
                        menuItem.Key1 = dialog.MenuItem.Key1;
                        menuItem.Key2 = dialog.MenuItem.Key2;
                        menuItem.Key3 = dialog.MenuItem.Key3;
                    }
                }

                if (e.ColumnIndex == 7)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void GridViewHotkeysCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    ShowHotkeysForm(row);
                }
            }

            if (grid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                var cell = (DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex];
                cell.Value = !(bool)cell.Value;
                var menuItem = (Settings.MenuItem)row.Tag;
                menuItem.Show = (bool)cell.Value;
            }
        }

        private void GridViewHotkeysCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1) && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    ShowHotkeysForm(row);
                }
            }
        }

        private void GridViewStartProgramCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && grid.Rows[e.RowIndex].Tag is StartProgramMenuItem menuItem)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    var dialog = new StartProgramForm(_languageManager, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.FileName;
                        row.Cells[2].Value = dialog.MenuItem.Arguments;
                        row.Tag = dialog.MenuItem;
                    }
                }
            }
        }

        private void GridViewWindowSizeCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5) && e.RowIndex >= 0 && grid.Rows[e.RowIndex].Tag is WindowSizeMenuItem menuItem)
            {
                var row = grid.Rows[e.RowIndex];
                if (!row.ReadOnly)
                {
                    var dialog = new SizeSettingsForm(_languageManager, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        row.Cells[0].Value = dialog.MenuItem.Title;
                        row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                        row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                        row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                        row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                        row.Cells[5].Value = dialog.MenuItem.ToString();

                        menuItem.Title = dialog.MenuItem.Title;
                        menuItem.Left = dialog.MenuItem.Left;
                        menuItem.Top = dialog.MenuItem.Top;
                        menuItem.Width = dialog.MenuItem.Width;
                        menuItem.Height = dialog.MenuItem.Height;
                        menuItem.Key1 = dialog.MenuItem.Key1;
                        menuItem.Key2 = dialog.MenuItem.Key2;
                        menuItem.Key3 = dialog.MenuItem.Key3;
                    }
                }
            }
        }

        private void ButtonAddStartProgramClick(object sender, EventArgs e)
        {
            var dialog = new StartProgramForm(_languageManager, null);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = dialog.MenuItem.Title;
                row.Cells[1].Value = dialog.MenuItem.FileName;
                row.Cells[2].Value = dialog.MenuItem.Arguments;
                row.Cells[3].ToolTipText = _languageManager.GetString("clm_start_program_edit");
                row.Cells[4].ToolTipText = _languageManager.GetString("clm_start_program_delete");
                row.Tag = dialog.MenuItem;
            }
        }

        private void ButtonAddWindowSizeClick(object sender, EventArgs e)
        {
            var dialog = new SizeSettingsForm(_languageManager, new WindowSizeMenuItem { Width = 1, Height = 1 });
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var index = gvWindowSize.Rows.Add();
                var row = gvWindowSize.Rows[index];
                row.Cells[0].Value = dialog.MenuItem.Title;
                row.Cells[1].Value = dialog.MenuItem.Left.HasValue ? dialog.MenuItem.Left.ToString() : string.Empty;
                row.Cells[2].Value = dialog.MenuItem.Top.HasValue ? dialog.MenuItem.Top.ToString() : string.Empty;
                row.Cells[3].Value = dialog.MenuItem.Width.ToString();
                row.Cells[4].Value = dialog.MenuItem.Height.ToString();
                row.Cells[5].Value = dialog.MenuItem.ToString();
                row.Cells[6].ToolTipText = _languageManager.GetString("clm_window_size_edit");
                row.Cells[7].ToolTipText = _languageManager.GetString("clm_window_size_delete");
                row.Tag = dialog.MenuItem;
            }
        }

        private void ButtonArrowUpClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnWindowSizeUp" ? gvWindowSize : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index > 0 ? index - 1 : 0;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
                grid.CurrentCell = grid.Rows[newIndex].Cells[0];
            }
        }

        private void ButtonArrowDownClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var grid = button.Name == "btnWindowSizeDown" ? gvWindowSize : gvStartProgram;
            if (grid.SelectedRows.Count > 0)
            {
                var index = grid.SelectedRows[0].Index;
                var newIndex = index < grid.Rows.Count - 1 ? index + 1 : grid.Rows.Count - 1;
                var selectedRow = grid.SelectedRows[0];
                grid.Rows.RemoveAt(index);
                grid.Rows.Insert(newIndex, selectedRow);
                grid.Rows[newIndex].Selected = true;
                grid.CurrentCell = grid.Rows[newIndex].Cells[0];
            }
        }

        private void ButtonMenuItemUpClick(object sender, EventArgs e)
        {
            if (gvHotkeys.SelectedRows.Count > 0)
            {
                var items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
                var item = (Settings.MenuItem)gvHotkeys.SelectedRows[0].Tag;
                var list = FindList(items, item);
                if (list != null && list.Count > 1)
                {
                    var index = list.IndexOf(item);
                    if (index > 0)
                    {
                        ((List<Settings.MenuItem>)list).Reverse(index - 1, 2);
                        gvHotkeys.Rows.Clear();
                        FillGridViewByItems(gvHotkeys, items);
                        foreach (DataGridViewRow row in gvHotkeys.Rows)
                        {
                            if (row.Tag == item)
                            {
                                row.Selected = true;
                                gvHotkeys.CurrentCell = row.Cells[0];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void ButtonMenuItemDownClick(object sender, EventArgs e)
        {
            if (gvHotkeys.SelectedRows.Count > 0)
            {
                var items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
                var item = (Settings.MenuItem)gvHotkeys.SelectedRows[0].Tag;
                var list = FindList(items, item);
                if (list != null && list.Count > 1)
                {
                    var index = list.IndexOf(item);
                    if (index < list.Count - 1)
                    {
                        ((List<Settings.MenuItem>)list).Reverse(index, 2);
                        gvHotkeys.Rows.Clear();
                        FillGridViewByItems(gvHotkeys, items);
                        foreach (DataGridViewRow row in gvHotkeys.Rows)
                        {
                            if (row.Tag == item)
                            {
                                row.Selected = true;
                                gvHotkeys.CurrentCell = row.Cells[0];
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void TrackbDimmerTransparencyValueChanged(object sender, EventArgs e)
        {
            lblTransparencyValue.Text = $"{trackbDimmerTransparency.Value}%";
        }

        private void ButtonChooseDimmerColorClick(object sender, EventArgs e)
        {
            var color = Color.Black;
            try
            {
                color = ColorTranslator.FromHtml(txtDimmerColor.Text);
            }
            catch
            {
            }

            var dialog = new ColorDialog
            {
                AllowFullOpen = true,
                AnyColor = true,
                FullOpen = true,
                Color = color
            };

            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                txtDimmerColor.Text = ColorTranslator.ToHtml(dialog.Color);
            }
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var settings = new ApplicationSettings();

            foreach (DataGridViewRow row in gvWindowSize.Rows)
            {
                if (row.Tag is WindowSizeMenuItem item)
                {
                    settings.MenuItems.WindowSizeItems.Add((WindowSizeMenuItem)item.Clone());
                }
            }

            foreach (DataGridViewRow row in gvStartProgram.Rows)
            {
                if (row.Tag is StartProgramMenuItem item)
                {
                    settings.MenuItems.StartProgramItems.Add((StartProgramMenuItem)item.Clone());
                }
            }

            settings.Key1 = (VirtualKeyModifier)cmbKey1.SelectedValue;
            settings.Key2 = (VirtualKeyModifier)cmbKey2.SelectedValue;
            settings.Key3 = (VirtualKey)cmbKey3.SelectedValue;
            settings.Key4 = (VirtualKey)cmbKey4.SelectedValue;
            settings.MouseButton = (MouseButton)cmbMouseButton.SelectedValue;
            settings.MenuItems.Items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
            settings.Dimmer.Color = txtDimmerColor.Text;
            settings.Dimmer.Transparency = trackbDimmerTransparency.Value;
            settings.Sizer = (WindowSizerType)cmbSizer.SelectedIndex;
            settings.EnableHighDPI = chkEnableHighDPI.Checked;
            settings.LanguageName = listBoxLanguage.SelectedValue == null ? string.Empty : listBoxLanguage.SelectedValue.ToString();

            if (!settings.Equals(_settings))
            {
                OkClick?.Invoke(this, new EventArgs<ApplicationSettings>(settings));
            }

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

        private void ShowHotkeysForm(DataGridViewRow row)
        {
            var menuItem = (Settings.MenuItem)row.Tag;
            var form = new HotkeysForm(_languageManager, menuItem);
            var result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                menuItem.Key1 = form.MenuItem.Key1;
                menuItem.Key2 = form.MenuItem.Key2;
                menuItem.Key3 = form.MenuItem.Key3;
                row.Cells[1].Value = menuItem.ToString();
                row.Tag = menuItem;
            }
        }

        private void FillGridViewByItems(DataGridView gridView, IList<Settings.MenuItem> items)
        {
            gridView.Tag = items;
            foreach (var item in items)
            {
                if (item.Type == MenuItemType.Item)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    var title = GetTransparencyTitle(item.Name);
                    title = title != null ? title : _languageManager.GetString(item.Name);
                    row.Tag = item;
                    row.Cells[0].Value = title;
                    row.Cells[1].Value = item == null ? string.Empty : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                }

                if (item.Type == MenuItemType.Separator)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    var title = _languageManager.GetString("separator");
                    row.Tag = item;
                    row.ReadOnly = true;
                    row.Cells[0].Value = title;
                    row.Cells[1].Value = item == null ? string.Empty : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                    ((DataGridViewDisableButtonCell)row.Cells[3]).Enabled = false;
                }

                if (item.Type == MenuItemType.Group)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = item;
                    row.ReadOnly = true;
                    row.Cells[0].Value = _languageManager.GetString(item.Name);
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                    ((DataGridViewDisableButtonCell)row.Cells[3]).Enabled = false;

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item)
                        {
                            var subItemIndex = gridView.Rows.Add();
                            var subItemRow = gridView.Rows[subItemIndex];
                            var title = GetTransparencyTitle(subItem.Name);
                            title = title != null ? title : _languageManager.GetString(subItem.Name);
                            subItemRow.Tag = subItem;
                            subItemRow.Cells[0].Value = title;
                            subItemRow.Cells[1].Value = subItem == null ? string.Empty : subItem.ToString();
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).Value = subItem.Show;
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                            var padding = subItemRow.Cells[0].Style.Padding;
                            subItemRow.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
                        }

                        if (subItem.Type == MenuItemType.Separator)
                        {
                            var subItemIndex = gridView.Rows.Add();
                            var subItemRow = gridView.Rows[subItemIndex];
                            var title = _languageManager.GetString("separator");
                            subItemRow.Tag = subItem;
                            subItemRow.ReadOnly = true;
                            subItemRow.Cells[0].Value = title;
                            subItemRow.Cells[1].Value = subItem == null ? "" : subItem.ToString();
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).Value = subItem.Show;
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                            ((DataGridViewDisableButtonCell)subItemRow.Cells[3]).Enabled = false;
                            var padding = subItemRow.Cells[0].Style.Padding;
                            subItemRow.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
                        }
                    }
                }
            }
        }

        private void FillGridViewByWindowSizeItems(DataGridView gridView, IList<WindowSizeMenuItem> items)
        {
            foreach (var item in items)
            {
                if (item.Type == MenuItemType.Item)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = (WindowSizeMenuItem)item.Clone();
                    row.Cells[0].Value = item.Title;
                    row.Cells[1].Value = item.Left.HasValue ? item.Left.ToString() : string.Empty;
                    row.Cells[2].Value = item.Top.HasValue ? item.Top.ToString() : string.Empty;
                    row.Cells[3].Value = item.Width.ToString();
                    row.Cells[4].Value = item.Height.ToString();
                    row.Cells[5].Value = item.ToString();
                    row.Cells[6].ToolTipText = _languageManager.GetString("clm_window_size_edit");
                    row.Cells[7].ToolTipText = _languageManager.GetString("clm_window_size_delete");
                }

                if (item.Type == MenuItemType.Separator)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = (WindowSizeMenuItem)item.Clone();
                    row.ReadOnly = true;
                    row.Cells[0].Value = _languageManager.GetString("separator");
                    row.Cells[6].ToolTipText = _languageManager.GetString("clm_window_size_edit");
                    row.Cells[7].ToolTipText = _languageManager.GetString("clm_window_size_delete");
                    ((DataGridViewDisableButtonCell)row.Cells[6]).Enabled = false;
                }
            }
        }

        private void FillGridViewByStartProgramItems(DataGridView gridView, IList<StartProgramMenuItem> items)
        {
            foreach (var item in items)
            {
                if (item.Type == MenuItemType.Item)
                {
                    var cloneItem = (StartProgramMenuItem)item.Clone();
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = cloneItem;
                    row.Cells[0].Value = cloneItem.Title;
                    row.Cells[1].Value = cloneItem.FileName;
                    row.Cells[2].Value = cloneItem.Arguments;
                    row.Cells[3].ToolTipText = _languageManager.GetString("clm_start_program_edit");
                    row.Cells[4].ToolTipText = _languageManager.GetString("clm_start_program_delete");
                }

                if (item.Type == MenuItemType.Separator)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = (StartProgramMenuItem)item.Clone();
                    row.ReadOnly = true;
                    row.Cells[0].Value = _languageManager.GetString("separator");
                    row.Cells[3].ToolTipText = _languageManager.GetString("clm_start_program_edit");
                    row.Cells[4].ToolTipText = _languageManager.GetString("clm_start_program_delete");
                    ((DataGridViewDisableButtonCell)row.Cells[3]).Enabled = false;
                }
            }
        }

        private string GetTransparencyTitle(string name) => name switch
        {
            MenuItemName.TransparencyOpaque => $"0%{_languageManager.GetString(name)}",
            MenuItemName.Transparency10 => "10%",
            MenuItemName.Transparency20 => "20%",
            MenuItemName.Transparency30 => "30%",
            MenuItemName.Transparency40 => "40%",
            MenuItemName.Transparency50 => "50%",
            MenuItemName.Transparency60 => "60%",
            MenuItemName.Transparency70 => "70%",
            MenuItemName.Transparency80 => "80%",
            MenuItemName.Transparency90 => "90%",
            MenuItemName.TransparencyInvisible => $"100%{_languageManager.GetString(name)}",
            _ => null
        };

        private IList<Settings.MenuItem> FindList(IList<Settings.MenuItem> list, Settings.MenuItem element)
        {
            foreach (var item in list)
            {
                if (item == element)
                {
                    return list;
                }

                if (item.Items.Any(x => x == element))
                {
                    return item.Items;
                }
            }
            return null;
        }
    }
}
