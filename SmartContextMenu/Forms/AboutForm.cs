using System;
using System.Windows.Forms;
using SmartContextMenu.Utils;

namespace SmartContextMenu.Forms
{
    partial class AboutForm : Form
    {
        private const string URL_SMART_CONTEXT_MENU = "https://github.com/AlexanderPro/SmartContextMenu";

        public AboutForm(LanguageManager manager)
        {
            InitializeComponent();
            btnOk.Text = manager.GetString("about_btn_ok");
            Text = manager.GetString("about_form") + AssemblyUtils.AssemblyProductName;
            lblProductName.Text = $"{AssemblyUtils.AssemblyProductName} v{AssemblyUtils.AssemblyProductVersion}";
            lblCopyright.Text = $"{AssemblyUtils.AssemblyCopyright} {AssemblyUtils.AssemblyCompany}";
            linkUrl.Text = URL_SMART_CONTEXT_MENU;
        }

        private void CloseClick(object sender, EventArgs e) => Close();

        private void LinkClick(object sender, EventArgs e) => SystemUtils.RunAs(SystemUtils.GetDefaultBrowserModuleName(), URL_SMART_CONTEXT_MENU, true);

        private void KeyDownClick(object sender, KeyEventArgs e) => CloseClick(sender, e);
    }
}
