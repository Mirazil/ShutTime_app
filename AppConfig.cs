using System;
using System.IO;
using System.Text.Json;

namespace ShutdownTimerApp
{
    public enum AppLanguage { Russian, English, Ukrainian }
    public enum AppTheme { Light, Dark, System }

    public class AppSettings
    {
        public AppLanguage Language { get; set; } = AppLanguage.Russian;
        public AppTheme Theme { get; set; } = AppTheme.System;
        public bool RunOnStartup { get; set; } = false;
    }

    public static class AppConfig
    {
        private static readonly string Dir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShutTime");
        private static readonly string FilePath = Path.Combine(Dir, "config.json");

        public static AppSettings Current { get; private set; } = new();

        public static void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var s = JsonSerializer.Deserialize<AppSettings>(json);
                    if (s != null) Current = s;
                }
            }
            catch { /* ignore */ }
        }

        public static void Save()
        {
            try
            {
                if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
                var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch { /* ignore */ }
        }
    }
}
