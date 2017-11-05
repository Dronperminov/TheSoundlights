using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TheSoundlights.Properties;

namespace TheSoundlights {
    public partial class SettingsForm : Form {
        int changedChannelsNumber;
        public SettingsForm() {
            InitializeComponent();

            spectrumCheckBox.Checked = Settings.Default.spectrum;
            topMostCheckBox.Checked = Settings.Default.topMost;

            backColorBox.Text = Settings.Default.backColor;
            foreColorBox.Text = Settings.Default.foreColor;
            colColorBox.Text = Settings.Default.colColor;
            statusBackColorBox.Text = Settings.Default.statusBackColor;
            statusForeColorBox.Text = Settings.Default.statusForeColor;

            changedChannelsNumber = Settings.Default.channelsNumber;
            channelsCombo.SelectedIndex = changedChannelsNumber == 3 ? 0 : 1;

            backColorBox.KeyPress += keyPress;
            foreColorBox.KeyPress += keyPress;
            colColorBox.KeyPress += keyPress;
            statusBackColorBox.KeyPress += keyPress;
            statusForeColorBox.KeyPress += keyPress;

            backColorBox.TextChanged += textChanged;
            foreColorBox.TextChanged += textChanged;
            colColorBox.TextChanged += textChanged;
            statusBackColorBox.TextChanged += textChanged;
            statusForeColorBox.TextChanged += textChanged;

            TopMost = true;

            foreach (Control control in Controls)
                control.TabStop = false;
        }

        private void textChanged(object sender, EventArgs e) {
            TextBox tb = ((TextBox)sender);

            if (tb.Text.IndexOf("#") == -1) {
                tb.SuspendLayout();
                int start = tb.SelectionStart;
                tb.Text = "#" + tb.Text;
                tb.SelectionStart = start + 1;
                tb.ResumeLayout();
            }
        }

        private void keyPress(object sender, KeyPressEventArgs e) {
            e.KeyChar = char.ToLower(e.KeyChar);

            if (!(char.IsNumber(e.KeyChar) || (e.KeyChar >= 'a' && e.KeyChar <= 'f') || e.KeyChar == 8 || (e.KeyChar == '#' && ((TextBox)sender).Text.IndexOf('#') == -1)))
                e.Handled = true;
        }

        private bool isColor(string htmlColor) {
            return htmlColor.Length == 4 || htmlColor.Length == 7;
        }

        string colorFromArray(string c, string[] file, string searched) {
            int index = -1;
            searched = searched.ToLower();

            for (int i = file.Length - 1; i >= 0; i--) {
                string f = file[i].ToLower();

                if (f.IndexOf(searched) == 0) {
                    index = i;
                    i = -1;
                }
            }

            if (index != -1) {
                MatchCollection matches = new Regex("[ ]*[=][ ]*[#][0-9,a-f]{6}[;][ ]*").Matches(file[index]);

                if (matches.Count != 0)
                    c = new Regex("[#][0-9,a-f]{6}").Match(matches[0].Value).Value;
                else {
                    matches = new Regex("[ ]*[=][ ]*[#][0-9,a-f]{3}[;][ ]*").Matches(file[index]);

                    if (matches.Count != 0)
                        c = new Regex("[#][0-9,a-f]{3}").Match(matches[0].Value).Value;
                }
            }

            return c;
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            Settings.Default.topMost = topMostCheckBox.Checked;
            Settings.Default.spectrum = spectrumCheckBox.Checked;

            string errors = "";

            if (!isColor(backColorBox.Text))
                errors += "\nцвет фона";

            if (!isColor(foreColorBox.Text))
                errors += "\nцвет текста";

            if (!isColor(colColorBox.Text))
                errors += "\nцвет границ";

            if (!isColor(statusBackColorBox.Text))
                errors += "\nцвет фона статусной строки";

            if (!isColor(statusForeColorBox.Text))
                errors += "\nцвет текста статусной строки";

            if (errors != "")
                MessageForm.Show(MessageForm.ping_lol, "Обнаружены ошибки в следующих цветах (некорректное число символов):" + errors + "\n\nЦвет задаётся в формате html: #xxxxxx или #xxx (x - шестнадцатиричное число)", "Обраружены ошибки");
            else {
                Settings.Default.backColor = backColorBox.Text;
                Settings.Default.foreColor = foreColorBox.Text;
                Settings.Default.colColor = colColorBox.Text;

                Settings.Default.statusBackColor = statusBackColorBox.Text;
                Settings.Default.statusForeColor = statusForeColorBox.Text;

                Settings.Default.channelsNumber = changedChannelsNumber;

                Settings.Default.Save();

                Close();
            }
        }

        private void changeThemeBtn_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SA theme files (*.SoundLightsTheme)|*.SoundLightsTheme";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK) {
                string[] file = File.ReadAllLines(ofd.FileName);

                backColorBox.Text = colorFromArray(Settings.Default.backColor, file, "backColor");
                foreColorBox.Text = colorFromArray(Settings.Default.foreColor, file, "foreColor");
                colColorBox.Text = colorFromArray(Settings.Default.colColor, file, "colColor");
                statusBackColorBox.Text = colorFromArray(Settings.Default.statusBackColor, file, "statusBackColor");
                statusForeColorBox.Text = colorFromArray(Settings.Default.statusForeColor, file, "statusForeColor");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            changedChannelsNumber = Convert.ToInt32(channelsCombo.SelectedItem);
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
