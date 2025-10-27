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
        private TableLayoutPanel layoutRoot;
        private Panel panelCard;
        private TableLayoutPanel cardLayout;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel flowButtons;

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
            layoutRoot = new TableLayoutPanel();
            panelCard = new Panel();
            cardLayout = new TableLayoutPanel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            flowButtons = new FlowLayoutPanel();

            layoutRoot.SuspendLayout();
            panelCard.SuspendLayout();
            cardLayout.SuspendLayout();
            SuspendLayout();

            // layoutRoot
            layoutRoot.AutoScroll = true;
            layoutRoot.ColumnCount = 1;
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRoot.Controls.Add(panelCard, 0, 0);
            layoutRoot.Dock = DockStyle.Fill;
            layoutRoot.RowCount = 2;
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRoot.Tag = "transparent";

            // panelCard
            panelCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCard.AutoSize = true;
            panelCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelCard.Controls.Add(cardLayout);
            panelCard.Margin = new Padding(0);
            panelCard.MaximumSize = new Size(0, 0);
            panelCard.Tag = "transparent";

            // cardLayout
            cardLayout.AutoSize = true;
            cardLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cardLayout.ColumnCount = 1;
            cardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cardLayout.Controls.Add(lblTitle, 0, 0);
            cardLayout.Controls.Add(lblSubtitle, 0, 1);
            cardLayout.Controls.Add(lblLanguage, 0, 2);
            cardLayout.Controls.Add(cmbLanguage, 0, 3);
            cardLayout.Controls.Add(lblTheme, 0, 4);
            cardLayout.Controls.Add(cmbTheme, 0, 5);
            cardLayout.Controls.Add(chkAutostart, 0, 6);
            cardLayout.Controls.Add(flowButtons, 0, 7);
            cardLayout.Dock = DockStyle.Fill;
            cardLayout.Padding = new Padding(8);
            cardLayout.RowCount = 8;
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());
            cardLayout.RowStyles.Add(new RowStyle());

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.Margin = new Padding(0, 0, 0, 2);

            // lblSubtitle
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtitle.Margin = new Padding(0, 0, 0, 10);
            lblSubtitle.Tag = "muted";

            // lblLanguage
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblLanguage.Margin = new Padding(0, 0, 0, 4);

            // cmbLanguage
            cmbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbLanguage.IntegralHeight = false;
            cmbLanguage.Margin = new Padding(0, 0, 0, 8);

            // lblTheme
            lblTheme.AutoSize = true;
            lblTheme.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblTheme.Margin = new Padding(0, 8, 0, 4);

            // cmbTheme
            cmbTheme.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbTheme.IntegralHeight = false;
            cmbTheme.Margin = new Padding(0, 0, 0, 8);

            // chkAutostart
            chkAutostart.AutoSize = true;
            chkAutostart.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkAutostart.Margin = new Padding(0, 10, 0, 0);

            // flowButtons
            flowButtons.AutoSize = true;
            flowButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Margin = new Padding(0, 12, 0, 0);
            flowButtons.Padding = new Padding(0);
            flowButtons.WrapContents = false;
            flowButtons.Controls.Add(btnOK);
            flowButtons.Controls.Add(btnCancel);

            // btnOK
            btnOK.AutoSize = true;
            btnOK.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnOK.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnOK.Margin = new Padding(0);
            btnOK.MinimumSize = new Size(0, 40);
            btnOK.Tag = "accent";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;

            // btnCancel
            btnCancel.AutoSize = true;
            btnCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.Margin = new Padding(8, 0, 0, 0);
            btnCancel.MinimumSize = new Size(0, 40);
            btnCancel.Tag = "ghost";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            // SettingsForm
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(420, 360);
            Controls.Add(layoutRoot);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            cardLayout.ResumeLayout(false);
            cardLayout.PerformLayout();
            ResumeLayout(false);
        }
    }
}
