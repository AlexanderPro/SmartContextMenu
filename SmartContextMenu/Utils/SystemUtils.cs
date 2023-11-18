using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Management;
using Microsoft.Win32;
using SmartContextMenu.Native;
using SmartContextMenu.Native.Enums;
using SmartContextMenu.Native.Structs;
using static SmartContextMenu.Native.User32;
using static SmartContextMenu.Native.Kernel32;
using static SmartContextMenu.Native.SHCore;
using static SmartContextMenu.Native.Constants;

namespace SmartContextMenu
{
    static class SystemUtils
    {
        public static ProcessInfo GetProcessInfo(int processId)
        {
            using var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId = {processId}");
            using var objects = searcher.Get();
            var processInfo = new ProcessInfo();
            foreach (ManagementObject obj in objects)
            {
                var argList = new string[] { string.Empty, string.Empty };
                var returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    processInfo.Owner = argList[1] + "\\" + argList[0];
                    break;
                }
            }

            var baseObject = objects.Cast<ManagementBaseObject>().FirstOrDefault();
            if (baseObject != null)
            {
                processInfo.CommandLine = baseObject["CommandLine"] != null ? baseObject["CommandLine"].ToString() : string.Empty;
                processInfo.HandleCount = baseObject["HandleCount"] != null ? (uint)baseObject["HandleCount"] : 0;
                processInfo.ThreadCount = baseObject["ThreadCount"] != null ? (uint)baseObject["ThreadCount"] : 0;
                processInfo.VirtualSize = baseObject["VirtualSize"] != null ? (ulong)baseObject["VirtualSize"] : 0;
                processInfo.WorkingSetSize = baseObject["WorkingSetSize"] != null ? (ulong)baseObject["WorkingSetSize"] : 0;
            }

            return processInfo;
        }

        public static IList<IntPtr> GetMonitors()
        {
            var monitors = new List<IntPtr>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect rect, IntPtr data) =>
            {
                monitors.Add(hMonitor);
                return true;
            }, IntPtr.Zero);
            return monitors;
        }

        public static Process GetProcessByIdSafely(int pId)
        {
            try
            {
                return Process.GetProcessById(pId);
            }
            catch
            {
                return null;
            }
        }

        public static void RunAs(string fileName, string arguments, bool showWindow, string workinDirectory = null)
        {
            foreach (var fullFileName in GetFullPaths(fileName))
            {
                var process = new Process();
                process.StartInfo.FileName = fullFileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = !string.IsNullOrEmpty(workinDirectory) ? workinDirectory : Path.GetDirectoryName(fullFileName);
                if (!showWindow)
                {
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                }
                process.Start();
            }
        }

        public static string GetDefaultBrowserModuleName()
        {
            var browserName = "iexplore.exe";
            using var userChoiceKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice");
            if (userChoiceKey == null)
            {
                return browserName;
            }

            var progIdValue = userChoiceKey.GetValue("Progid");
            if (progIdValue == null)
            {
                return browserName;
            }

            var progId = progIdValue.ToString();
            var path = progId + "\\shell\\open\\command";
            using var pathKey = Registry.ClassesRoot.OpenSubKey(path);
            if (pathKey == null)
            {
                return browserName;
            }

            try
            {
                path = pathKey.GetValue(null).ToString().ToLower().Replace("\"", "");
                const string exeSuffix = ".exe";
                if (!path.EndsWith(exeSuffix))
                {
                    path = path.Substring(0, path.LastIndexOf(exeSuffix, StringComparison.Ordinal) + exeSuffix.Length);
                }
                return path;
            }
            catch
            {
                return browserName;
            }
        }

        private static List<string> GetFullPaths(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new List<string> { Path.GetFullPath(fileName) };
            }

            var fullPaths = new List<string>();
            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                {
                    fullPaths.Add(fullPath);
                }
            }
            return fullPaths;
        }

        public static bool TerminateProcess(int processId, uint exitCode)
        {
            var hProcess = OpenProcess(PROCESS_TERMINATE, false, processId);
            if (hProcess != IntPtr.Zero)
            {
                try
                {
                    return Kernel32.TerminateProcess(hProcess, exitCode);
                }
                catch
                {
                    CloseHandle(hProcess);
                }
            }
            return false;
        }

        public static void EnableHighDPISupport()
        {
            if (Environment.OSVersion.Version >= new Version(6, 3, 0)) // win 8.1 added support for per monitor dpi
            {
                if (Environment.OSVersion.Version >= new Version(10, 0, 15063)) // win 10 creators update added support for per monitor v2
                {
                    SetProcessDpiAwarenessContext((int)DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                }
                else
                {
                    SetProcessDpiAwareness(ProcessDpiAwareness.Process_Per_Monitor_DPI_Aware);
                }
            }
            else
            {
                SetProcessDPIAware();
            }
        }
    }
}