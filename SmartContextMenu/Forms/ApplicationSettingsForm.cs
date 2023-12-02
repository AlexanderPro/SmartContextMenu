using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SmartContextMenu.Settings;
using SmartContextMenu.Controls;

namespace SmartContextMenu.Forms
{
    public partial class SettingsForm : Form
    {
        private ApplicationSettings _settings;
        private LanguageManager _languageManager;

        public event EventHandler<EventArgs<ApplicationSettings>> OkClick;

        public SettingsForm(ApplicationSettings settings)
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
            tabpMenu.Text = _languageManager.GetString("tab_settings_menu");
            grpbLanguage.Text = _languageManager.GetString("grpb_language");
            grpbMouseHotkeys.Text = _languageManager.GetString("grpb_process_exclusions");
            grpbStartProgram.Text = _languageManager.GetString("grpb_start_program");
            grpbWindowSize.Text = _languageManager.GetString("grpb_window_size");
            grpbSizer.Text = _languageManager.GetString("grpb_sizer");
            grpbDisplay.Text = _languageManager.GetString("grpb_display");
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

            foreach (var item in _settings.MenuItems.WindowSizeItems)
            {
                var index = gvWindowSize.Rows.Add();
                var row = gvWindowSize.Rows[index];
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

            foreach (var item in _settings.MenuItems.StartProgramItems)
            {
                var cloneItem = (StartProgramMenuItem)item.Clone();
                var index = gvStartProgram.Rows.Add();
                var row = gvStartProgram.Rows[index];
                row.Cells[0].Value = cloneItem.Title;
                row.Cells[1].Value = cloneItem.FileName;
                row.Cells[2].Value = cloneItem.Arguments;
                row.Cells[3].ToolTipText = _languageManager.GetString("clm_start_program_edit");
                row.Cells[4].ToolTipText = _languageManager.GetString("clm_start_program_delete");
                row.Tag = cloneItem;
            }

            cmbLanguage.DisplayMember = "Text";
            cmbLanguage.ValueMember = "Value";

            var languageItems = new[] {
                new { Text = "", Value = "" },
                new { Text = "English", Value = "en" },
                new { Text = "Deutsch", Value = "de" },
                new { Text = "Français", Value = "fr" },
                new { Text = "Italiano", Value = "it" },
                new { Text = "Magyar", Value = "hu" },
                new { Text = "Português", Value = "pt" },
                new { Text = "Русский", Value = "ru" },
                new { Text = "Српски", Value = "sr" },
                new { Text = "简体中文", Value = "zh_cn" },
                new { Text = "繁體中文", Value = "zh_tw"},
                new { Text = "日本語", Value = "ja" },
                new { Text = "한국어", Value = "ko" }
            };

            cmbLanguage.DataSource = languageItems;
            cmbLanguage.SelectedValue = _settings.LanguageName;

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
            FillGridViewHotKeys(gvHotkeys, items);
        }

        private void GridViewStartProgramCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                if (e.ColumnIndex == 3 && row.Tag is StartProgramMenuItem menuItem)
                {
                    var dialog = new StartProgramForm(_settings, menuItem);
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
                if (e.ColumnIndex == 6 && grid.Rows[e.RowIndex].Tag is WindowSizeMenuItem menuItem)
                {
                    var dialog = new SizeSettingsForm(_settings, menuItem);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        var row = grid.Rows[e.RowIndex];
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
            var row = grid.Rows[e.RowIndex];
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && row.Tag is StartProgramMenuItem menuItem)
            {
                var dialog = new StartProgramForm(_settings, menuItem);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    row.Cells[0].Value = dialog.MenuItem.Title;
                    row.Cells[1].Value = dialog.MenuItem.FileName;
                    row.Cells[2].Value = dialog.MenuItem.Arguments;
                    row.Tag = dialog.MenuItem;
                }
            }
        }

        private void GridViewWindowSizeCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5) && e.RowIndex >= 0 && grid.Rows[e.RowIndex].Tag is WindowSizeMenuItem menuItem)
            {
                var dialog = new SizeSettingsForm(_settings, menuItem);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var row = grid.Rows[e.RowIndex];
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

        private void ButtonAddStartProgramClick(object sender, EventArgs e)
        {
            var dialog = new StartProgramForm(_settings, null);
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
            var dialog = new SizeSettingsForm(_settings, new WindowSizeMenuItem { Width = 1, Height = 1 });
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
                        FillGridViewHotKeys(gvHotkeys, items);
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
                        FillGridViewHotKeys(gvHotkeys, items);
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

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            var settings = new ApplicationSettings();

            foreach (DataGridViewRow row in gvWindowSize.Rows)
            {
                if (row.Tag is WindowSizeMenuItem item)
                {
                    settings.MenuItems.WindowSizeItems.Add(new WindowSizeMenuItem 
                    { 
                        Title = item.Title,
                        Left = item.Left,
                        Top = item.Top,
                        Width = item.Width, 
                        Height = item.Height,
                        Key1 = item.Key1,
                        Key2 = item.Key2,
                        Key3 = item.Key3
                    });
                }
            }

            foreach (DataGridViewRow row in gvStartProgram.Rows)
            {
                settings.MenuItems.StartProgramItems.Add((StartProgramMenuItem)row.Tag);
            }

            settings.MenuItems.Items = (IList<Settings.MenuItem>)gvHotkeys.Tag;
            settings.Sizer = (WindowSizerType)cmbSizer.SelectedIndex;
            settings.EnableHighDPI = chkEnableHighDPI.Checked;
            settings.LanguageName = cmbLanguage.SelectedValue == null ? string.Empty : cmbLanguage.SelectedValue.ToString();

            if (!_settings.Equals(_settings))
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
            var form = new HotkeysForm(_settings, menuItem);
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

        private void FillGridViewHotKeys(DataGridView gridView, IList<Settings.MenuItem> items)
        {
            gridView.Tag = items;
            foreach (var item in items)
            {
                if (item.Type == MenuItemType.Item)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    //var id = MenuItemId.GetId(item.Name);
                    //var title = GetTransparencyTitle(id);
                    //title = title != null ? title : _languageManager.GetString(item.Name);
                    //row.Tag = item;
                    //row.Cells[0].Value = title;
                    //row.Cells[1].Value = item == null ? "" : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                }

                if (item.Type == MenuItemType.Separator)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    var title = _languageManager.GetString("separator");
                    row.Tag = item;
                    row.Cells[0].Value = title;
                    row.Cells[1].Value = item == null ? "" : item.ToString();
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                }

                if (item.Type == MenuItemType.Group)
                {
                    var index = gridView.Rows.Add();
                    var row = gridView.Rows[index];
                    row.Tag = item;
                    row.Cells[0].Value = _languageManager.GetString(item.Name);
                    row.ReadOnly = true;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).Value = item.Show;
                    ((DataGridViewCheckBoxCell)row.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                    ((DataGridViewDisableButtonCell)row.Cells[3]).Enabled = false;

                    foreach (var subItem in item.Items)
                    {
                        if (subItem.Type == MenuItemType.Item)
                        {
                            var subItemIndex = gridView.Rows.Add();
                            var subItemRow = gridView.Rows[subItemIndex];
                            //var id = MenuItemId.GetId(subItem.Name);
                            //var title = GetTransparencyTitle(id);
                            //title = title != null ? title : _languageManager.GetString(subItem.Name);
                            //subItemRow.Tag = subItem;
                            //subItemRow.Cells[0].Value = title;
                            //subItemRow.Cells[1].Value = subItem == null ? "" : subItem.ToString();
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
                            subItemRow.Cells[0].Value = title;
                            subItemRow.Cells[1].Value = subItem == null ? "" : subItem.ToString();
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).Value = subItem.Show;
                            ((DataGridViewCheckBoxCell)subItemRow.Cells[2]).ToolTipText = _languageManager.GetString("clm_hotkeys_show_tooltip");
                            var padding = subItemRow.Cells[0].Style.Padding;
                            subItemRow.Cells[0].Style.Padding = new Padding(20, padding.Top, padding.Right, padding.Bottom);
                        }
                    }
                }
            }
        }

        private string GetTransparencyTitle(int id) => id switch
        {
            /*MenuItemId.SC_TRANS_00 => "0%" + _languageManager.GetString("trans_opaque"),
            MenuItemId.SC_TRANS_10 => "10%",
            MenuItemId.SC_TRANS_20 => "20%",
            MenuItemId.SC_TRANS_30 => "30%",
            MenuItemId.SC_TRANS_40 => "40%",
            MenuItemId.SC_TRANS_50 => "50%",
            MenuItemId.SC_TRANS_60 => "60%",
            MenuItemId.SC_TRANS_70 => "70%",
            MenuItemId.SC_TRANS_80 => "80%",
            MenuItemId.SC_TRANS_90 => "90%",
            MenuItemId.SC_TRANS_100 => "100%" + _languageManager.GetString("trans_invisible"),*/
            _ => string.Empty
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
