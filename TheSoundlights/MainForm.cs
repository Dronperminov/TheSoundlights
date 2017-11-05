using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using TheSoundlights.Properties;

namespace TheSoundlights {
    public partial class MainForm : Form {
        const string connectText = "Подключиться";
        const string disconnectText = "Отключиться";

        int[,] tbValues = new int[2, LightShow.numBands] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        int prevIndex = 0;

        MyBluetooth myBt = null;
        LightShow lightShow = null;
        ToolTip tip = new ToolTip();

        public static PrivateFontCollection fontCollection = new PrivateFontCollection();

        public MainForm() {
            InitializeComponent();

            connectBtn.Text = connectText;

            fontLoading();

            Font = new Font(fontCollection.Families[0], 8.25f);

            coloringComponents();

            tip.SetToolTip(barsCheckBox, "Передвигать ползунки одновременно на уровень передвигаемого");
            tip.SetToolTip(fadeCheckBox, "Включение плавной смены цвета после 5 секунд отсутствия звука");
            tip.SetToolTip(lightCheckBox, "Переключение между режимами цветомузыки и постоянного свечения");

            tip.SetToolTip(scanBtn, "Поиск доступных устройств TheSoundLights");
            tip.SetToolTip(deviceCombo, "Список доступных устройств TheSoundLights для подключения");

            myBt = new MyBluetooth(connectBtn, connectBtn_Click);

            if (myBt.getRadio() == null) {
                MessageForm.Show(MessageForm.ping_sad, "На Вашем устройстве отключен или отсутствует Bluetooth!", "Проверьте состояние Bluetooth");

                Environment.Exit(0);
            } else
                lightShow = new LightShow(myBt, spectrumChart, trackInit(), fadeCheckBox, barsCheckBox, lightCheckBox);
        }

        private void coloringComponents() {
            Color backColor = ColorTranslator.FromHtml(Settings.Default.backColor);
            Color foreColor = ColorTranslator.FromHtml(Settings.Default.foreColor);
            Color colColor = ColorTranslator.FromHtml(Settings.Default.colColor);
            Color statusBackColor = ColorTranslator.FromHtml(Settings.Default.statusBackColor);
            Color statusForeColor = ColorTranslator.FromHtml(Settings.Default.statusForeColor);

            BackColor = backColor;
            ForeColor = foreColor;

            status.BackColor = statusBackColor;
            status.ForeColor = statusForeColor;

            deviceCombo.FlatStyle = FlatStyle.Standard;
            deviceCombo.BackColor = backColor;
            deviceCombo.ForeColor = foreColor;

            spectrumChart.ChartAreas[0].BackColor = Color.Transparent;
            spectrumChart.BackColor = Color.Transparent;
            spectrumChart.BorderlineColor = foreColor;
            spectrumChart.Series["FFT"].Color = colColor;

            buttonInitialize(backColor, foreColor, colColor, scanBtn, connectBtn);
            checkBoxColoring(foreColor, barsCheckBox, fadeCheckBox, lightCheckBox);
        }

        private void scanBtn_Click(object sender, EventArgs e) {
            new Thread(() => {
                status.Invoke(new Action(() => status.Items[0].Text = "Поиск доступных устройств"));

                // делаем недоступными кнопку сканирования и список устройств
                enablingComponents(false, scanBtn, connectBtn);
                // ищем BlueTooth устройства и настраиваем доступность кнопки подключения
                enablingComponents(myBt.scanDevs(deviceCombo) > 0, connectBtn);
                // возвращаем доступность кнопке сканирования и списку устройств
                enablingComponents(true, scanBtn, deviceCombo);

                status.Invoke(new Action(() => status.Items[0].Text = ""));
            }).Start();
        }

        private void deviceCombo_SelectedIndexChanged(object sender, EventArgs e) {
            myBt.serialPort.PortName = myBt.getPorts()[deviceCombo.SelectedIndex].portName;

            if (prevIndex != deviceCombo.SelectedIndex) {
                for (int i = 0; i < LightShow.numBands; i++)
                    lightShow.bars[i].Value = tbValues[0, i] = tbValues[1, i] = 0;

                barsCheckBox.Checked = false;
                fadeCheckBox.Checked = true;
                lightCheckBox.Checked = false;
            }

            prevIndex = deviceCombo.SelectedIndex;
        }

        private void connectBtn_Click(object sender, EventArgs e) {
            new Thread(() => {
                switch (connectBtn.Text) {
                    // собираемся подключаться
                    case connectText:
                        status.Invoke(new Action(() => status.Items[0].Text = "Подключение к " + deviceCombo.Items[deviceCombo.SelectedIndex]));

                        enablingComponents(false, deviceCombo, connectBtn, scanBtn);

                        // пытаемся открыть порт
                        if (myBt.open()) {
                            // меняем текст кнопки на "Отключиться"
                            connectBtn.Invoke(new Action(() => connectBtn.Text = disconnectText));

                            // возвращаем кнопке подключения доступность
                            enablingComponents(true, connectBtn);

                            lightShow.Start();
                        } else
                            enablingComponents(true, deviceCombo, connectBtn, scanBtn);

                        status.Invoke(new Action(() => status.Items[0].Text = ""));
                        break;

                    // собираемся отключаться
                    case disconnectText:
                        // меняем текст кнопки на "Подключиться"
                        connectBtn.Invoke(new Action(() => connectBtn.Text = connectText));

                        // возвращаем кнопке подключения доступность 
                        enablingComponents(true, deviceCombo, scanBtn);

                        // останавливаем обработку цветомузыки
                        lightShow.Stop();
                        // закрываем соединение с устройством
                        myBt.close();
                        break;
                }

                // TODO: другой поток!
                ActiveControl = spectrumChart;
            }).Start();
        }

        private void Form1_Load(object sender, EventArgs e) {
            scanBtn_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                DialogResult res = MessageForm.Show(MessageForm.ping_awful, "Вы действительно хотите закрыть приложение?", "Кажется, Вы нажали на крестик...", "Да", "Нет");

                if (res == DialogResult.Yes)
                    Environment.Exit(1);
                else
                    e.Cancel = true;
            } catch (Exception) {
                MessageBox.Show("ooh");
                Environment.Exit(1);
            }
        }

        private void barValueChanged(object sender, EventArgs e) {
            // если трекбар в фокусе и передигаем вместе, то значение передвигаемого присваиваем всем остальным
            if (barsCheckBox.Checked && ((TrackBar)sender).Focused)
                for (int i = 0; i < LightShow.numBands; i++)
                    lightShow.bars[i].Value = ((TrackBar)sender).Value;

            // в режиме постоянного цвета изменяем состояния выходных каналов цветомузыки
            if (lightCheckBox.Checked)
                for (int i = 0; i < LightShow.numBands; i++)
                    lightShow.outStates[i] = (lightShow.bars[i].Value + 5) * 25.5f;

            // определяем, в каком состоянии находится программа
            int state = lightCheckBox.Checked ? 1 : 0;

            // запоминаем текущие значения трекбаров в соответствующем режиме
            for (int i = 0; i < LightShow.numBands; i++)
                if (lightShow.bars[i] == (TrackBar)sender)
                    tbValues[state, i] = ((TrackBar)sender).Value;
        }

        private void lightCheckBox_CheckedChanged(object sender, EventArgs e) {
            for (int i = 0; i < LightShow.numBands; i++) {
                lightShow.bars[i].Value = tbValues[lightCheckBox.Checked ? 1 : 0, i];

                if (lightCheckBox.Checked)
                    lightShow.outStates[i] = (lightShow.bars[i].Value + 5) * 25.5f;
            }                    
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;

            if (e.KeyData == Keys.S) {
                int channelsNumber = Settings.Default.channelsNumber;

                new SettingsForm().ShowDialog();

                TopMost = Settings.Default.topMost;

                coloringComponents();

                if (channelsNumber != Settings.Default.channelsNumber) {
                    for (int i = 0; i < 4; i++)
                        tbValues[0, i] = tbValues[1, i] = 0;

                    if (connectBtn.Text == disconnectText)
                        lightShow.Stop();

                    TrackBar[] bars = trackInit();

                    lightShow = new LightShow(myBt, spectrumChart, bars, fadeCheckBox, barsCheckBox, lightCheckBox);

                    if (connectBtn.Text == disconnectText) 
                        lightShow.Start();
                }

                e.Handled = true;
            }
        }
    }
}
