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
        public static Boolean isAlarmSet = false;
        delegate void UpdateLog(string msg);
        delegate void UpdateBtnEnable(Boolean isRun);
        delegate void MessageShow(string msg);

        public static void ShowMessage(string msg)
        {
            Form form = Application.OpenForms["frmMain"];
            if (form == null)
                return;

            if (form.InvokeRequired)
            {
                MessageShow ph = new MessageShow(ShowMessage);
                form.BeginInvoke(ph, msg);
            }
            else
            {
                MessageBox.Show(form, msg);
            }

        }

        public static void SetRunBtnEnable(Boolean isRun)
        {
            Form form = Application.OpenForms["frmMain"];
            if (form == null)
                return;
            Button btnScriptRun = form.Controls.Find("btnScriptRun", true).FirstOrDefault() as Button;
            Button btnDel = form.Controls.Find("btnDel", true).FirstOrDefault() as Button;
            Button btnUp = form.Controls.Find("btnUp", true).FirstOrDefault() as Button;
            Button btnDown = form.Controls.Find("btnDown", true).FirstOrDefault() as Button;
            Button btnStepRun = form.Controls.Find("btnStepRun", true).FirstOrDefault() as Button;
            Button btnImport = form.Controls.Find("btnImport", true).FirstOrDefault() as Button;
            Button btnExport = form.Controls.Find("btnExport", true).FirstOrDefault() as Button;
            Button btnSend = form.Controls.Find("btnSend", true).FirstOrDefault() as Button;
            Button btnAddScript = form.Controls.Find("btnAddScript", true).FirstOrDefault() as Button;
            Button btnNewScript = form.Controls.Find("btnNewScript", true).FirstOrDefault() as Button;

            if (form.InvokeRequired)
            {
                UpdateBtnEnable ph = new UpdateBtnEnable(SetRunBtnEnable);
                form.BeginInvoke(ph, isRun);
            }
            else
            {
                btnScriptRun.Enabled = !isRun;
                btnDel.Enabled = !isRun;
                btnUp.Enabled = !isRun;
                btnDown.Enabled = !isRun;
                btnStepRun.Enabled = !isRun;
                btnImport.Enabled = !isRun;
                btnExport.Enabled = !isRun;
                btnSend.Enabled = !isRun;
                btnAddScript.Enabled = !isRun;
                btnNewScript.Enabled = !isRun;
            }
            
        }

        public static void AlarmUpdate(string status)
        {
            Form form = Application.OpenForms["frmMain"];
            if (form == null)
                return;
            Label W = form.Controls.Find("lbl_alarm", true).FirstOrDefault() as Label;
            Button btnReset = form.Controls.Find("btnReset", true).FirstOrDefault() as Button;

            if (W == null)
                return;

            if (W.InvokeRequired)
            {
                UpdateLog ph = new UpdateLog(AlarmUpdate);
                W.BeginInvoke(ph, status);
            }
            else
            {
                W.Text = status;
                switch (status)
                {
                    case "Alarm clear":
                        W.BackColor = Color.LimeGreen;
                        btnReset.Enabled = false;
                        isAlarmSet = false;
                        break;
                    case "Alarm set":
                        W.BackColor = Color.Red;
                        btnReset.Enabled = true;
                        isAlarmSet = true;
                        break;
                }
            }
        }

        public static void ConnectUpdate(string state)
        {
            try
            {
                Form form = Application.OpenForms["frmMain"];
                Label W;
                if (form == null)
                    return;

                W = form.Controls.Find("lbl_ConnectState", true).FirstOrDefault() as Label;
                Button btnDisConn = form.Controls.Find("btnDisConn", true).FirstOrDefault() as Button;
                Button btnConn = form.Controls.Find("btnConn", true).FirstOrDefault() as Button;
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
                            W.BackColor = Color.LimeGreen;
                            btnConn.Enabled = false;
                            btnDisConn.Enabled = true;
                            break;
                        case "Disconnected":
                            W.BackColor = Color.Gray;
                            btnConn.Enabled = true;
                            btnDisConn.Enabled = false;
                            break;
                        case "Connection_Error":
                            W.BackColor = Color.Red;
                            btnConn.Enabled = true;
                            btnDisConn.Enabled = false;
                            break;
                        case "Connecting":
                            W.BackColor = Color.Yellow;
                            btnConn.Enabled = false;
                            btnDisConn.Enabled = false;
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
                TabControl tabMode;
                if (form == null)
                    return;

                W = form.Controls.Find("rtbMsg", true).FirstOrDefault() as RichTextBox;
                tabMode = form.Controls.Find("tabMode", true).FirstOrDefault() as TabControl;
                
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
                    //if (W.Text.Length > 1000)
                    //{
                    //    W.Text = W.Text.Substring(W.Text.Length - 1000);
                    //}
                    if (W.Lines.Length > 1000)
                    {
                        W.Select(0, W.GetFirstCharIndexFromLine(W.Lines.Length - 1000));
                        W.SelectedText = "";
                    }
                    W.ScrollToCaret();
                    if (tabMode.SelectedIndex != 0)
                        tabMode.SelectedIndex = 0;
                }
            }
            catch
            {

            }
        }
    }
}
