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
                ["Theme_Forest"] = "Лесная",
                ["Theme_Sunset"] = "Закат",
                ["Theme_Ocean"] = "Океан",
                ["RunOnStartup"] = "Запускать с Windows",
                ["RunMinimized"] = "Запускать свёрнутым",
                ["MinimizeOnClose"] = "Сворачивать при закрытии",

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
                ["Btn_Stop"] = "Стоп",
                ["Btn_Cancel"] = "Отмена",
                ["Tab_Timer"] = "Таймер",
                ["Tab_Settings"] = "Настройки",
                ["Tray_Show"] = "Приложение",
                ["Tray_Exit"] = "Выйти",
                ["ConfirmExitWithTimer_Title"] = "Закрытие приложения",
                ["ConfirmExitWithTimer_Message"] = "Таймер сейчас активен. Вы уверены, что хотите закрыть приложение?",
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
                ["Theme_Forest"] = "Forest",
                ["Theme_Sunset"] = "Sunset",
                ["Theme_Ocean"] = "Ocean",
                ["RunOnStartup"] = "Run at Windows startup",
                ["RunMinimized"] = "Start minimized",
                ["MinimizeOnClose"] = "Minimize to tray on close",

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
                ["Btn_Stop"] = "Stop",
                ["Btn_Cancel"] = "Cancel",
                ["Tab_Timer"] = "Timer",
                ["Tab_Settings"] = "Settings",
                ["Tray_Show"] = "Application",
                ["Tray_Exit"] = "Exit",
                ["ConfirmExitWithTimer_Title"] = "Exit application",
                ["ConfirmExitWithTimer_Message"] = "The timer is currently running. Are you sure you want to close the application?",
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
                ["Theme_Forest"] = "Лісова",
                ["Theme_Sunset"] = "Захід сонця",
                ["Theme_Ocean"] = "Океанська",
                ["RunOnStartup"] = "Запускати з Windows",
                ["RunMinimized"] = "Запускати згорнутим",
                ["MinimizeOnClose"] = "Згортати під час закриття",

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
                ["Btn_Stop"] = "Стоп",
                ["Btn_Cancel"] = "Скасувати",
                ["Tab_Timer"] = "Таймер",
                ["Tab_Settings"] = "Налаштування",
                ["Tray_Show"] = "Додаток",
                ["Tray_Exit"] = "Вийти",
                ["ConfirmExitWithTimer_Title"] = "Закриття застосунку",
                ["ConfirmExitWithTimer_Message"] = "Таймер зараз активний. Ви впевнені, що хочете закрити застосунок?",
            },
        };

        public static AppLanguage CurrentLanguage => AppConfig.Current.Language;

        public static string T(string key) =>
            _d.TryGetValue(CurrentLanguage, out var m) && m.TryGetValue(key, out var v) ? v : key;
    }

    public static class ThemeHelper
    {
        public static bool IsLightTheme() => GetPalette(AppConfig.Current.Theme).IsLight;

        public static void Apply(Form form)
        {
            var palette = GetPalette(AppConfig.Current.Theme);

            form.BackColor = palette.BackColor;
            form.ForeColor = palette.ForegroundColor;
            ApplyTo(form.Controls, palette);
        }

        private static void ApplyTo(Control.ControlCollection controls, ThemePalette palette)
        {
            foreach (Control c in controls)
            {
                switch (c)
                {
                    case Button b:
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.FlatAppearance.MouseOverBackColor = b.Tag as string == "accent" ? palette.AccentHoverColor : palette.MutedHoverColor;
                        b.FlatAppearance.MouseDownBackColor = b.Tag as string == "accent" ? palette.AccentHoverColor : palette.MutedPressedColor;
                        if (b.Tag as string == "accent")
                        {
                            b.BackColor = palette.AccentColor;
                            b.ForeColor = Color.White;
                        }
                        else if (b.Tag as string == "ghost")
                        {
                            b.BackColor = Color.Transparent;
                            b.ForeColor = palette.ForegroundColor;
                        }
                        else
                        {
                            b.BackColor = palette.SurfaceColor;
                            b.ForeColor = palette.ForegroundColor;
                        }
                        break;
                    case ComboBox cb:
                        cb.BackColor = palette.SurfaceColor; cb.ForeColor = palette.ForegroundColor; cb.FlatStyle = FlatStyle.Flat; break;
                    case MaskedTextBox mtb:
                        mtb.BackColor = palette.SurfaceColor; mtb.ForeColor = palette.ForegroundColor; mtb.BorderStyle = BorderStyle.FixedSingle; break;
                    case TextBox tb:
                        tb.BackColor = palette.SurfaceColor; tb.ForeColor = palette.ForegroundColor; tb.BorderStyle = BorderStyle.FixedSingle; break;
                    case CheckBox ch:
                        ch.BackColor = Color.Transparent; ch.ForeColor = palette.ForegroundColor; break;
                    case Label lb:
                        lb.BackColor = Color.Transparent;
                        lb.ForeColor = lb.Tag as string == "muted" ? palette.MutedTextColor : palette.ForegroundColor;
                        break;
                    case TabControl tc:
                        tc.BackColor = palette.BackColor; tc.ForeColor = palette.ForegroundColor; break;
                    case TabPage page:
                        page.BackColor = palette.BackColor; page.ForeColor = palette.ForegroundColor; break;
                    case Panel p:
                        if (p.Tag as string == "transparent")
                        {
                            p.BackColor = Color.Transparent;
                        }
                        else if (p.Tag as string == "accent")
                        {
                            p.BackColor = palette.AccentColor;
                        }
                        else
                        {
                            p.BackColor = palette.SurfaceColor;
                        }
                        p.ForeColor = palette.ForegroundColor;
                        break;
                }
                if (c.HasChildren) ApplyTo(c.Controls, palette);
            }
        }

        private static ThemePalette GetPalette(AppTheme theme) =>
            theme == AppTheme.System ? GetSystemPalette() : GetNonSystemPalette(theme);

        private static ThemePalette GetSystemPalette()
        {
            bool useLight = GetSystemLightPreference();
            return GetNonSystemPalette(useLight ? AppTheme.Light : AppTheme.Dark);
        }

        private static ThemePalette GetNonSystemPalette(AppTheme theme) => theme switch
        {
            AppTheme.Light => new ThemePalette(
                backColor: Color.FromArgb(245, 246, 250),
                surfaceColor: Color.White,
                foregroundColor: Color.FromArgb(23, 23, 26),
                accentColor: Color.FromArgb(76, 110, 245),
                accentHoverColor: Color.FromArgb(90, 124, 255),
                mutedHoverColor: Color.FromArgb(232, 234, 243),
                mutedPressedColor: Color.FromArgb(218, 221, 233),
                mutedTextColor: Color.FromArgb(120, 123, 134),
                isLight: true),
            AppTheme.Dark => new ThemePalette(
                backColor: Color.FromArgb(18, 18, 20),
                surfaceColor: Color.FromArgb(32, 32, 36),
                foregroundColor: Color.WhiteSmoke,
                accentColor: Color.FromArgb(98, 128, 255),
                accentHoverColor: Color.FromArgb(118, 144, 255),
                mutedHoverColor: Color.FromArgb(48, 48, 52),
                mutedPressedColor: Color.FromArgb(60, 60, 66),
                mutedTextColor: Color.FromArgb(160, 160, 170),
                isLight: false),
            AppTheme.Forest => new ThemePalette(
                backColor: Color.FromArgb(24, 36, 29),
                surfaceColor: Color.FromArgb(37, 53, 44),
                foregroundColor: Color.FromArgb(216, 234, 224),
                accentColor: Color.FromArgb(64, 160, 112),
                accentHoverColor: Color.FromArgb(82, 184, 134),
                mutedHoverColor: Color.FromArgb(48, 70, 58),
                mutedPressedColor: Color.FromArgb(59, 85, 70),
                mutedTextColor: Color.FromArgb(167, 193, 180),
                isLight: false),
            AppTheme.Sunset => new ThemePalette(
                backColor: Color.FromArgb(252, 244, 237),
                surfaceColor: Color.FromArgb(255, 250, 244),
                foregroundColor: Color.FromArgb(84, 52, 40),
                accentColor: Color.FromArgb(255, 140, 105),
                accentHoverColor: Color.FromArgb(255, 155, 120),
                mutedHoverColor: Color.FromArgb(245, 232, 222),
                mutedPressedColor: Color.FromArgb(236, 219, 207),
                mutedTextColor: Color.FromArgb(153, 116, 98),
                isLight: true),
            AppTheme.Ocean => new ThemePalette(
                backColor: Color.FromArgb(12, 26, 38),
                surfaceColor: Color.FromArgb(22, 42, 56),
                foregroundColor: Color.FromArgb(210, 231, 245),
                accentColor: Color.FromArgb(0, 168, 232),
                accentHoverColor: Color.FromArgb(26, 188, 252),
                mutedHoverColor: Color.FromArgb(32, 58, 74),
                mutedPressedColor: Color.FromArgb(41, 70, 88),
                mutedTextColor: Color.FromArgb(152, 180, 198),
                isLight: false),
            _ => new ThemePalette(
                backColor: SystemColors.Control,
                surfaceColor: SystemColors.ControlLightLight,
                foregroundColor: SystemColors.ControlText,
                accentColor: SystemColors.Highlight,
                accentHoverColor: SystemColors.Highlight,
                mutedHoverColor: SystemColors.ControlLight,
                mutedPressedColor: SystemColors.ControlDark,
                mutedTextColor: SystemColors.GrayText,
                isLight: true)
        };

        private static bool GetSystemLightPreference()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize");
                var val = key?.GetValue("AppsUseLightTheme");
                return val is int i ? i == 1 : true;
            }
            catch
            {
                return true;
            }
        }

        private readonly struct ThemePalette
        {
            public ThemePalette(Color backColor, Color surfaceColor, Color foregroundColor, Color accentColor,
                Color accentHoverColor, Color mutedHoverColor, Color mutedPressedColor, Color mutedTextColor, bool isLight)
            {
                BackColor = backColor;
                SurfaceColor = surfaceColor;
                ForegroundColor = foregroundColor;
                AccentColor = accentColor;
                AccentHoverColor = accentHoverColor;
                MutedHoverColor = mutedHoverColor;
                MutedPressedColor = mutedPressedColor;
                MutedTextColor = mutedTextColor;
                IsLight = isLight;
            }

            public Color BackColor { get; }
            public Color SurfaceColor { get; }
            public Color ForegroundColor { get; }
            public Color AccentColor { get; }
            public Color AccentHoverColor { get; }
            public Color MutedHoverColor { get; }
            public Color MutedPressedColor { get; }
            public Color MutedTextColor { get; }
            public bool IsLight { get; }
        }
    }
}
