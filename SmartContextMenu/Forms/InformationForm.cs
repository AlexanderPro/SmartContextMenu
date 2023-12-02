using System;
using System.Windows.Forms;
using System.Globalization;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class InformationForm : Form
    {
        public InformationForm(ApplicationSettings settings, WindowDetails windowDetails)
        {
            InitializeComponent();
            var languageManager = new LanguageManager(settings.LanguageName);
            InitializeControls(languageManager, windowDetails);
        }

        private void InitializeControls(LanguageManager languageManager, WindowDetails windowDetails)
        {
            grpWindow.Text = languageManager.GetString("grp_window");
            grpProcess.Text = languageManager.GetString("grp_process");
            lblGetWindowText.Text = languageManager.GetString("lbl_get_window_text");
            lblWmGetText.Text = languageManager.GetString("lbl_wm_gettext");
            lblGetClassName.Text = languageManager.GetString("lbl_get_class_name");
            lblRealGetWindowClass.Text = languageManager.GetString("lbl_real_get_window_class");
            lblFontName.Text = languageManager.GetString("lbl_font_name");
            lblWindowHandle.Text = languageManager.GetString("lbl_window_handle");
            lblParentWindowHandle.Text = languageManager.GetString("lbl_parent_window_handle");
            lblWindowPosition.Text = languageManager.GetString("lbl_window_position");
            lblWindowSize.Text = languageManager.GetString("lbl_window_size");
            lblExtendedFrameBounds.Text = languageManager.GetString("lbl_extended_frame_bounds");
            lblInstance.Text = languageManager.GetString("lbl_instance");
            lblProcessId.Text = languageManager.GetString("lbl_process_id");
            lblThreadId.Text = languageManager.GetString("lbl_thread_id");
            lblGwlStyle.Text = languageManager.GetString("lbl_gwl_style");
            lblGclStyle.Text = languageManager.GetString("lbl_gcl_style");
            lblGwlExStyle.Text = languageManager.GetString("lbl_gwl_exstyle");
            lblWindowInfoExStyle.Text = languageManager.GetString("lbl_windowinfo_exstyle");
            lblLwaAlpha.Text = languageManager.GetString("lbl_lwa_alpha");
            lblLwaColorKey.Text = languageManager.GetString("lbl_lwa_colorkey");
            lblGwlUserData.Text = languageManager.GetString("lbl_gwl_userdata");
            lblDwlUser.Text = languageManager.GetString("lbl_dwl_user");
            lblFullPath.Text = languageManager.GetString("lbl_full_path");
            lblCommandLine.Text = languageManager.GetString("lbl_command_line");
            lblStartedAt.Text = languageManager.GetString("lbl_started_at");
            lblOwner.Text = languageManager.GetString("lbl_owner");
            lblThreads.Text = languageManager.GetString("lbl_threads");
            lblWorkingSetSize.Text = languageManager.GetString("lbl_working_set_size");
            lblParent.Text = languageManager.GetString("lbl_parent");
            lblPriority.Text = languageManager.GetString("lbl_priority");
            lblHandles.Text = languageManager.GetString("lbl_handles");
            lblVirtualSize.Text = languageManager.GetString("lbl_virtual_size");
            lblProductName.Text = languageManager.GetString("lbl_product_name");
            lblCopyright.Text = languageManager.GetString("lbl_copyright");
            lblFileVersion.Text = languageManager.GetString("lbl_file_version");
            lblProductVersion.Text = languageManager.GetString("lbl_product_version");
            btnOk.Text = languageManager.GetString("information_btn_apply");
            Text = languageManager.GetString("information");

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone(); ;
            nfi.NumberGroupSeparator = ",";
            txtGetWindowText.Text = windowDetails.GetWindowText;
            txtWmGetText.Text = windowDetails.WM_GETTEXT;
            txtGetClassName.Text = windowDetails.GetClassName;
            txtRealGetWindowClass.Text = windowDetails.RealGetWindowClass;
            txtFontName.Text = windowDetails.FontName;
            txtWindowHandle.Text = $"0x{windowDetails.Handle.ToInt64():X}";
            txtParentWindowHandle.Text = $"0x{windowDetails.ParentHandle.ToInt64():X}";
            txtWindowPosition.Text = $"{windowDetails.Size.Left}, {windowDetails.Size.Top}";
            txtWindowSize.Text = $"{windowDetails.Size.Width}x{windowDetails.Size.Height}  ({windowDetails.ClientSize.Width}x{windowDetails.ClientSize.Height})";
            txtInstance.Text = $"0x{windowDetails.Instance.ToInt64():X}";
            txtProcessId.Text = $"0x{windowDetails.ProcessId:X} ({windowDetails.ProcessId})";
            txtThreadId.Text = $"0x{windowDetails.ThreadId:X} ({windowDetails.ThreadId})";
            txtExtendedFrameBounds.Text = $"{windowDetails.FrameBounds.Top} {windowDetails.FrameBounds.Right} {windowDetails.FrameBounds.Bottom} {windowDetails.FrameBounds.Left}";
            txtGwlStyle.Text = $"0x{windowDetails.GWL_STYLE:X}";
            txtGclStyle.Text = $"0x{windowDetails.GCL_STYLE:X}";
            txtGwlExStyle.Text = $"0x{windowDetails.GWL_EXSTYLE:X}";
            txtWindowInfoExStyle.Text = $"0x{windowDetails.WindowInfoExStyle:X}";
            txtLwaAlpha.Text = windowDetails.LWA_ALPHA ? "+" : "-";
            txtLwaColorKey.Text = windowDetails.LWA_COLORKEY ? "+" : "-";
            txtGwlUserData.Text = $"0x{windowDetails.GWL_USERDATA:X}";
            txtDwlUser.Text = $"0x{windowDetails.DWL_USER:X}";
            txtFullPath.Text = windowDetails.FullPath;
            txtCommandLine.Text = windowDetails.CommandLine;
            txtStartedAt.Text = windowDetails.StartTime == null ? "" : windowDetails.StartTime.Value.ToString("dd.MM.yyyy HH:mm:ss");
            txtOwner.Text = windowDetails.Owner;
            txtParent.Text = windowDetails.Parent;
            txtPriority.Text = windowDetails.Priority.ToString();
            txtThreads.Text = windowDetails.ThreadCount.ToString();
            txtHandles.Text = windowDetails.HandleCount.ToString();
            txtWorkingSetSize.Text = ((decimal)windowDetails.WorkingSetSize).ToString("#,0", nfi);
            txtVirtualSize.Text = ((decimal)windowDetails.VirtualSize).ToString("#,0", nfi);
            txtProductName.Text = windowDetails.ProductName;
            txtCopyright.Text = windowDetails.Copyright;
            txtFileVersion.Text = windowDetails.FileVersion;
            txtProductVersion.Text = windowDetails.ProductVersion;
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
