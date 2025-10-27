using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    public partial class MainForm : Form
    {
        private Timer timer;
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
            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
        }

        private void ApplyLocalization()
        {
            Text = I18n.T("Title");
            labelTimeCaption.Text = I18n.T("Caption_Time");
            RefillActionItems();
            RefillConditionItems();
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
        }

        private void Timer_Tick(object sender, EventArgs e)
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

        private void settingsButton_Click(object sender, EventArgs e)
        {
            using var dlg = new SettingsForm();
            var res = dlg.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                // применяем новые настройки
                ApplyLocalization();
                ApplyTheme();
            }
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
