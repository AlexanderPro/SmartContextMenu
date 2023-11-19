using System;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using SmartContextMenu.Settings;
using SmartContextMenu.Utils;

namespace SmartContextMenu.Forms
{
    partial class AboutForm : Form
    {
        private const string URL_SMART_CONTEXT_MENU = "https://github.com/AlexanderPro/SmartContextMenu";

        public AboutForm(ApplicationSettings settings)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            btnOk.Text = resourceManager.GetString("about_btn_ok");
            Text = resourceManager.GetString("about_form") + AssemblyUtils.AssemblyProductName;
            lblProductName.Text = $"{AssemblyUtils.AssemblyProductName} v{AssemblyUtils.AssemblyProductVersion}";
            lblCopyright.Text = $"{AssemblyUtils.AssemblyCopyright}-{DateTime.Now.Year} {AssemblyUtils.AssemblyCompany}";
            linkUrl.Text = URL_SMART_CONTEXT_MENU;
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkClick(object sender, EventArgs e)
        {
            SystemUtils.RunAs(SystemUtils.GetDefaultBrowserModuleName(), URL_SMART_CONTEXT_MENU, true);
        }

        private void KeyDownClick(object sender, KeyEventArgs e)
        {
            CloseClick(sender, e);
        }
    }
}
