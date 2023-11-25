using System;
using System.Resources;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using SmartContextMenu.Settings;

namespace SmartContextMenu.Forms
{
    partial class InformationForm : Form
    {
        public InformationForm(ApplicationSettings settings, WindowDetails windowDetails)
        {
            InitializeComponent();
            var resourceManager = new ResourceManager($"SmartContextMenu.Resource.{settings.LanguageName}.resx", Assembly.GetExecutingAssembly());
            InitializeControls(resourceManager, windowDetails);
        }

        private void InitializeControls(ResourceManager resourceManager, WindowDetails windowDetails)
        {
            grpWindow.Text = resourceManager.GetString("grp_window");
            grpProcess.Text = resourceManager.GetString("grp_process");
            lblGetWindowText.Text = resourceManager.GetString("lbl_get_window_text");
            lblWmGetText.Text = resourceManager.GetString("lbl_wm_gettext");
            lblGetClassName.Text = resourceManager.GetString("lbl_get_class_name");
            lblRealGetWindowClass.Text = resourceManager.GetString("lbl_real_get_window_class");
            lblFontName.Text = resourceManager.GetString("lbl_font_name");
            lblWindowHandle.Text = resourceManager.GetString("lbl_window_handle");
            lblParentWindowHandle.Text = resourceManager.GetString("lbl_parent_window_handle");
            lblWindowPosition.Text = resourceManager.GetString("lbl_window_position");
            lblWindowSize.Text = resourceManager.GetString("lbl_window_size");
            lblExtendedFrameBounds.Text = resourceManager.GetString("lbl_extended_frame_bounds");
            lblInstance.Text = resourceManager.GetString("lbl_instance");
            lblProcessId.Text = resourceManager.GetString("lbl_process_id");
            lblThreadId.Text = resourceManager.GetString("lbl_thread_id");
            lblGwlStyle.Text = resourceManager.GetString("lbl_gwl_style");
            lblGclStyle.Text = resourceManager.GetString("lbl_gcl_style");
            lblGwlExStyle.Text = resourceManager.GetString("lbl_gwl_exstyle");
            lblWindowInfoExStyle.Text = resourceManager.GetString("lbl_windowinfo_exstyle");
            lblLwaAlpha.Text = resourceManager.GetString("lbl_lwa_alpha");
            lblLwaColorKey.Text = resourceManager.GetString("lbl_lwa_colorkey");
            lblGwlUserData.Text = resourceManager.GetString("lbl_gwl_userdata");
            lblDwlUser.Text = resourceManager.GetString("lbl_dwl_user");
            lblFullPath.Text = resourceManager.GetString("lbl_full_path");
            lblCommandLine.Text = resourceManager.GetString("lbl_command_line");
            lblStartedAt.Text = resourceManager.GetString("lbl_started_at");
            lblOwner.Text = resourceManager.GetString("lbl_owner");
            lblThreads.Text = resourceManager.GetString("lbl_threads");
            lblWorkingSetSize.Text = resourceManager.GetString("lbl_working_set_size");
            lblParent.Text = resourceManager.GetString("lbl_parent");
            lblPriority.Text = resourceManager.GetString("lbl_priority");
            lblHandles.Text = resourceManager.GetString("lbl_handles");
            lblVirtualSize.Text = resourceManager.GetString("lbl_virtual_size");
            lblProductName.Text = resourceManager.GetString("lbl_product_name");
            lblCopyright.Text = resourceManager.GetString("lbl_copyright");
            lblFileVersion.Text = resourceManager.GetString("lbl_file_version");
            lblProductVersion.Text = resourceManager.GetString("lbl_product_version");
            btnOk.Text = resourceManager.GetString("information_btn_apply");
            Text = resourceManager.GetString("information");

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
