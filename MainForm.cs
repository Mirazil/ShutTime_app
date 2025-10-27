using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    public partial class MainForm : Form
    {
        private class OptionItem<T>
        {
            public OptionItem(string text, T value)
            {
                Text = text;
                Value = value;
            }

            public string Text { get; }
            public T Value { get; }
            public override string ToString() => Text;
        }

        private System.Windows.Forms.Timer timer;
        private TimeSpan remainingTime;
        private bool isRunning = false;
        private bool idleMode = false; // режим «при бездействии»

        public MainForm()
        {
            AppConfig.Load();
            InitializeComponent();
            InitTimer();
            ApplyLocalization();
            ApplyTheme();
            SetupUI();
        }

        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
        }

        private void ApplyLocalization()
        {
            Text = I18n.T("Title");
            tabPageTimer.Text = I18n.T("Tab_Timer");
            tabPageSettings.Text = I18n.T("Tab_Settings");
            labelTimerTitle.Text = I18n.T("Timer_Title");
            labelTimerSubtitle.Text = I18n.T("Timer_Subtitle");
            labelActionCaption.Text = I18n.T("Caption_Action");
            labelConditionCaption.Text = I18n.T("Caption_Condition");
            labelTimeCaption.Text = I18n.T("Caption_Time");
            labelSettingsTitle.Text = I18n.T("Settings_Title_Highlight");
            labelSettingsSubtitle.Text = I18n.T("Settings_Subtitle");
            labelSettingsLanguage.Text = I18n.T("Language");
            labelSettingsTheme.Text = I18n.T("Theme");
            checkBoxAutostart.Text = I18n.T("RunOnStartup");
            buttonApplySettings.Text = I18n.T("Btn_OK");
            RefillActionItems();
            RefillConditionItems();
            PopulateSettingsLists();
            LoadSettingsValues();
        }

        private void RefillActionItems()
        {
            comboBoxAction.Items.Clear();
            comboBoxAction.Items.AddRange(new object[]
            {
                I18n.T("Action_Shutdown"),
                I18n.T("Action_Sleep"),
                I18n.T("Action_Restart"),
                I18n.T("Action_Lock")
            });
            if (comboBoxAction.Items.Count > 0) comboBoxAction.SelectedIndex = 0;
        }

        private void RefillConditionItems()
        {
            comboBoxCondition.Items.Clear();
            comboBoxCondition.Items.AddRange(new object[]
            {
                I18n.T("Cond_After"),
                I18n.T("Cond_At"),
                I18n.T("Cond_Idle")
            });
            if (comboBoxCondition.Items.Count > 0) comboBoxCondition.SelectedIndex = 0;
        }

        private void ApplyTheme() => ThemeHelper.Apply(this);

        private void SetupUI()
        {
            labelCountdown.Text = "00:00:00";
            maskedTextBoxTime.Text = "004500"; // 00:45:00
            buttonStart.Text = "▶";
        }

        private void PopulateSettingsLists()
        {
            var selectedLanguage = comboBoxLanguage.SelectedItem as OptionItem<AppLanguage>;
            var selectedTheme = comboBoxTheme.SelectedItem as OptionItem<AppTheme>;

            comboBoxLanguage.Items.Clear();
            comboBoxLanguage.Items.AddRange(new object[]
            {
                new OptionItem<AppLanguage>("Русский", AppLanguage.Russian),
                new OptionItem<AppLanguage>("English", AppLanguage.English),
                new OptionItem<AppLanguage>("Українська", AppLanguage.Ukrainian),
            });

            comboBoxTheme.Items.Clear();
            comboBoxTheme.Items.AddRange(new object[]
            {
                new OptionItem<AppTheme>(I18n.T("Theme_Light"), AppTheme.Light),
                new OptionItem<AppTheme>(I18n.T("Theme_Dark"), AppTheme.Dark),
                new OptionItem<AppTheme>(I18n.T("Theme_System"), AppTheme.System),
            });

            if (selectedLanguage != null)
            {
                for (int i = 0; i < comboBoxLanguage.Items.Count; i++)
                {
                    if (((OptionItem<AppLanguage>)comboBoxLanguage.Items[i]).Value.Equals(selectedLanguage.Value))
                    {
                        comboBoxLanguage.SelectedIndex = i;
                        break;
                    }
                }
            }

            if (selectedTheme != null)
            {
                for (int i = 0; i < comboBoxTheme.Items.Count; i++)
                {
                    if (((OptionItem<AppTheme>)comboBoxTheme.Items[i]).Value.Equals(selectedTheme.Value))
                    {
                        comboBoxTheme.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void LoadSettingsValues()
        {
            for (int i = 0; i < comboBoxLanguage.Items.Count; i++)
            {
                if (((OptionItem<AppLanguage>)comboBoxLanguage.Items[i]).Value == AppConfig.Current.Language)
                {
                    comboBoxLanguage.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < comboBoxTheme.Items.Count; i++)
            {
                if (((OptionItem<AppTheme>)comboBoxTheme.Items[i]).Value == AppConfig.Current.Theme)
                {
                    comboBoxTheme.SelectedIndex = i;
                    break;
                }
            }

            checkBoxAutostart.Checked = AppConfig.Current.RunOnStartup || AutoStartHelper.IsEnabled();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (idleMode)
            {
                var idle = GetIdleTime();
                var threshold = ParseTime(maskedTextBoxTime.Text);
                var left = threshold - idle;
                if (left.TotalSeconds > 0)
                {
                    labelCountdown.Text = left.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    StopTimer();
                    ExecuteAction();
                }
                return;
            }

            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                labelCountdown.Text = remainingTime.ToString(@"hh\:mm\:ss");
            }
            else
            {
                StopTimer();
                ExecuteAction();
            }
        }

        private void StopTimer()
        {
            timer.Stop();
            isRunning = false;
            buttonStart.Text = "▶";
        }

        private void ExecuteAction()
        {
            switch (comboBoxAction.SelectedIndex)
            {
                case 0: // Shutdown
                    Process.Start("shutdown", "/s /t 0");
                    break;
                case 1: // Sleep
                    Application.SetSuspendState(PowerState.Suspend, true, true);
                    break;
                case 2: // Restart
                    Process.Start("shutdown", "/r /t 0");
                    break;
                case 3: // Lock
                    LockWorkStation();
                    break;
            }
        }

        [DllImport("user32.dll")] public static extern bool LockWorkStation();

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static TimeSpan GetIdleTime()
        {
            LASTINPUTINFO lii = new() { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };
            GetLastInputInfo(ref lii);
            uint idleMs = (uint)Environment.TickCount - lii.dwTime;
            return TimeSpan.FromMilliseconds(idleMs);
        }

        private static TimeSpan ParseTime(string maskedText)
        {
            // maskedText like "hhmmss" or "hh:mm:ss" — разберём оба случая
            var t = maskedText.Replace(":", "");
            if (t.Length != 6) t = "000000";
            int hh = int.Parse(t.Substring(0, 2));
            int mm = int.Parse(t.Substring(2, 2));
            int ss = int.Parse(t.Substring(4, 2));
            return new TimeSpan(hh, mm, ss);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                idleMode = comboBoxCondition.SelectedIndex == 2;
                if (idleMode)
                {
                    // таймер просто будет отслеживать бездействие
                    buttonStart.Text = "⏸";
                    isRunning = true;
                    timer.Start();
                    return;
                }

                var input = ParseTime(maskedTextBoxTime.Text);

                if (comboBoxCondition.SelectedIndex == 0) // «Через заданное время»
                {
                    remainingTime = input;
                }
                else // «В заданное время»
                {
                    var now = DateTime.Now;
                    var target = new DateTime(now.Year, now.Month, now.Day,
                                              input.Hours, input.Minutes, input.Seconds);
                    if (target <= now) target = target.AddDays(1);
                    remainingTime = target - now;
                }

                labelCountdown.Text = remainingTime.ToString(@"hh\:mm\:ss");
                timer.Start();
                isRunning = true;
                buttonStart.Text = "⏸";
            }
            else
            {
                StopTimer();
            }
        }

        private void buttonApplySettings_Click(object sender, EventArgs e)
        {
            if (comboBoxLanguage.SelectedItem is OptionItem<AppLanguage> language)
            {
                AppConfig.Current.Language = language.Value;
            }

            if (comboBoxTheme.SelectedItem is OptionItem<AppTheme> theme)
            {
                AppConfig.Current.Theme = theme.Value;
            }

            AppConfig.Current.RunOnStartup = checkBoxAutostart.Checked;
            AutoStartHelper.Set(checkBoxAutostart.Checked);
            AppConfig.Save();

            ApplyLocalization();
            ApplyTheme();
        }

        private void comboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // маска остаётся HH:MM:SS. Меняется только смысл ввода.
            // Для удобства сбросим поле на 00:45:00
            if (!isRunning)
            {
                maskedTextBoxTime.Text = "004500";
            }
        }
    }
}
