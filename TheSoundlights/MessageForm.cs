using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheSoundlights {
    public partial class MessageForm : Form {
        static MessageForm msgForm;
        static DialogResult result = DialogResult.None;

        public const int ping_lol = 0;
        public const int ping_sad = 1;
        public const int ping_question = 2;
        public const int ping_awful = 3;

        public MessageForm() {
            InitializeComponent();
        }

        public static DialogResult Show(int pingN, string text, string caption = "", string btnOKText = "OK", string btnCancelText = "") {
            msgForm = new MessageForm();

            msgForm.Font = new Font(MainForm.fontCollection.Families[0], 8.25f);
            msgForm.label.Font = new Font(MainForm.fontCollection.Families[0], 11f);

            msgForm.Text = caption;
            msgForm.label.Text = text.Replace("\r\n", "\n").Replace("\n", "\r\n");

            msgForm.btn.Text = btnOKText;
            msgForm.cancelBtn.Text = btnCancelText;

            if (btnCancelText == "") {
                msgForm.cancelBtn.Visible = false;
                msgForm.locateControlCenter(msgForm.btn, msgForm.btn.Location.Y);
            }

            msgForm.label.Enter += (object sender, EventArgs e) => {
                msgForm.ActiveControl = msgForm.btn;
            };

            Bitmap ping = null;

            switch (pingN) {
                case ping_lol:
                    ping = Properties.Resources.ping_lol;
                    break;

                case ping_sad:
                    ping = Properties.Resources.ping_sad;
                    break;

                case ping_question:
                    ping = Properties.Resources.ping_question;
                    break;

                case ping_awful:
                    ping = Properties.Resources.ping_awful;
                    break;
            }

            msgForm.Paint += (object sender, PaintEventArgs e) => {
                int w = 110;
                int h = (int)(((double)w / ping.Width) * ping.Height);

                int x = msgForm.Width - w - 15;
                int y = 5;

                e.Graphics.DrawImage(ping, x, y, w, h);
            };
            
            msgForm.ActiveControl = msgForm.cancelBtn.Text == "" ? msgForm.btn : msgForm.cancelBtn;
            msgForm.ShowDialog();

            return result;
        }

        private void locateControlCenter(Control elem, int y) {
            elem.Location = new Point((Width - elem.Width) / 2, y);
        }

        private void btn_Click(object sender, EventArgs e) {
            result = btn.Text == "OK" ? DialogResult.OK : DialogResult.Yes;

            msgForm.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            result = cancelBtn.Text == "Cancel" ? DialogResult.Cancel : DialogResult.No;

            msgForm.Close();
        }
    }
}
