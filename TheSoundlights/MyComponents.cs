using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace TheSoundlights {
    public partial class MainForm : Form {
        public class MyButton : Button {
            protected override bool ShowFocusCues {
                get {
                    return false;
                }
            }
        }
        
        public class MyComboBox : ComboBox {
            protected override bool ShowFocusCues {
                get {
                    return false;
                }
            }

            protected override void OnPaintBackground(PaintEventArgs e) {
                base.OnPaintBackground(e);
                using (var brush = new SolidBrush(BackColor)) {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                    e.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
                }
            }
        }
        
        public class MyCheckBox : CheckBox {
            protected override bool ShowFocusCues {
                get {
                    return false;
                }
            }
        }
        
        public class MyTrackBar : TrackBar {
            protected override void WndProc(ref Message m) {
                if (m.Msg == 0x0007)
                    return;

                base.WndProc(ref m);
            }
        }
    }
}
