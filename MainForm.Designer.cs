using System.Drawing;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControlMain;
        private TabPage tabPageTimer;
        private TabPage tabPageSettings;
        private Label labelCountdown;
        private ComboBox comboBoxAction;
        private ComboBox comboBoxCondition;
        private MaskedTextBox maskedTextBoxTime;
        private Button buttonStart;
        private Label labelTimeCaption;
        private Label labelSettingsLanguage;
        private Label labelSettingsTheme;
        private ComboBox comboBoxLanguage;
        private ComboBox comboBoxTheme;
        private CheckBox checkBoxAutostart;
        private Button buttonApplySettings;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            tabControlMain = new TabControl();
            tabPageTimer = new TabPage();
            tabPageSettings = new TabPage();
            labelCountdown = new Label();
            comboBoxAction = new ComboBox();
            comboBoxCondition = new ComboBox();
            maskedTextBoxTime = new MaskedTextBox();
            buttonStart = new Button();
            labelTimeCaption = new Label();
            labelSettingsLanguage = new Label();
            labelSettingsTheme = new Label();
            comboBoxLanguage = new ComboBox();
            comboBoxTheme = new ComboBox();
            checkBoxAutostart = new CheckBox();
            buttonApplySettings = new Button();

            tabControlMain.SuspendLayout();
            tabPageTimer.SuspendLayout();
            tabPageSettings.SuspendLayout();
            SuspendLayout();

            // tabControlMain
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Controls.Add(tabPageTimer);
            tabControlMain.Controls.Add(tabPageSettings);

            // tabPageTimer
            tabPageTimer.Padding = new Padding(10);
            tabPageTimer.UseVisualStyleBackColor = true;

            // labelCountdown
            labelCountdown.Font = new Font("Consolas", 28F, FontStyle.Bold);
            labelCountdown.Text = "00:00:00";
            labelCountdown.TextAlign = ContentAlignment.MiddleCenter;
            labelCountdown.Dock = DockStyle.Top;
            labelCountdown.Height = 90;

            // comboBoxAction
            comboBoxAction.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAction.Font = new Font("Segoe UI", 10F);
            comboBoxAction.Top = 110; comboBoxAction.Left = 20;
            comboBoxAction.Width = 260;

            // comboBoxCondition
            comboBoxCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCondition.Font = new Font("Segoe UI", 10F);
            comboBoxCondition.Top = 150; comboBoxCondition.Left = 20;
            comboBoxCondition.Width = 260;
            comboBoxCondition.SelectedIndexChanged += comboBoxCondition_SelectedIndexChanged;

            // labelTimeCaption
            labelTimeCaption.AutoSize = true;
            labelTimeCaption.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            labelTimeCaption.Top = 190; labelTimeCaption.Left = 20;

            // maskedTextBoxTime
            maskedTextBoxTime.Font = new Font("Segoe UI", 10F);
            maskedTextBoxTime.Mask = "00:00:00";
            maskedTextBoxTime.Text = "004500";
            maskedTextBoxTime.Top = 210; maskedTextBoxTime.Left = 20;
            maskedTextBoxTime.Width = 110;

            // buttonStart
            buttonStart.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            buttonStart.Text = "▶";
            buttonStart.Top = 250; buttonStart.Left = 110;
            buttonStart.Width = 90; buttonStart.Height = 80;
            buttonStart.Click += buttonStart_Click;

            tabPageTimer.Controls.Add(buttonStart);
            tabPageTimer.Controls.Add(maskedTextBoxTime);
            tabPageTimer.Controls.Add(labelTimeCaption);
            tabPageTimer.Controls.Add(comboBoxCondition);
            tabPageTimer.Controls.Add(comboBoxAction);
            tabPageTimer.Controls.Add(labelCountdown);

            // tabPageSettings
            tabPageSettings.Padding = new Padding(10);
            tabPageSettings.UseVisualStyleBackColor = true;

            // labelSettingsLanguage
            labelSettingsLanguage.AutoSize = true;
            labelSettingsLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSettingsLanguage.Top = 20; labelSettingsLanguage.Left = 20;

            // comboBoxLanguage
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.Font = new Font("Segoe UI", 10F);
            comboBoxLanguage.Top = 45; comboBoxLanguage.Left = 20;
            comboBoxLanguage.Width = 260;

            // labelSettingsTheme
            labelSettingsTheme.AutoSize = true;
            labelSettingsTheme.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelSettingsTheme.Top = 85; labelSettingsTheme.Left = 20;

            // comboBoxTheme
            comboBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTheme.Font = new Font("Segoe UI", 10F);
            comboBoxTheme.Top = 110; comboBoxTheme.Left = 20;
            comboBoxTheme.Width = 260;

            // checkBoxAutostart
            checkBoxAutostart.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            checkBoxAutostart.Top = 150; checkBoxAutostart.Left = 20;
            checkBoxAutostart.Width = 260;

            // buttonApplySettings
            buttonApplySettings.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            buttonApplySettings.Top = 190; buttonApplySettings.Left = 20;
            buttonApplySettings.Width = 120; buttonApplySettings.Height = 35;
            buttonApplySettings.Click += buttonApplySettings_Click;

            tabPageSettings.Controls.Add(buttonApplySettings);
            tabPageSettings.Controls.Add(checkBoxAutostart);
            tabPageSettings.Controls.Add(comboBoxTheme);
            tabPageSettings.Controls.Add(labelSettingsTheme);
            tabPageSettings.Controls.Add(comboBoxLanguage);
            tabPageSettings.Controls.Add(labelSettingsLanguage);

            // MainForm
            ClientSize = new Size(310, 340);
            Controls.Add(tabControlMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "ShutTime 1.0";
            StartPosition = FormStartPosition.CenterScreen;

            tabPageTimer.ResumeLayout(false);
            tabPageTimer.PerformLayout();
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            tabControlMain.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
