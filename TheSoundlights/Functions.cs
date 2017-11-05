using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace TheSoundlights {
    public partial class MainForm : Form {
        // настройка доступности компонентов
        public void enablingComponents(bool enable, params Control[] controls) {
            for (int i = 0; i < controls.Length; i++)
                controls[i].Invoke(new Action(() => controls[i].Enabled = enable));
        }

        // настройка видимости компонентов
        static public void visiblingComponents(bool visible, params Control[] controls) {
            for (int i = 0; i < controls.Length; i++)
                controls[i].Invoke(new Action(() => controls[i].Visible = visible));
        }

        void fontLoading() {
            using (MemoryStream fontStream = new MemoryStream(Properties.Resources.HelveticaRegular)) {
                IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                Marshal.Copy(fontData, 0, data, (int)fontStream.Length);
                fontCollection.AddMemoryFont(data, (int)fontStream.Length);
                fontStream.Close();
                Marshal.FreeCoTaskMem(data);
            }
        }

        static string arrayToString(int[] array) {
            string s = "";
            for (int i = 0; i < array.Length; i++)
                s += i + ". " + array[i] + "\n";

            return s;
        }

        public TrackBar[] trackInit() {
            TrackBar[] bars = new TrackBar[4] { bar1, bar2, bar3, bar4 };

            for (int i = 0; i < 4; i++) {
                bars[i].ValueChanged -= barValueChanged;

                bars[i].Maximum = 5;
                bars[i].Minimum = -5;
                bars[i].Value = 0;
                bars[i].Visible = false;

                bars[i].ValueChanged += barValueChanged;
                bars[i].Scroll += barToolTipShow;
                bars[i].MouseDown += barToolTipShow;
                bars[i].MouseUp += barMouseUp;
            }

            if (Properties.Settings.Default.channelsNumber == 3) {
                bars[0].Location = new Point(19, bars[0].Location.Y);
                bars[1].Location = new Point(-47, bars[1].Location.Y);
                bars[2].Location = new Point(44, bars[2].Location.Y);
                bars[3].Location = new Point(69, bars[3].Location.Y);
            }
            else {
                bars[0].Location = new Point(7, bars[0].Location.Y);
                bars[1].Location = new Point(32, bars[1].Location.Y);
                bars[2].Location = new Point(57, bars[2].Location.Y);
                bars[3].Location = new Point(82, bars[3].Location.Y);
            }

            return bars;
        }

        private void buttonInitialize(Color backColor, Color foreColor, Color colColor, params MyButton[] btns) {
            foreach (MyButton btn in btns) {
                btn.FlatStyle = FlatStyle.Flat;
                btn.UseVisualStyleBackColor = true;

                btn.ForeColor = foreColor;
                btn.BackColor = btn.Enabled ? Color.Transparent : Color.Transparent;

                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = btn.Enabled ? colColor : ColorTranslator.FromHtml("#808080");

                btn.FlatAppearance.MouseOverBackColor = colColor;
                btn.FlatAppearance.MouseDownBackColor = colColor;

                btn.EnabledChanged += (object sender, EventArgs e) => {
                    btn.FlatAppearance.BorderColor = btn.Enabled ? colColor : ColorTranslator.FromHtml("#808080");
                    btn.BackColor = btn.Enabled ? Color.Transparent : Color.Transparent;
                };
            }
        }

        private void barMouseUp(object sender, EventArgs e) {
            if (((TrackBar)sender).Focused)
                tip.Hide((TrackBar)sender);
        }

        private void barToolTipShow(object sender, EventArgs e) {
            TrackBar bar = (TrackBar)sender;
            int y = bar.Height / 2 - 9;

            string value = (bar.Value > 0 ? "+" : "") + bar.Value;

            if (bar.Focused)
                tip.Show(value, bar, 23, (int)(y - bar.Value * 6.6));

            if (barsCheckBox.Checked)
                foreach (TrackBar b in lightShow.bars)
                    b.Value = bar.Value;
        }
        
        private void checkBoxColoring(Color foreColor, params MyCheckBox[] checkBoxs) {
            foreach (MyCheckBox ch in checkBoxs) {
                ch.FlatStyle = FlatStyle.Standard;

                ch.BackColor = Color.Transparent;
                ch.ForeColor = foreColor;
            }
        }
    }
}
