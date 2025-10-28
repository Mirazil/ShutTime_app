using System;
using System.Diagnostics;
using System.Drawing;
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

        private const string PlaySymbol = "\u25B6";
        private const string PauseSymbol = "\u23F8";
        private const string DefaultTimerValue = "004500";

        private readonly System.Windows.Forms.Timer timer;
        private TimeSpan remainingTime;
        private bool isRunning = false;
        private bool idleMode = false; // режим «при бездействии»
        private bool exitRequested = false;

        public MainForm()
        {
            AppConfig.Load();
            InitializeComponent();

            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;

            ApplyLocalization();
            ApplyTheme();
            SetupUI();

            var appIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (appIcon != null)
            {
                Icon = appIcon;
            }

            var icon = Icon ?? appIcon ?? SystemIcons.Application;
            notifyIcon.Icon = icon;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AdjustWindowSize();
            if (ShouldStartMinimized())
            {
                HideToTray();
            }
            else
            {
                UpdateNotifyIconVisibility();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AdjustWindowSize();
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
            checkBoxRunMinimized.Text = I18n.T("RunMinimized");
            checkBoxMinimizeOnClose.Text = I18n.T("MinimizeOnClose");
            buttonApplySettings.Text = I18n.T("Btn_OK");
            UpdateTrayLocalization();
            RefillActionItems();
            RefillConditionItems();
            PopulateSettingsLists();
            LoadSettingsValues();
            AdjustWindowSize();
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
            maskedTextBoxTime.Text = DefaultTimerValue; // 00:45:00
            buttonStart.Text = PlaySymbol;
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
                new OptionItem<AppTheme>(I18n.T("Theme_Forest"), AppTheme.Forest),
                new OptionItem<AppTheme>(I18n.T("Theme_Sunset"), AppTheme.Sunset),
                new OptionItem<AppTheme>(I18n.T("Theme_Ocean"), AppTheme.Ocean),
            });

            if (selectedLanguage != null)
            {
                for (int i = 0; i < comboBoxLanguage.Items.Count; i++)
                {
                    if (comboBoxLanguage.Items[i] is OptionItem<AppLanguage> item &&
                        item.Value.Equals(selectedLanguage.Value))
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
                    if (comboBoxTheme.Items[i] is OptionItem<AppTheme> item &&
                        item.Value.Equals(selectedTheme.Value))
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
                if (comboBoxLanguage.Items[i] is OptionItem<AppLanguage> item &&
                    item.Value == AppConfig.Current.Language)
                {
                    comboBoxLanguage.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < comboBoxTheme.Items.Count; i++)
            {
                if (comboBoxTheme.Items[i] is OptionItem<AppTheme> item &&
                    item.Value == AppConfig.Current.Theme)
                {
                    comboBoxTheme.SelectedIndex = i;
                    break;
                }
            }

            checkBoxAutostart.Checked = AppConfig.Current.RunOnStartup || AutoStartHelper.IsEnabled();
            checkBoxMinimizeOnClose.Checked = AppConfig.Current.MinimizeOnClose;
            checkBoxRunMinimized.Checked = AppConfig.Current.RunMinimized;
            UpdateRunMinimizedCheckboxState();
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
            idleMode = false;
            buttonStart.Text = PlaySymbol;
        }

        private void ExecuteAction()
        {
            try
            {
                switch (comboBoxAction.SelectedIndex)
                {
                    case 0:
                        StartShutdownCommand("/s /t 0");
                        break;
                    case 1:
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                        break;
                    case 2:
                        StartShutdownCommand("/r /t 0");
                        break;
                    case 3:
                        LockWorkStation();
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void StartShutdownCommand(string arguments)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false
            });
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
            if (!GetLastInputInfo(ref lii))
            {
                return TimeSpan.Zero;
            }

            uint tickCount = unchecked((uint)Environment.TickCount);
            uint idleMs = unchecked(tickCount - lii.dwTime);
            return TimeSpan.FromMilliseconds(idleMs);
        }

        private static TimeSpan ParseTime(string maskedText)
        {
            // Extract digits from masked input; pad missing positions with zeros.
            Span<char> digits = stackalloc char[6];
            int length = 0;

            foreach (char ch in maskedText)
            {
                if (char.IsDigit(ch) && length < digits.Length)
                {
                    digits[length++] = ch;
                }
            }

            for (; length < digits.Length; length++)
            {
                digits[length] = '0';
            }

            int hours = ParseComponent(digits, 0);
            int minutes = ParseComponent(digits, 2);
            int seconds = ParseComponent(digits, 4);

            hours = Math.Clamp(hours, 0, 23);
            minutes = Math.Clamp(minutes, 0, 59);
            seconds = Math.Clamp(seconds, 0, 59);

            return new TimeSpan(hours, minutes, seconds);

            static int ParseComponent(ReadOnlySpan<char> source, int start) =>
                int.TryParse(source.Slice(start, 2), out var value) ? value : 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                idleMode = comboBoxCondition.SelectedIndex == 2;
                if (idleMode)
                {
                    // таймер просто будет отслеживать бездействие
                    buttonStart.Text = PauseSymbol;
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
                buttonStart.Text = PauseSymbol;
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
            AppConfig.Current.RunMinimized = checkBoxAutostart.Checked && checkBoxRunMinimized.Checked;
            AppConfig.Current.MinimizeOnClose = checkBoxMinimizeOnClose.Checked;
            AutoStartHelper.Set(checkBoxAutostart.Checked);
            AppConfig.Save();

            ApplyLocalization();
            ApplyTheme();
            UpdateNotifyIconVisibility();
            AdjustWindowSize();
        }

        private void checkBoxAutostart_CheckedChanged(object? sender, EventArgs e)
        {
            UpdateRunMinimizedCheckboxState();
        }

        private void UpdateRunMinimizedCheckboxState()
        {
            bool enabled = checkBoxAutostart.Checked;
            checkBoxRunMinimized.Enabled = enabled;
            if (!enabled)
            {
                checkBoxRunMinimized.Checked = false;
            }
        }

        private void comboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // маска остаётся HH:MM:SS. Меняется только смысл ввода.
            // Для удобства сбросим поле на 00:45:00
            if (!isRunning)
            {
                maskedTextBoxTime.Text = DefaultTimerValue;
            }
        }

        private void notifyIcon_DoubleClick(object? sender, EventArgs e) => RestoreFromTray();

        private void trayMenuShow_Click(object? sender, EventArgs e) => RestoreFromTray();

        private void trayMenuExit_Click(object? sender, EventArgs e)
        {
            if (!ConfirmExitPrompt()) return;

            exitRequested = true;
            notifyIcon.Visible = false;
            Close();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!exitRequested && e.CloseReason == CloseReason.UserClosing && AppConfig.Current.MinimizeOnClose)
            {
                e.Cancel = true;
                HideToTray();
                return;
            }

            if (!exitRequested && ShouldConfirmExit(e.CloseReason) && !ConfirmExitPrompt())
            {
                e.Cancel = true;
                return;
            }

            notifyIcon.Visible = false;
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            timer.Dispose();
            notifyIcon.Dispose();
        }

        private void HideToTray()
        {
            notifyIcon.Visible = true;
            ShowInTaskbar = false;
            Hide();
            UpdateNotifyIconVisibility();
        }

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Activate();
            exitRequested = false;
            UpdateNotifyIconVisibility();
        }

        private bool ConfirmExitPrompt()
        {
            if (!isRunning) return true;

            var result = MessageBox.Show(
                I18n.T("ConfirmExitWithTimer_Message"),
                I18n.T("ConfirmExitWithTimer_Title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            return result == DialogResult.Yes;
        }

        private bool ShouldConfirmExit(CloseReason reason)
        {
            if (!isRunning) return false;

            return reason switch
            {
                CloseReason.WindowsShutDown => false,
                CloseReason.TaskManagerClosing => false,
                _ => true,
            };
        }

        private void UpdateTrayLocalization()
        {
            trayMenuShow.Text = I18n.T("Tray_Show");
            trayMenuExit.Text = I18n.T("Tray_Exit");
            notifyIcon.Text = I18n.T("Title");
        }

        private void UpdateNotifyIconVisibility()
        {
            notifyIcon.Visible = AppConfig.Current.MinimizeOnClose || !Visible;
        }

        private bool ShouldStartMinimized()
        {
            if (!AppConfig.Current.RunMinimized)
            {
                return false;
            }

            bool autostartEnabled = AppConfig.Current.RunOnStartup || AutoStartHelper.IsEnabled();
            return autostartEnabled;
        }

        private void tabControlMain_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AdjustWindowSize();
        }

        private void AdjustWindowSize()
        {
            var page = tabControlMain.SelectedTab;
            if (page == null) return;

            int chrome = Height - ClientSize.Height;
            int headerHeight = tabControlMain.DisplayRectangle.Top;
            int contentHeight = MeasureTabPageContentHeight(page);
            int desiredHeight = headerHeight + contentHeight + chrome + Padding.Vertical;
            if (desiredHeight <= 0) return;

            if (Height != desiredHeight)
            {
                Height = desiredHeight;
            }

            MinimumSize = new Size(Width, desiredHeight);
        }

        private int MeasureTabPageContentHeight(TabPage page)
        {
            int availableWidth = page.ClientSize.Width;
            if (availableWidth <= 0)
            {
                availableWidth = tabControlMain.ClientSize.Width;
            }

            int maxChildHeight = 0;
            foreach (Control child in page.Controls)
            {
                if (!child.Visible) continue;

                int widthForChild = Math.Max(availableWidth - child.Margin.Horizontal, 0);
                Size preferred = child.GetPreferredSize(new Size(widthForChild, 0));
                int childHeight = preferred.Height > 0 ? preferred.Height : child.Height;
                childHeight += child.Margin.Vertical;
                maxChildHeight = Math.Max(maxChildHeight, childHeight);
            }

            return page.Padding.Vertical + maxChildHeight;
        }
    }
}
