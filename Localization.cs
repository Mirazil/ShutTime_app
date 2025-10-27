using System.Collections.Generic;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    public static class I18n
    {
        private static readonly Dictionary<AppLanguage, Dictionary<string, string>> _d
            = new Dictionary<AppLanguage, Dictionary<string, string>>
        {
            [AppLanguage.Russian] = new()
            {
                ["Title"] = "ShutTime 1.0",
                ["SettingsTitle"] = "Настройки",
                ["Language"] = "Язык",
                ["Theme"] = "Тема",
                ["Theme_Light"] = "Светлая",
                ["Theme_Dark"] = "Тёмная",
                ["Theme_System"] = "Как в системе",
                ["RunOnStartup"] = "Запускать с Windows",

                ["Action_Shutdown"] = "Выключить компьютер",
                ["Action_Sleep"] = "Спящий режим",
                ["Action_Restart"] = "Перезагрузка",
                ["Action_Lock"] = "Заблокировать пользователя",

                ["Cond_After"] = "Через заданное время",
                ["Cond_At"] = "В заданное время",
                ["Cond_Idle"] = "При бездействии пользователя",

                ["Caption_Time"] = "Время",
                ["Btn_OK"] = "ОК",
                ["Btn_Cancel"] = "Отмена",
            },
            [AppLanguage.English] = new()
            {
                ["Title"] = "ShutTime 1.0",
                ["SettingsTitle"] = "Settings",
                ["Language"] = "Language",
                ["Theme"] = "Theme",
                ["Theme_Light"] = "Light",
                ["Theme_Dark"] = "Dark",
                ["Theme_System"] = "System",
                ["RunOnStartup"] = "Run at Windows startup",

                ["Action_Shutdown"] = "Shut down",
                ["Action_Sleep"] = "Sleep",
                ["Action_Restart"] = "Restart",
                ["Action_Lock"] = "Lock user",

                ["Cond_After"] = "After delay",
                ["Cond_At"] = "At time",
                ["Cond_Idle"] = "On user idle",

                ["Caption_Time"] = "Time",
                ["Btn_OK"] = "OK",
                ["Btn_Cancel"] = "Cancel",
            },
            [AppLanguage.Ukrainian] = new()
            {
                ["Title"] = "ShutTime 1.0",
                ["SettingsTitle"] = "Налаштування",
                ["Language"] = "Мова",
                ["Theme"] = "Тема",
                ["Theme_Light"] = "Світла",
                ["Theme_Dark"] = "Темна",
                ["Theme_System"] = "Як у системі",
                ["RunOnStartup"] = "Запускати з Windows",

                ["Action_Shutdown"] = "Вимкнути комп'ютер",
                ["Action_Sleep"] = "Режим сну",
                ["Action_Restart"] = "Перезавантаження",
                ["Action_Lock"] = "Заблокувати користувача",

                ["Cond_After"] = "Через заданий час",
                ["Cond_At"] = "У заданий час",
                ["Cond_Idle"] = "За бездіяльності",

                ["Caption_Time"] = "Час",
                ["Btn_OK"] = "ОК",
                ["Btn_Cancel"] = "Скасувати",
            },
        };

        public static AppLanguage CurrentLanguage => AppConfig.Current.Language;

        public static string T(string key) =>
            _d.TryGetValue(CurrentLanguage, out var m) && m.TryGetValue(key, out var v) ? v : key;
    }

    public static class ThemeHelper
    {
        public static bool IsLightTheme()
        {
            var theme = AppConfig.Current.Theme;
            if (theme == AppTheme.Light) return true;
            if (theme == AppTheme.Dark) return false;

            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                var val = key?.GetValue("AppsUseLightTheme");
                return val is int i ? i == 1 : true;
            }
            catch { return true; }
        }

        public static void Apply(Form form)
        {
            bool light = IsLightTheme();
            Color back = light ? Color.WhiteSmoke : Color.FromArgb(28, 28, 28);
            Color panel = light ? Color.White : Color.FromArgb(40, 40, 40);
            Color fore = light ? Color.Black : Color.White;

            form.BackColor = back;
            form.ForeColor = fore;
            ApplyTo(form.Controls, panel, fore);
        }

        private static void ApplyTo(Control.ControlCollection controls, Color back, Color fore)
        {
            foreach (Control c in controls)
            {
                switch (c)
                {
                    case Button b:
                        b.FlatStyle = FlatStyle.Flat;
                        b.BackColor = back;
                        b.ForeColor = fore;
                        break;
                    case ComboBox cb:
                        cb.BackColor = back; cb.ForeColor = fore; break;
                    case MaskedTextBox mtb:
                        mtb.BackColor = back; mtb.ForeColor = fore; break;
                    case TextBox tb:
                        tb.BackColor = back; tb.ForeColor = fore; break;
                    case CheckBox ch:
                        ch.BackColor = back; ch.ForeColor = fore; break;
                    case Label lb:
                        lb.BackColor = back; lb.ForeColor = fore; break;
                }
                if (c.HasChildren) ApplyTo(c.Controls, back, fore);
            }
        }
    }
}
