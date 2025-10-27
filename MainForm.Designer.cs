using System.Drawing;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelCountdown;
        private ComboBox comboBoxAction;
        private ComboBox comboBoxCondition;
        private MaskedTextBox maskedTextBoxTime;
        private Button buttonStart;
        private Button settingsButton;
        private Button closeButton;
        private Label labelTimeCaption;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            labelCountdown = new Label();
            comboBoxAction = new ComboBox();
            comboBoxCondition = new ComboBox();
            maskedTextBoxTime = new MaskedTextBox();
            buttonStart = new Button();
            settingsButton = new Button();
            closeButton = new Button();
            labelTimeCaption = new Label();

            SuspendLayout();

            // labelCountdown
            labelCountdown.Font = new Font("Consolas", 28F, FontStyle.Bold);
            labelCountdown.Text = "00:00:00";
            labelCountdown.TextAlign = ContentAlignment.MiddleCenter;
            labelCountdown.Dock = DockStyle.Top;
            labelCountdown.Height = 90;

            // comboBoxAction
            comboBoxAction.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAction.Font = new Font("Segoe UI", 10F);
            comboBoxAction.Top = 100; comboBoxAction.Left = 20;
            comboBoxAction.Width = 260;

            // comboBoxCondition
            comboBoxCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCondition.Font = new Font("Segoe UI", 10F);
            comboBoxCondition.Top = 140; comboBoxCondition.Left = 20;
            comboBoxCondition.Width = 260;
            comboBoxCondition.SelectedIndexChanged += comboBoxCondition_SelectedIndexChanged;

            // labelTimeCaption
            labelTimeCaption.AutoSize = true;
            labelTimeCaption.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            labelTimeCaption.Top = 180; labelTimeCaption.Left = 20;

            // maskedTextBoxTime
            maskedTextBoxTime.Font = new Font("Segoe UI", 10F);
            maskedTextBoxTime.Mask = "00:00:00";
            maskedTextBoxTime.Text = "004500";
            maskedTextBoxTime.Top = 200; maskedTextBoxTime.Left = 20;
            maskedTextBoxTime.Width = 110;

            // buttonStart
            buttonStart.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            buttonStart.Text = "▶";
            buttonStart.Top = 240; buttonStart.Left = 100;
            buttonStart.Width = 90; buttonStart.Height = 80;
            buttonStart.Click += buttonStart_Click;

            // settingsButton
            settingsButton.Text = "⚙";
            settingsButton.Font = new Font("Segoe UI", 12F);
            settingsButton.Top = 10; settingsButton.Left = 220;
            settingsButton.Width = 30; settingsButton.Height = 30;
            settingsButton.Click += settingsButton_Click;

            // closeButton
            closeButton.Text = "✖";
            closeButton.Font = new Font("Segoe UI", 12F);
            closeButton.Top = 10; closeButton.Left = 260;
            closeButton.Width = 30; closeButton.Height = 30;
            closeButton.Click += (s, e) => Close();

            // MainForm
            ClientSize = new Size(310, 340);
            Controls.Add(labelCountdown);
            Controls.Add(comboBoxAction);
            Controls.Add(comboBoxCondition);
            Controls.Add(labelTimeCaption);
            Controls.Add(maskedTextBoxTime);
            Controls.Add(buttonStart);
            Controls.Add(settingsButton);
            Controls.Add(closeButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "ShutTime 1.0";
            StartPosition = FormStartPosition.CenterScreen;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
