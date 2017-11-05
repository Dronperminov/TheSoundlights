using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Management;

namespace TheSoundlights {
    public partial class MainForm : Form {
        class MyBluetooth {
            public struct MyPort {
                public string devName;
                public string portName;
                public string address;
            }

            List<MyPort> myPorts;
            List<MyPort> tmpPorts;
            public SerialPort serialPort;

            public MyBluetooth(Button connectBtn, EventHandler disconnect) {
                // пересоздаём списки портов
                tmpPorts = new List<MyPort>(0);
                myPorts = new List<MyPort>(0);

                // настраиваем последовательный порт
                serialPort = new SerialPort();
                serialPort.BaudRate = 9600;

                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;

                serialPort.ReadTimeout = 200;
                serialPort.WriteTimeout = 200;

                serialPort.Disposed += (object sender, EventArgs e) => {
                    if (connectBtn.Text == disconnectText) {
                        MessageForm.Show(MessageForm.ping_lol, "Попробуйте подключиться ещё раз", "Потеряна связь с устройством.");

                        disconnect(sender, e);
                    }
                };
            }

            public BluetoothRadio getRadio() {
                return BluetoothRadio.PrimaryRadio;
            }

            public List<MyPort> getPorts() {
                return myPorts;
            }

            public void findPorts() {
                tmpPorts.Clear();

                ManagementObjectCollection moc = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity").Get();

                foreach (ManagementObject service in moc) {
                    if (service.Properties["Caption"].Value != null) {
                        string capt = service.Properties["Caption"].Value.ToString();

                        if (capt.Contains("(COM")) {
                            capt = capt.Substring(capt.IndexOf("(COM") + 1).Replace(")", "");

                            string address = service.Properties["DeviceID"].Value.ToString();
                            address = address.Substring(0, address.LastIndexOf("_"));
                            address = address.Substring(address.LastIndexOf("&") + 1);

                            MyPort tmp = new MyPort();
                            tmp.portName = capt;
                            tmp.address = address;
                            tmpPorts.Add(tmp);
                        }
                    }
                }
            }

            public int scanDevs(ComboBox devCombo) {
                if (getRadio() != null) {
                    devCombo.Invoke(new Action(() => {
                        devCombo.Items.Clear();
                        devCombo.Enabled = false;
                    }));

                    findPorts();

                    BluetoothDeviceInfo[] scanDevs = new BluetoothClient().DiscoverDevices();

                    myPorts.Clear();

                    for (int i = 0; i < scanDevs.Length; i++) {
                        if (Array.IndexOf(scanDevs[i].InstalledServices, BluetoothService.SerialPort) != -1) {
                            string address = scanDevs[i].DeviceAddress.ToString();

                            int j = 0;
                            string com = "";

                            while (j < tmpPorts.Count && com == "") {
                                if (address == tmpPorts[j].address) {
                                    MyPort tmp = new MyPort();

                                    tmp.address = address;
                                    tmp.devName = scanDevs[i].DeviceName;
                                    tmp.portName = tmpPorts[j].portName;

                                    com = tmpPorts[j].portName;

                                    myPorts.Add(tmp);
                                }

                                j++;
                            }

                            if (com != "")
                                devCombo.Invoke(new Action(() => devCombo.Items.Add(scanDevs[i].DeviceName /*+ " (" + com + ")"*/)));
                        }
                    }

                    if (myPorts.Count > 0)
                        devCombo.Invoke(new Action(() => devCombo.SelectedIndex = 0));
                    else
                        MessageForm.Show(MessageForm.ping_sad, "Проверьте устройство и повторите попытку ещё раз.", "Устройства не обнаружены");
                }
                else
                    MessageForm.Show(MessageForm.ping_sad, "На Вашем устройстве отключен Bluetooth!", "Включите Bluetooth");

                return myPorts.Count;
            }

            public bool open() {
                bool openResult = true;

                try {
                    serialPort.Open();
                } catch (Exception e) {
                    string msg = e.Message;

                    if (e.Message.Contains("Превышен таймаут семафора"))
                        MessageForm.Show(MessageForm.ping_sad, "Возможные причины:\n1. устройство выключено\n2. выбрано не то устройство\n\nВозможные решения:\n1. включите устройство\n2. выберите другое устройство", "Не удалось соединиться с устройством");
                    else if (e.Message.Contains("Элемент не найден"))
                        MessageForm.Show(MessageForm.ping_sad, "Попробуйте отключить устройство от питания и подключить заново.", "Не удалось соединиться с устройством");
                    else if (e.Message.Contains("Доступ к порту") && e.Message.Contains("закрыт"))
                        MessageForm.Show(MessageForm.ping_sad, "Выбраное устройство уже используется.", "Не удалось соединиться с устройством");
                    else if (e.Message.Contains("Устройство не подключено"))
                        MessageForm.Show(MessageForm.ping_question, "Ну я не знаю... Попробуй ещё раз...", "Не удалось соединиться с устройством");
                    else
                        MessageForm.Show(MessageForm.ping_question, e.Message, "Не удалось соединиться с устройством");

                    openResult = false;
                }

                return openResult;
            }

            public bool write(string msg) {
                bool isWrited = false;

                try {
                    if (serialPort.IsOpen) {
                        serialPort.Write(msg);
                        isWrited = true;
                    }
                } catch (Exception) {
                    close();

                    isWrited = false;
                }

                return isWrited;
            }

            public void close() {
                try {
                    if (serialPort.IsOpen)
                        serialPort.Close();
                }
                catch (Exception e) {
                    MessageForm.Show(MessageForm.ping_question, e.Message, "Что-то пошло не так...");
                    serialPort.Dispose();
                }
            }
        }
    }
}
