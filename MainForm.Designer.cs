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
        private TableLayoutPanel layoutTimer;
        private Panel panelTimerCard;
        private TableLayoutPanel timerCardLayout;
        private Label labelTimerTitle;
        private Label labelTimerSubtitle;
        private Label labelCountdown;
        private Label labelActionCaption;
        private ComboBox comboBoxAction;
        private Label labelConditionCaption;
        private ComboBox comboBoxCondition;
        private Label labelTimeCaption;
        private MaskedTextBox maskedTextBoxTime;
        private Button buttonStart;
        private TableLayoutPanel layoutSettings;
        private Panel panelSettingsCard;
        private TableLayoutPanel settingsCardLayout;
        private Label labelSettingsTitle;
        private Label labelSettingsSubtitle;
        private Label labelSettingsLanguage;
        private ComboBox comboBoxLanguage;
        private Label labelSettingsTheme;
        private ComboBox comboBoxTheme;
        private FlowLayoutPanel flowLayoutAutostart;
        private CheckBox checkBoxAutostart;
        private CheckBox checkBoxRunMinimized;
        private CheckBox checkBoxMinimizeOnClose;
        private Button buttonApplySettings;
        private Label labelSettingsMadeBy;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem trayMenuShow;
        private ToolStripMenuItem trayMenuExit;

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
            layoutTimer = new TableLayoutPanel();
            panelTimerCard = new Panel();
            timerCardLayout = new TableLayoutPanel();
            labelTimerTitle = new Label();
            labelTimerSubtitle = new Label();
            labelCountdown = new Label();
            labelActionCaption = new Label();
            comboBoxAction = new ComboBox();
            labelConditionCaption = new Label();
            comboBoxCondition = new ComboBox();
            labelTimeCaption = new Label();
            maskedTextBoxTime = new MaskedTextBox();
            buttonStart = new Button();
            layoutSettings = new TableLayoutPanel();
            panelSettingsCard = new Panel();
            settingsCardLayout = new TableLayoutPanel();
            labelSettingsTitle = new Label();
            labelSettingsSubtitle = new Label();
            labelSettingsLanguage = new Label();
            comboBoxLanguage = new ComboBox();
            labelSettingsTheme = new Label();
            comboBoxTheme = new ComboBox();
            flowLayoutAutostart = new FlowLayoutPanel();
            checkBoxAutostart = new CheckBox();
            checkBoxRunMinimized = new CheckBox();
            checkBoxMinimizeOnClose = new CheckBox();
            buttonApplySettings = new Button();
            labelSettingsMadeBy = new Label();
            notifyIcon = new NotifyIcon(components);
            trayMenu = new ContextMenuStrip(components);
            trayMenuShow = new ToolStripMenuItem();
            trayMenuExit = new ToolStripMenuItem();

            tabControlMain.SuspendLayout();
            tabPageTimer.SuspendLayout();
            tabPageSettings.SuspendLayout();
            layoutTimer.SuspendLayout();
            panelTimerCard.SuspendLayout();
            timerCardLayout.SuspendLayout();
            layoutSettings.SuspendLayout();
            panelSettingsCard.SuspendLayout();
            settingsCardLayout.SuspendLayout();
            SuspendLayout();

            // tabControlMain
            tabControlMain.Controls.Add(tabPageTimer);
            tabControlMain.Controls.Add(tabPageSettings);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tabControlMain.ItemSize = new Size(80, 32);
            tabControlMain.Margin = new Padding(0);
            tabControlMain.Padding = new Point(0, 0);
            tabControlMain.SizeMode = TabSizeMode.Normal;
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;

            // tabPageTimer
            tabPageTimer.Controls.Add(layoutTimer);
            tabPageTimer.Location = new Point(4, 46);
            tabPageTimer.Margin = new Padding(0);
            tabPageTimer.Padding = new Padding(8);
            tabPageTimer.UseVisualStyleBackColor = false;

            // layoutTimer
            layoutTimer.AutoScroll = true;
            layoutTimer.ColumnCount = 1;
            layoutTimer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutTimer.Controls.Add(panelTimerCard, 0, 0);
            layoutTimer.Dock = DockStyle.Fill;
            layoutTimer.RowCount = 2;
            layoutTimer.RowStyles.Add(new RowStyle());
            layoutTimer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutTimer.Tag = "transparent";

            // panelTimerCard
            panelTimerCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTimerCard.AutoSize = true;
            panelTimerCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelTimerCard.Controls.Add(timerCardLayout);
            panelTimerCard.Margin = new Padding(0);
            panelTimerCard.MaximumSize = new Size(0, 0);
            panelTimerCard.Tag = "transparent";

            // timerCardLayout
            timerCardLayout.AutoSize = true;
            timerCardLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            timerCardLayout.ColumnCount = 1;
            timerCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            timerCardLayout.Controls.Add(labelTimerTitle, 0, 0);
            timerCardLayout.Controls.Add(labelTimerSubtitle, 0, 1);
            timerCardLayout.Controls.Add(labelCountdown, 0, 2);
            timerCardLayout.Controls.Add(labelActionCaption, 0, 3);
            timerCardLayout.Controls.Add(comboBoxAction, 0, 4);
            timerCardLayout.Controls.Add(labelConditionCaption, 0, 5);
            timerCardLayout.Controls.Add(comboBoxCondition, 0, 6);
            timerCardLayout.Controls.Add(labelTimeCaption, 0, 7);
            timerCardLayout.Controls.Add(maskedTextBoxTime, 0, 8);
            timerCardLayout.Controls.Add(buttonStart, 0, 9);
            timerCardLayout.Dock = DockStyle.Fill;
            timerCardLayout.Padding = new Padding(8);
            timerCardLayout.RowCount = 10;
            timerCardLayout.Tag = "transparent";
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());
            timerCardLayout.RowStyles.Add(new RowStyle());

            // labelTimerTitle
            labelTimerTitle.AutoSize = true;
            labelTimerTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTimerTitle.Margin = new Padding(0, 0, 0, 2);

            // labelTimerSubtitle
            labelTimerSubtitle.AutoSize = true;
            labelTimerSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelTimerSubtitle.Margin = new Padding(0, 0, 0, 6);
            labelTimerSubtitle.Tag = "muted";

            // labelCountdown
            labelCountdown.Dock = DockStyle.Fill;
            labelCountdown.Font = new Font("Segoe UI Semibold", 42F, FontStyle.Bold, GraphicsUnit.Point);
            labelCountdown.Margin = new Padding(0, 4, 0, 8);
            labelCountdown.MinimumSize = new Size(0, 120);
            labelCountdown.Text = "00:00:00";
            labelCountdown.TextAlign = ContentAlignment.MiddleCenter;

            // labelActionCaption
            labelActionCaption.AutoSize = true;
            labelActionCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelActionCaption.Margin = new Padding(0, 0, 0, 4);

            // comboBoxAction
            comboBoxAction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxAction.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxAction.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAction.IntegralHeight = false;
            comboBoxAction.Margin = new Padding(0, 0, 0, 6);

            // labelConditionCaption
            labelConditionCaption.AutoSize = true;
            labelConditionCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelConditionCaption.Margin = new Padding(0, 0, 0, 4);

            // comboBoxCondition
            comboBoxCondition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxCondition.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCondition.IntegralHeight = false;
            comboBoxCondition.Margin = new Padding(0, 0, 0, 6);
            comboBoxCondition.SelectedIndexChanged += comboBoxCondition_SelectedIndexChanged;

            // labelTimeCaption
            labelTimeCaption.AutoSize = true;
            labelTimeCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelTimeCaption.Margin = new Padding(0, 4, 0, 4);

            // maskedTextBoxTime
            maskedTextBoxTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            maskedTextBoxTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxTime.Mask = "00:00:00";
            maskedTextBoxTime.Margin = new Padding(0, 0, 0, 8);
            maskedTextBoxTime.Text = "004500";
            maskedTextBoxTime.TextAlign = HorizontalAlignment.Center;

            // buttonStart
            buttonStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonStart.Font = new Font("Segoe UI Symbol", 20F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStart.Margin = new Padding(0, 6, 0, 0);
            buttonStart.MinimumSize = new Size(0, 44);
            buttonStart.Tag = "accent";
            buttonStart.Text = "";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;

            // tabPageSettings
            tabPageSettings.Controls.Add(layoutSettings);
            tabPageSettings.Location = new Point(4, 46);
            tabPageSettings.Margin = new Padding(0);
            tabPageSettings.Padding = new Padding(8);
            tabPageSettings.UseVisualStyleBackColor = false;

            // layoutSettings
            layoutSettings.AutoScroll = true;
            layoutSettings.ColumnCount = 1;
            layoutSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSettings.Controls.Add(panelSettingsCard, 0, 0);
            layoutSettings.Dock = DockStyle.Fill;
            layoutSettings.RowCount = 2;
            layoutSettings.RowStyles.Add(new RowStyle());
            layoutSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSettings.Tag = "transparent";

            // panelSettingsCard
            panelSettingsCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSettingsCard.AutoSize = true;
            panelSettingsCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelSettingsCard.Controls.Add(settingsCardLayout);
            panelSettingsCard.Margin = new Padding(0);
            panelSettingsCard.MaximumSize = new Size(0, 0);
            panelSettingsCard.Tag = "transparent";

            // settingsCardLayout
            settingsCardLayout.AutoSize = true;
            settingsCardLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            settingsCardLayout.ColumnCount = 1;
            settingsCardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            settingsCardLayout.Controls.Add(labelSettingsTitle, 0, 0);
            settingsCardLayout.Controls.Add(labelSettingsSubtitle, 0, 1);
            settingsCardLayout.Controls.Add(labelSettingsLanguage, 0, 2);
            settingsCardLayout.Controls.Add(comboBoxLanguage, 0, 3);
            settingsCardLayout.Controls.Add(labelSettingsTheme, 0, 4);
            settingsCardLayout.Controls.Add(comboBoxTheme, 0, 5);
            settingsCardLayout.Controls.Add(flowLayoutAutostart, 0, 6);
            settingsCardLayout.Controls.Add(checkBoxMinimizeOnClose, 0, 7);
            settingsCardLayout.Controls.Add(buttonApplySettings, 0, 8);
            settingsCardLayout.Controls.Add(labelSettingsMadeBy, 0, 9);
            settingsCardLayout.Dock = DockStyle.Fill;
            settingsCardLayout.Padding = new Padding(8);
            settingsCardLayout.RowCount = 10;
            settingsCardLayout.Tag = "transparent";
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());
            settingsCardLayout.RowStyles.Add(new RowStyle());

            // labelSettingsTitle
            labelSettingsTitle.AutoSize = true;
            labelSettingsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelSettingsTitle.Margin = new Padding(0, 0, 0, 2);

            // labelSettingsSubtitle
            labelSettingsSubtitle.AutoSize = true;
            labelSettingsSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelSettingsSubtitle.Margin = new Padding(0, 0, 0, 10);
            labelSettingsSubtitle.Tag = "muted";

            // labelSettingsLanguage
            labelSettingsLanguage.AutoSize = true;
            labelSettingsLanguage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelSettingsLanguage.Margin = new Padding(0, 4, 0, 4);

            // comboBoxLanguage
            comboBoxLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.IntegralHeight = false;
            comboBoxLanguage.Margin = new Padding(0, 0, 0, 8);

            // labelSettingsTheme
            labelSettingsTheme.AutoSize = true;
            labelSettingsTheme.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelSettingsTheme.Margin = new Padding(0, 8, 0, 4);

            // comboBoxTheme
            comboBoxTheme.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxTheme.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTheme.IntegralHeight = false;
            comboBoxTheme.Margin = new Padding(0, 0, 0, 8);

            // flowLayoutAutostart
            flowLayoutAutostart.AutoSize = true;
            flowLayoutAutostart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutAutostart.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutAutostart.Margin = new Padding(0, 8, 0, 0);
            flowLayoutAutostart.Tag = "transparent";
            flowLayoutAutostart.WrapContents = false;
            flowLayoutAutostart.Controls.Add(checkBoxAutostart);
            flowLayoutAutostart.Controls.Add(checkBoxRunMinimized);

            // checkBoxAutostart
            checkBoxAutostart.AutoSize = true;
            checkBoxAutostart.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxAutostart.Margin = new Padding(0, 0, 12, 0);
            checkBoxAutostart.CheckedChanged += checkBoxAutostart_CheckedChanged;

            // checkBoxRunMinimized
            checkBoxRunMinimized.AutoSize = true;
            checkBoxRunMinimized.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxRunMinimized.Margin = new Padding(0);

            // checkBoxMinimizeOnClose
            checkBoxMinimizeOnClose.AutoSize = true;
            checkBoxMinimizeOnClose.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxMinimizeOnClose.Margin = new Padding(0, 6, 0, 0);

            // buttonApplySettings
            buttonApplySettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonApplySettings.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonApplySettings.Margin = new Padding(0, 12, 0, 0);
            buttonApplySettings.MinimumSize = new Size(0, 40);
            buttonApplySettings.Tag = "accent";
            buttonApplySettings.UseVisualStyleBackColor = false;
            buttonApplySettings.Click += buttonApplySettings_Click;

            // labelSettingsMadeBy
            labelSettingsMadeBy.AutoSize = true;
            labelSettingsMadeBy.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            labelSettingsMadeBy.Margin = new Padding(0, 12, 0, 0);
            labelSettingsMadeBy.Tag = "muted";
            labelSettingsMadeBy.Text = "Made by ksoChibi";

            // trayMenu
            trayMenu.ImageScalingSize = new Size(24, 24);
            trayMenu.Items.AddRange(new ToolStripItem[] { trayMenuShow, trayMenuExit });
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(153, 68);

            // trayMenuShow
            trayMenuShow.Name = "trayMenuShow";
            trayMenuShow.Size = new Size(152, 32);
            trayMenuShow.Text = "Application";
            trayMenuShow.Click += trayMenuShow_Click;

            // trayMenuExit
            trayMenuExit.Name = "trayMenuExit";
            trayMenuExit.Size = new Size(152, 32);
            trayMenuExit.Text = "Exit";
            trayMenuExit.Click += trayMenuExit_Click;

            // notifyIcon
            notifyIcon.ContextMenuStrip = trayMenu;
            notifyIcon.Text = "ShutTime 1.0";
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            // MainForm
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(480, 560);
            Controls.Add(tabControlMain);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(480, 0);
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShutTime 1.0";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;

            tabPageTimer.ResumeLayout(false);
            layoutTimer.ResumeLayout(false);
            panelTimerCard.ResumeLayout(false);
            timerCardLayout.ResumeLayout(false);
            timerCardLayout.PerformLayout();
            tabPageSettings.ResumeLayout(false);
            layoutSettings.ResumeLayout(false);
            panelSettingsCard.ResumeLayout(false);
            settingsCardLayout.ResumeLayout(false);
            settingsCardLayout.PerformLayout();
            tabControlMain.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
