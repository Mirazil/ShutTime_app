using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ShutdownTimerApp
{
    public static class AutoStartHelper
    {
        private const string RunKeyPath = @"Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        private static string AppName => string.IsNullOrWhiteSpace(Application.ProductName)
            ? AppDomain.CurrentDomain.FriendlyName
            : Application.ProductName;

        public static bool IsEnabled()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
                if (key == null) return false;

                var stored = key.GetValue(AppName) as string;
                if (string.IsNullOrWhiteSpace(stored)) return false;

                var normalizedStored = NormalizePath(stored);
                var normalizedCurrent = NormalizePath(Application.ExecutablePath);
                return string.Equals(normalizedStored, normalizedCurrent, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static void Set(bool enabled)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true)
                    ?? Registry.CurrentUser.CreateSubKey(RunKeyPath);
                if (key == null) return;

                if (enabled)
                {
                    key.SetValue(AppName, $"\"{Application.ExecutablePath}\"");
                }
                else
                {
                    key.DeleteValue(AppName, throwOnMissingValue: false);
                }
            }
            catch
            {
                // ignore registry errors
            }
        }

        private static string NormalizePath(string value)
        {
            return value.Trim().Trim('"');
        }
    }
}
