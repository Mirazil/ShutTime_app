using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    public partial class SettingsForm : Form
    {
        private class OptionItem<T>
        {
            public string Text { get; set; } = "";
            public T Value { get; set; } = default!;
            public override string ToString() => Text;
        }

        public SettingsForm()
        {
            InitializeComponent();
            ApplyLocalization();
            ThemeHelper.Apply(this);
            LoadValues();
        }

        private void ApplyLocalization()
        {
            Text = I18n.T("SettingsTitle");
            lblTitle.Text = I18n.T("Settings_Title_Highlight");
            lblSubtitle.Text = I18n.T("Settings_Subtitle");
            lblLanguage.Text = I18n.T("Language");
            lblTheme.Text = I18n.T("Theme");
            chkAutostart.Text = I18n.T("RunOnStartup");
            btnOK.Text = I18n.T("Btn_OK");
            btnCancel.Text = I18n.T("Btn_Cancel");

            cmbLanguage.DataSource = new List<OptionItem<AppLanguage>> {
                new(){ Text = "Русский", Value = AppLanguage.Russian },
                new(){ Text = "English", Value = AppLanguage.English },
                new(){ Text = "Українська", Value = AppLanguage.Ukrainian },
            };

            cmbTheme.DataSource = new List<OptionItem<AppTheme>> {
                new(){ Text = I18n.T("Theme_Light"), Value = AppTheme.Light },
                new(){ Text = I18n.T("Theme_Dark"),  Value = AppTheme.Dark  },
                new(){ Text = I18n.T("Theme_System"),Value = AppTheme.System},
            };
        }

        private void LoadValues()
        {
            // язык
            for (int i = 0; i < cmbLanguage.Items.Count; i++)
                if (((OptionItem<AppLanguage>)cmbLanguage.Items[i]).Value == AppConfig.Current.Language)
                    cmbLanguage.SelectedIndex = i;

            // тема
            for (int i = 0; i < cmbTheme.Items.Count; i++)
                if (((OptionItem<AppTheme>)cmbTheme.Items[i]).Value == AppConfig.Current.Theme)
                    cmbTheme.SelectedIndex = i;

            chkAutostart.Checked = AppConfig.Current.RunOnStartup || AutoStartHelper.IsEnabled();
        }

        private void btnOK_Click(object? sender, EventArgs e)
        {
            if (cmbLanguage.SelectedItem is OptionItem<AppLanguage> selectedLanguage)
            {
                AppConfig.Current.Language = selectedLanguage.Value;
            }

            if (cmbTheme.SelectedItem is OptionItem<AppTheme> selectedTheme)
            {
                AppConfig.Current.Theme = selectedTheme.Value;
            }
            AppConfig.Current.RunOnStartup = chkAutostart.Checked;

            AutoStartHelper.Set(chkAutostart.Checked);
            AppConfig.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
