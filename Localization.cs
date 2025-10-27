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
                ["Caption_Action"] = "Действие",
                ["Caption_Condition"] = "Условие",
                ["Timer_Title"] = "Интеллектуальный таймер",
                ["Timer_Subtitle"] = "Задайте действие и условие выполнения",
                ["Settings_Title_Highlight"] = "Персонализация",
                ["Settings_Subtitle"] = "Настройте язык, тему и автозапуск",
                ["Btn_OK"] = "ОК",
                ["Btn_Cancel"] = "Отмена",
                ["Tab_Timer"] = "Таймер",
                ["Tab_Settings"] = "Настройки",
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
                ["Caption_Action"] = "Action",
                ["Caption_Condition"] = "Condition",
                ["Timer_Title"] = "Intelligent timer",
                ["Timer_Subtitle"] = "Define what happens and when",
                ["Settings_Title_Highlight"] = "Personalization",
                ["Settings_Subtitle"] = "Adjust language, theme, and startup",
                ["Btn_OK"] = "OK",
                ["Btn_Cancel"] = "Cancel",
                ["Tab_Timer"] = "Timer",
                ["Tab_Settings"] = "Settings",
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
                ["Caption_Action"] = "Дія",
                ["Caption_Condition"] = "Умова",
                ["Timer_Title"] = "Розумний таймер",
                ["Timer_Subtitle"] = "Задайте дію та момент виконання",
                ["Settings_Title_Highlight"] = "Персоналізація",
                ["Settings_Subtitle"] = "Налаштуйте мову, тему та автозапуск",
                ["Btn_OK"] = "ОК",
                ["Btn_Cancel"] = "Скасувати",
                ["Tab_Timer"] = "Таймер",
                ["Tab_Settings"] = "Налаштування",
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
            Color back = light ? Color.FromArgb(245, 246, 250) : Color.FromArgb(18, 18, 20);
            Color surface = light ? Color.White : Color.FromArgb(32, 32, 36);
            Color fore = light ? Color.FromArgb(23, 23, 26) : Color.WhiteSmoke;
            Color accent = light ? Color.FromArgb(76, 110, 245) : Color.FromArgb(98, 128, 255);
            Color accentHover = light ? Color.FromArgb(90, 124, 255) : Color.FromArgb(118, 144, 255);
            Color mutedHover = light ? Color.FromArgb(232, 234, 243) : Color.FromArgb(48, 48, 52);
            Color mutedPressed = light ? Color.FromArgb(218, 221, 233) : Color.FromArgb(60, 60, 66);

            form.BackColor = back;
            form.ForeColor = fore;
            ApplyTo(form.Controls, back, surface, fore, accent, accentHover, mutedHover, mutedPressed, light);
        }

        private static void ApplyTo(Control.ControlCollection controls, Color back, Color surface, Color fore,
            Color accent, Color accentHover, Color mutedHover, Color mutedPressed, bool light)
        {
            foreach (Control c in controls)
            {
                switch (c)
                {
                    case Button b:
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.FlatAppearance.MouseOverBackColor = b.Tag as string == "accent" ? accentHover : mutedHover;
                        b.FlatAppearance.MouseDownBackColor = b.Tag as string == "accent" ? accentHover : mutedPressed;
                        if (b.Tag as string == "accent")
                        {
                            b.BackColor = accent;
                            b.ForeColor = Color.White;
                        }
                        else if (b.Tag as string == "ghost")
                        {
                            b.BackColor = Color.Transparent;
                            b.ForeColor = fore;
                        }
                        else
                        {
                            b.BackColor = surface;
                            b.ForeColor = fore;
                        }
                        break;
                    case ComboBox cb:
                        cb.BackColor = surface; cb.ForeColor = fore; cb.FlatStyle = FlatStyle.Flat; break;
                    case MaskedTextBox mtb:
                        mtb.BackColor = surface; mtb.ForeColor = fore; mtb.BorderStyle = BorderStyle.FixedSingle; break;
                    case TextBox tb:
                        tb.BackColor = surface; tb.ForeColor = fore; tb.BorderStyle = BorderStyle.FixedSingle; break;
                    case CheckBox ch:
                        ch.BackColor = Color.Transparent; ch.ForeColor = fore; break;
                    case Label lb:
                        lb.BackColor = Color.Transparent;
                        lb.ForeColor = lb.Tag as string == "muted"
                            ? (light ? Color.FromArgb(120, 123, 134) : Color.FromArgb(160, 160, 170))
                            : fore;
                        break;
                    case TabControl tc:
                        tc.BackColor = back; tc.ForeColor = fore; break;
                    case TabPage page:
                        page.BackColor = back; page.ForeColor = fore; break;
                    case Panel p:
                        if (p.Tag as string == "transparent")
                        {
                            p.BackColor = Color.Transparent;
                        }
                        else if (p.Tag as string == "accent")
                        {
                            p.BackColor = accent;
                        }
                        else
                        {
                            p.BackColor = surface;
                        }
                        p.ForeColor = fore;
                        break;
                }
                if (c.HasChildren) ApplyTo(c.Controls, back, surface, fore, accent, accentHover, mutedHover, mutedPressed, light);
            }
        }
    }
}
