using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;
using System.Resources;
using System.Reflection;
using SmartContextMenu.Forms;
using SmartContextMenu.Utils;
using SmartContextMenu.Extensions;
using SmartContextMenu.Settings;
using SmartContextMenu.Native;
using SmartContextMenu.Native.Enums;

namespace SmartContextMenu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            Application.ThreadException += OnThreadException;

            var settings = ApplicationSettingsFile.Read();
            
            // Enable High DPI Support
            if (settings.EnableHighDPI)
            {
                SystemUtils.EnableHighDPISupport();
            }

            // Command Line Interface
            var commandLineParser = new CommandLineParser(args);
            if (commandLineParser.HasToggle("help"))
            {
                var dialog = new MessageBoxForm();
                dialog.Message = BuildHelpString();
                dialog.Text = "Help";
                dialog.ShowDialog();
                return;
            }

            ProcessCommandLine(commandLineParser, settings);

            if (commandLineParser.HasToggle("n") || commandLineParser.HasToggle("nogui"))
            {
                return;
            }


            var mutex = new Mutex(false, AssemblyUtils.AssemblyTitle, out var createNew);
            if (!createNew)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(settings));
        }

        static void ProcessCommandLine(CommandLineParser сommandLineParser, ApplicationSettings settings)
        {
            // Delay
            if (сommandLineParser.HasToggle("d") || сommandLineParser.HasToggle("delay"))
            {
                var delayString = сommandLineParser.GetToggleValueOrDefault("d", null) ?? сommandLineParser.GetToggleValueOrDefault("delay", null);
                if (int.TryParse(delayString, out var delay))
                {
                    Thread.Sleep(delay);
                }
            }

            // Clear Clipboard
            if (сommandLineParser.HasToggle("clearclipboard"))
            {
                Clipboard.Clear();
            }

            var windowHandles = new List<IntPtr>();
            var processId = (int?)null;
            if (сommandLineParser.HasToggle("processId"))
            {
                var processIdString = сommandLineParser.GetToggleValueOrDefault("processId", null);
                processId = !string.IsNullOrWhiteSpace(processIdString) && int.TryParse(processIdString, out var pid) ? pid : (int?)null;
            }

            if (сommandLineParser.HasToggle("handle"))
            {
                var windowHandleString = сommandLineParser.GetToggleValueOrDefault("handle", null);
                if (!string.IsNullOrWhiteSpace(windowHandleString))
                {
                    var windowHandle = windowHandleString.StartsWith("0x") ? int.TryParse(windowHandleString.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier, null, out var number) ? new IntPtr(number) :
                        IntPtr.Zero : int.TryParse(windowHandleString, out var number2) ? new IntPtr(number2) : IntPtr.Zero;
                    windowHandles.Add(windowHandle);
                }
            }

            if (сommandLineParser.HasToggle("title"))
            {
                var windowTitle = сommandLineParser.GetToggleValueOrDefault("title", null);
                var handles = WindowUtils.FindWindowByTitle(windowTitle, processId, (value, title) => string.Compare(value, title, true) == 0);
                windowHandles.AddRange(handles);
            }

            if (сommandLineParser.HasToggle("titleBegins"))
            {
                var windowTitle = сommandLineParser.GetToggleValueOrDefault("titleBegins", null);
                var handles = WindowUtils.FindWindowByTitle(windowTitle, processId, (value, title) => title.StartsWith(value, StringComparison.OrdinalIgnoreCase));
                windowHandles.AddRange(handles);
            }

            if (сommandLineParser.HasToggle("titleEnds"))
            {
                var windowTitle = сommandLineParser.GetToggleValueOrDefault("titleEnds", null);
                var handles = WindowUtils.FindWindowByTitle(windowTitle, processId, (value, title) => title.EndsWith(value, StringComparison.OrdinalIgnoreCase));
                windowHandles.AddRange(handles);
            }

            if (сommandLineParser.HasToggle("titleContains"))
            {
                var windowTitle = сommandLineParser.GetToggleValueOrDefault("titleContains", null);
                var handles = WindowUtils.FindWindowByTitle(windowTitle, processId, (value, title) => title.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                windowHandles.AddRange(handles);
            }


            foreach (var windowHandle in windowHandles.Where(x => x != IntPtr.Zero && User32.GetParent(x) == IntPtr.Zero))
            {
                var window = new Window(windowHandle);

                // Set a Window monitor
                if (сommandLineParser.HasToggle("m") || сommandLineParser.HasToggle("monitor"))
                {
                    var monitorString = сommandLineParser.GetToggleValueOrDefault("m", null) ?? сommandLineParser.GetToggleValueOrDefault("monitor", null);
                    if (int.TryParse(monitorString, out var monitor))
                    {
                        var monitorItem = SystemUtils.GetMonitors().Select((x, i) => new { Index = i, MonitorHandle = x }).FirstOrDefault(x => x.Index == monitor);
                        if (monitorItem != null)
                        {
                            window.MoveToMonitor(monitorItem.MonitorHandle);
                        }
                    }
                }

                // Set a Window width
                if (сommandLineParser.HasToggle("w") || сommandLineParser.HasToggle("width"))
                {
                    var widthString = сommandLineParser.GetToggleValueOrDefault("w", null) ?? сommandLineParser.GetToggleValueOrDefault("width", null);
                    if (int.TryParse(widthString, out var width))
                    {
                        window.SetWidth(width);
                    }
                }

                // Set a Window height
                if (сommandLineParser.HasToggle("h") || сommandLineParser.HasToggle("height"))
                {
                    var heightString = сommandLineParser.GetToggleValueOrDefault("h", null) ?? сommandLineParser.GetToggleValueOrDefault("height", null);
                    if (int.TryParse(heightString, out var height))
                    {
                        window.SetHeight(height);
                    }
                }

                // Set a Window left position
                if (сommandLineParser.HasToggle("l") || сommandLineParser.HasToggle("left"))
                {
                    var leftString = сommandLineParser.GetToggleValueOrDefault("l", null) ?? сommandLineParser.GetToggleValueOrDefault("left", null);
                    if (int.TryParse(leftString, out var left))
                    {
                        window.SetLeft(left);
                    }
                }

                // Set a Window top position
                if (сommandLineParser.HasToggle("t") || сommandLineParser.HasToggle("top"))
                {
                    var topString = сommandLineParser.GetToggleValueOrDefault("t", null) ?? сommandLineParser.GetToggleValueOrDefault("top", null);
                    if (int.TryParse(topString, out var top))
                    {
                        window.SetTop(top);
                    }
                }

                // Set a Window position
                if (сommandLineParser.HasToggle("a") || сommandLineParser.HasToggle("alignment"))
                {
                    var windowAlignmentString = сommandLineParser.GetToggleValueOrDefault("a", null) ?? сommandLineParser.GetToggleValueOrDefault("alignment", null);
                    var windowAlignment = Enum.TryParse<WindowAlignment>(windowAlignmentString, true, out var alignment) ? alignment : 0;
                    window.SetAlignment(windowAlignment);
                }

                // Set a Window transparency
                if (сommandLineParser.HasToggle("transparency"))
                {
                    if (byte.TryParse(сommandLineParser.GetToggleValueOrDefault("transparency", null), out var transparency))
                    {
                        transparency = transparency > 100 ? (byte)100 : transparency;
                        window.SetTransparency(transparency);
                    }
                }

                // Set a Process priority
                if (сommandLineParser.HasToggle("p") || сommandLineParser.HasToggle("priority"))
                {
                    var processPriorityString = сommandLineParser.GetToggleValueOrDefault("p", null) ?? сommandLineParser.GetToggleValueOrDefault("priority", null);
                    var processPriority = Enum.TryParse<Priority>(processPriorityString, true, out var priority) ? priority : 0;
                    window.SetPriority(processPriority);
                }

                // Set a Window AlwaysOnTop
                if (сommandLineParser.HasToggle("alwaysontop"))
                {
                    var alwaysontopString = сommandLineParser.GetToggleValueOrDefault("alwaysontop", string.Empty).ToLower();

                    if (alwaysontopString == "on")
                    {
                        window.MakeAlwaysOnTop(true);
                    }

                    if (alwaysontopString == "off")
                    {
                        window.MakeAlwaysOnTop(false);
                    }
                }

                // Set a Window Aero Glass
                if (сommandLineParser.HasToggle("g") || сommandLineParser.HasToggle("aeroglass"))
                {
                    var aeroglassString = (сommandLineParser.GetToggleValueOrDefault("g", null) ?? сommandLineParser.GetToggleValueOrDefault("aeroglass", string.Empty)).ToLower();
                    var enabled = aeroglassString == "on" ? true : aeroglassString == "off" ? false : (bool?)null;

                    if (enabled.HasValue)
                    {
                        var version = Environment.OSVersion.Version;
                        if (version.Major == 6 && (version.Minor == 0 || version.Minor == 1))
                        {
                            WindowUtils.AeroGlassForVistaAndSeven(window.Handle, enabled.Value);
                        }
                        else if (version.Major >= 6 || (version.Major == 6 && version.Minor > 1))
                        {
                            WindowUtils.AeroGlassForEightAndHigher(window.Handle, enabled.Value);
                        }
                    }
                }

                // Hide For Alt+Tab
                if (сommandLineParser.HasToggle("hidealttab"))
                {
                    var hideAltTabString = сommandLineParser.GetToggleValueOrDefault("hidealttab", string.Empty).ToLower();

                    if (hideAltTabString == "on")
                    {
                        window.HideForAltTab(true);
                    }

                    if (hideAltTabString == "off")
                    {
                        window.HideForAltTab(false);
                    }
                }

                // Click Through
                if (сommandLineParser.HasToggle("clickthrough"))
                {
                    var clickthroughString = сommandLineParser.GetToggleValueOrDefault("clickthrough", string.Empty).ToLower();

                    if (clickthroughString == "on")
                    {
                        window.ClickThrough(true);
                    }

                    if (clickthroughString == "off")
                    {
                        window.ClickThrough(false);
                    }
                }

                // Send To Bottom Window
                if (сommandLineParser.HasToggle("sendtobottom"))
                {
                    window.SendToBottom();
                }

                // Open File In Explorer
                if (сommandLineParser.HasToggle("o") || сommandLineParser.HasToggle("openinexplorer"))
                {
                    try
                    {
                        SystemUtils.RunAs("explorer.exe", "/select, " + window.Process.GetMainModuleFileName(), true);
                    }
                    catch
                    {
                    }
                }

                // Copy to clipboard
                if (сommandLineParser.HasToggle("c") || сommandLineParser.HasToggle("copytoclipboard"))
                {
                    var text = window.ExtractText();
                    if (text != null)
                    {
                        Clipboard.SetText(text);
                    }
                }

                // Copy Screenshot
                if (сommandLineParser.HasToggle("copyscreenshot"))
                {
                    var bitmap = WindowUtils.PrintWindow(window.Handle);
                    Clipboard.SetImage(bitmap);
                }

                //Information dialog
                if (сommandLineParser.HasToggle("i") || сommandLineParser.HasToggle("information"))
                {
                    var dialog = new InformationForm(settings, window.GetWindowInfo());
                    dialog.ShowDialog();
                }

                //Save Screenshot
                if (сommandLineParser.HasToggle("s") || сommandLineParser.HasToggle("savescreenshot"))
                {
                    var resourceManager = new ResourceManager($"SmartContextMenu.Resources.{settings.LanguageName}", Assembly.GetExecutingAssembly());
                    var bitmap = WindowUtils.PrintWindow(window.Handle);
                    var dialog = new SaveFileDialog
                    {
                        OverwritePrompt = true,
                        ValidateNames = true,
                        Title = resourceManager.GetString("save_screenshot_title"),
                        FileName = resourceManager.GetString("save_screenshot_filename"),
                        DefaultExt = resourceManager.GetString("save_screenshot_default_ext"),
                        RestoreDirectory = false,
                        Filter = resourceManager.GetString("save_screenshot_filter")
                    };
                    if (dialog.ShowDialog(window.Win32Window) == DialogResult.OK)
                    {
                        var fileExtension = Path.GetExtension(dialog.FileName).ToLower();
                        var imageFormat = fileExtension == ".bmp" ? ImageFormat.Bmp :
                            fileExtension == ".gif" ? ImageFormat.Gif :
                            fileExtension == ".jpeg" ? ImageFormat.Jpeg :
                            fileExtension == ".png" ? ImageFormat.Png :
                            fileExtension == ".tiff" ? ImageFormat.Tiff :
                            fileExtension == ".wmf" ? ImageFormat.Wmf : ImageFormat.Bmp;

                        bitmap.Save(dialog.FileName, imageFormat);
                    }
                }

                // Disable "Minimize" Button 
                if (сommandLineParser.HasToggle("minimizebutton"))
                {
                    var minimizebuttonString = сommandLineParser.GetToggleValueOrDefault("minimizebutton", string.Empty).ToLower();

                    if (minimizebuttonString == "on")
                    {
                        window.DisableMinimizeButton(false);
                    }

                    if (minimizebuttonString == "off")
                    {
                        window.DisableMinimizeButton(true);
                    }
                }

                // Disable "Maximize" Button 
                if (сommandLineParser.HasToggle("maximizebutton"))
                {
                    var maximizebuttonString = сommandLineParser.GetToggleValueOrDefault("maximizebutton", string.Empty).ToLower();

                    if (maximizebuttonString == "on")
                    {
                        window.DisableMaximizeButton(false);
                    }

                    if (maximizebuttonString == "off")
                    {
                        window.DisableMaximizeButton(true);
                    }
                }
            }
        }

        static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ex ??= new Exception("OnCurrentDomainUnhandledException");
            OnThreadException(sender, new ThreadExceptionEventArgs(ex));
        }

        static void OnThreadException(object sender, ThreadExceptionEventArgs e) =>
            MessageBox.Show(e.Exception.ToString(), AssemblyUtils.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

        static string BuildHelpString() =>
                @"   --help             The help
   --title            Title
   --titleBegins      Title begins 
   --titleEnds        Title ends
   --titleContains    Title contains
   --handle           Handle (1234567890) (0xFFFFFF)
   --processId        PID (1234567890)
-d --delay            Delay in milliseconds
-l --left             Left
-t --top              Top
-w --width            Width
-h --height           Height
-i --information      Information dialog
-s --savescreenshot   Save Screenshot
-m --monitor          [0, 1, 2, 3, ...]
-a --alignment        [topleft,
                       topcenter,
                       topright,
                       middleleft,
                       middlecenter,
                       middleright,
                       bottomleft,
                       bottomcenter,
                       bottomright,
                       centerhorizontally,
                       centervertically]
-p --priority         [realtime,
                       high,
                       abovenormal,
                       normal,
                       belownormal,
                       idle]
   --transparency     [0 ... 100]
   --alwaysontop      [on, off]
-g --aeroglass        [on, off]
   --hidealttab       [on, off]
   --clickthrough     [on, off]
   --minimizebutton   [on, off]
   --maximizebutton   [on, off]
   --sendtobottom     Send To Bottom
-o --openinexplorer   Open File In Explorer
-c --copytoclipboard  Copy Window Text To Clipboard
   --copyscreenshot   Copy Screenshot To Clipboard
   --clearclipboard   Clear Clipboard
-n --nogui            No GUI

Example:
SmartContextMenu.exe --title ""Untitled - Notepad"" -a topleft -p high --alwaysontop on --nogui";
    }
}
