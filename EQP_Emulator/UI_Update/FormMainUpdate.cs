using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQP_Emulator.UI_Update
{
    class FormMainUpdate
    {
        delegate void UpdateLog(string msg);

        public static void ConnectUpdate(string state)
        {
            try
            {
                Form form = Application.OpenForms["frmMain"];
                Label W;
                if (form == null)
                    return;

                W = form.Controls.Find("lbl_ConnectState", true).FirstOrDefault() as Label;
                if (W == null)
                    return;

                if (W.InvokeRequired)
                {
                    UpdateLog ph = new UpdateLog(ConnectUpdate);
                    W.BeginInvoke(ph, state);
                }
                else
                {
                    W.Text = state;
                    switch (state)
                    {
                        case "Connected":
                            W.BackColor = Color.Lime;
                            break;
                        case "Disconnected":
                            W.BackColor = Color.Gray;
                            break;
                        case "Connection_Error":
                            W.BackColor = Color.Red;
                            break;
                        case "Connecting":
                            W.BackColor = Color.Yellow;
                            break;
                    }

                }
            }
            catch
            {

            }
        }

        public static void LogUpdate(string msg)
        {
            try
            {
                Form form = Application.OpenForms["frmMain"];
                RichTextBox W;
                if (form == null)
                    return;

                W = form.Controls.Find("rtbMsg", true).FirstOrDefault() as RichTextBox;
                if (W == null)
                    return;

                if (W.InvokeRequired)
                {
                    UpdateLog ph = new UpdateLog(LogUpdate);
                    W.BeginInvoke(ph, msg);
                }
                else
                {
                    W.AppendText(msg + "\n");
                    if (W.Text.Length > 1000)
                    {
                        W.Text = W.Text.Substring(W.Text.Length - 1000);
                    }
                    W.ScrollToCaret();
                }
            }
            catch
            {

            }
        }
    }
}
