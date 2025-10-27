using System.Drawing;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblLanguage;
        private Label lblTheme;
        private ComboBox cmbLanguage;
        private ComboBox cmbTheme;
        private CheckBox chkAutostart;
        private Button btnOK;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblLanguage = new Label();
            lblTheme = new Label();
            cmbLanguage = new ComboBox();
            cmbTheme = new ComboBox();
            chkAutostart = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // form
            ClientSize = new Size(360, 240);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            // labels
            lblLanguage.AutoSize = true;
            lblLanguage.Left = 20; lblLanguage.Top = 20; lblLanguage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            lblTheme.AutoSize = true;
            lblTheme.Left = 20; lblTheme.Top = 90; lblTheme.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // combos
            cmbLanguage.Left = 20; cmbLanguage.Top = 50; cmbLanguage.Width = 220;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.Left = 20; cmbTheme.Top = 120; cmbTheme.Width = 220;
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;

            // checkbox
            chkAutostart.Left = 20; chkAutostart.Top = 160; chkAutostart.Width = 260;

            // buttons
            btnOK.Left = 200; btnOK.Top = 190; btnOK.Width = 60;
            btnOK.Click += btnOK_Click;

            btnCancel.Left = 270; btnCancel.Top = 190; btnCancel.Width = 60;
            btnCancel.Click += btnCancel_Click;

            Controls.Add(lblLanguage);
            Controls.Add(lblTheme);
            Controls.Add(cmbLanguage);
            Controls.Add(cmbTheme);
            Controls.Add(chkAutostart);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);

            ResumeLayout(false);
        }
    }
}
