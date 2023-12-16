using System;
using System.Windows.Forms;
using System.Globalization;

namespace SmartContextMenu.Forms
{
    partial class InformationForm : Form
    {
        public InformationForm(LanguageManager manager, WindowDetails windowDetails)
        {
            InitializeComponent();
            InitializeControls(manager, windowDetails);
        }

        private void InitializeControls(LanguageManager manager, WindowDetails windowDetails)
        {
            grpWindow.Text = manager.GetString("grp_window");
            grpProcess.Text = manager.GetString("grp_process");
            lblGetWindowText.Text = manager.GetString("lbl_get_window_text");
            lblWmGetText.Text = manager.GetString("lbl_wm_gettext");
            lblGetClassName.Text = manager.GetString("lbl_get_class_name");
            lblRealGetWindowClass.Text = manager.GetString("lbl_real_get_window_class");
            lblFontName.Text = manager.GetString("lbl_font_name");
            lblWindowHandle.Text = manager.GetString("lbl_window_handle");
            lblParentWindowHandle.Text = manager.GetString("lbl_parent_window_handle");
            lblWindowPosition.Text = manager.GetString("lbl_window_position");
            lblWindowSize.Text = manager.GetString("lbl_window_size");
            lblExtendedFrameBounds.Text = manager.GetString("lbl_extended_frame_bounds");
            lblInstance.Text = manager.GetString("lbl_instance");
            lblProcessId.Text = manager.GetString("lbl_process_id");
            lblThreadId.Text = manager.GetString("lbl_thread_id");
            lblGwlStyle.Text = manager.GetString("lbl_gwl_style");
            lblGclStyle.Text = manager.GetString("lbl_gcl_style");
            lblGwlExStyle.Text = manager.GetString("lbl_gwl_exstyle");
            lblWindowInfoExStyle.Text = manager.GetString("lbl_windowinfo_exstyle");
            lblLwaAlpha.Text = manager.GetString("lbl_lwa_alpha");
            lblLwaColorKey.Text = manager.GetString("lbl_lwa_colorkey");
            lblGwlUserData.Text = manager.GetString("lbl_gwl_userdata");
            lblDwlUser.Text = manager.GetString("lbl_dwl_user");
            lblFullPath.Text = manager.GetString("lbl_full_path");
            lblCommandLine.Text = manager.GetString("lbl_command_line");
            lblStartedAt.Text = manager.GetString("lbl_started_at");
            lblOwner.Text = manager.GetString("lbl_owner");
            lblThreads.Text = manager.GetString("lbl_threads");
            lblWorkingSetSize.Text = manager.GetString("lbl_working_set_size");
            lblParent.Text = manager.GetString("lbl_parent");
            lblPriority.Text = manager.GetString("lbl_priority");
            lblHandles.Text = manager.GetString("lbl_handles");
            lblVirtualSize.Text = manager.GetString("lbl_virtual_size");
            lblProductName.Text = manager.GetString("lbl_product_name");
            lblCopyright.Text = manager.GetString("lbl_copyright");
            lblFileVersion.Text = manager.GetString("lbl_file_version");
            lblProductVersion.Text = manager.GetString("lbl_product_version");
            btnOk.Text = manager.GetString("information_btn_apply");
            Text = manager.GetString("information");

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

        private void ButtonCloseClick(object sender, EventArgs e) => Close();

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
    }
}
