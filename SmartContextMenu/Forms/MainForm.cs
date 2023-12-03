using System;
using System.Windows.Forms;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;

namespace SmartContextMenu.Forms
{
    public partial class MainForm : Form
    {
        private ApplicationSettings _settings;
        private readonly SystemTrayMenu _systemTrayMenu;
        private AboutForm _aboutForm;
        private ApplicationSettingsForm _settingsForm;


        public MainForm(ApplicationSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            _systemTrayMenu = new SystemTrayMenu();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);


            if (_settings.ShowSystemTrayIcon)
            {
                _systemTrayMenu.MenuItemAutoStartClick += MenuItemAutoStartClick;
                _systemTrayMenu.MenuItemSettingsClick += MenuItemSettingsClick;
                _systemTrayMenu.MenuItemAboutClick += MenuItemAboutClick;
                _systemTrayMenu.MenuItemExitClick += MenuItemExitClick;
                _systemTrayMenu.Build(_settings);
                _systemTrayMenu.CheckMenuItemAutoStart(AutoStarter.IsAutoStartByRegisterEnabled(AssemblyUtils.AssemblyProductName, AssemblyUtils.AssemblyLocation));
            }
        }

        private void MenuItemAutoStartClick(object sender, EventArgs e)
        {
            var keyName = AssemblyUtils.AssemblyProductName;
            var assemblyLocation = AssemblyUtils.AssemblyLocation;
            var autoStartEnabled = AutoStarter.IsAutoStartByRegisterEnabled(keyName, assemblyLocation);
            if (autoStartEnabled)
            {
                AutoStarter.UnsetAutoStartByRegister(keyName);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.UnsetAutoStartByScheduler(keyName);
                }
            }
            else
            {
                AutoStarter.SetAutoStartByRegister(keyName, assemblyLocation);
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    AutoStarter.SetAutoStartByScheduler(keyName, assemblyLocation);
                }
            }
            ((ToolStripMenuItem)sender).Checked = !autoStartEnabled;
        }

        private void MenuItemAboutClick(object sender, EventArgs e)
        {
            if (_aboutForm == null || _aboutForm.IsDisposed || !_aboutForm.IsHandleCreated)
            {
                _aboutForm = new AboutForm(_settings);
            }
            _aboutForm.Show();
            _aboutForm.Activate();
        }

        private void MenuItemSettingsClick(object sender, EventArgs e)
        {
            if (_settingsForm == null || _settingsForm.IsDisposed || !_settingsForm.IsHandleCreated)
            {
                _settingsForm = new ApplicationSettingsForm(_settings);
                _settingsForm.OkClick += (sender, e) => 
                {
                    _settings = e.Entity;
                    _systemTrayMenu.RefreshLanguage(_settings);
                    ApplicationSettingsFile.Save(_settings);
                };
            }

            _settingsForm.Show();
            _settingsForm.Activate();
        }

        private void MenuItemExitClick(object sender, EventArgs e) => Close();
    }
}
