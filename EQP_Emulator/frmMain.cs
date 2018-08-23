using EQP_Emulator.UI_Update;
using EQP_Emulator.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace EQP_Emulator
{
    public partial class frmMain : Form, IConnectionReport
    {
        SocketClient conn;
        CmdDefine define = new CmdDefine();
        BindingList<CmdScript> oCmdScript = new BindingList<CmdScript>();
        Boolean isAlarmSet = false;
        Boolean isCmdFin = true;
        Boolean isScriptStop = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            foreach (string element in define.cmdType)
            {
                cbCmdType.Items.Add(element);
            }
            tbTimes.MaxLength = 6;
        }

        private void createCommand(object sender, EventArgs e)
        {
            string strCmd = cbCmdType.Text + ":" + cbCmd.Text;
            StringBuilder cmd = new StringBuilder(strCmd);
            if (cbPara1.Visible)
            {
                cmd.Append("/" + cbPara1.Text);
            }
            if (cbPara2.Visible)
            {
                cmd.Append("/" + cbPara2.Text);
            }
            if (cbPara3.Visible)
            {
                cmd.Append("/" + cbPara3.Text);
            }
            if (cbPara4.Visible)
            {
                cmd.Append("/" + cbPara4.Text);
            }
            tbCmd.Text = cmd.ToString();
        }

        private void cbCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPara1.Items.Clear();
            cbPara2.Items.Clear();
            cbPara3.Items.Clear();
            cbPara4.Items.Clear();
            cbPara1.Visible = false;
            cbPara2.Visible = false;
            cbPara3.Visible = false;
            cbPara4.Visible = false;


            string strCmd = cbCmdType.Text + ":" + cbCmd.Text;
            string[] p1Ary;
            string[] p2Ary;
            string[] p3Ary;
            string[] p4Ary;

            if (define.cmdParams1.TryGetValue(strCmd, out p1Ary))
            {
                foreach (string element in p1Ary)
                {
                    cbPara1.Items.Add(element);
                }
                cbPara1.Visible = true;
                cbPara1.SelectedIndex = 0;
            }

            if (define.cmdParams2.TryGetValue(strCmd, out p2Ary))
            {
                foreach (string element in p2Ary)
                {
                    cbPara2.Items.Add(element);
                }
                cbPara2.Visible = true;
                cbPara2.SelectedIndex = 0;
            }

            if (define.cmdParams3.TryGetValue(strCmd, out p3Ary))
            {
                foreach (string element in p3Ary)
                {
                    cbPara3.Items.Add(element);
                }
                cbPara3.Visible = true;
                cbPara3.SelectedIndex = 0;
            }

            if (define.cmdParams4.TryGetValue(strCmd, out p4Ary))
            {
                foreach (string element in p4Ary)
                {
                    cbPara4.Items.Add(element);
                }
                cbPara4.Visible = true;
                cbPara4.SelectedIndex = 0;
            }
        }

        private void cbCmdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCmdType.Text.Equals(""))
                return;

            cbCmd.Items.Clear();
            string strCmdType = cbCmdType.Text;
            string[] cmdAry = define.cmdList[strCmdType];
            if (cmdAry != null)
            {
                foreach (string element in cmdAry)
                {
                    cbCmd.Items.Add(element);
                }
                cbCmd.SelectedIndex = 0;
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            conn = new SocketClient(tbHostIP.Text, int.Parse(tbPort.Text), this);
            conn.Start();
        }

        void IConnectionReport.On_Connection_Message(object Msg)
        {
            string replyMsg = (string)Msg;
            if (replyMsg.StartsWith("NAK") || replyMsg.StartsWith("CAN") || replyMsg.StartsWith("ABS"))
            {
                isAlarmSet = true;
            }
            else
            {
                isAlarmSet = false;
            }

            FormMainUpdate.LogUpdate("Reveive <= " + replyMsg);
            //Thread.Sleep(1000);
            if (replyMsg.StartsWith("INF"))
            {
                string[] cmd = replyMsg.Split(new char[] { ':', '/' });
                if (define.autoAckCmd.Contains(cmd[1]))
                {
                    Thread.Sleep(200);
                    string ackMsg = replyMsg.Replace("INF:", "ACK:");
                    conn.Send(ackMsg + "\r");
                    FormMainUpdate.LogUpdate("     Send => " + ackMsg);
                }
                isCmdFin = true;
            }
        }

        void IConnectionReport.On_Connection_Connecting(string Msg)
        {
            FormMainUpdate.ConnectUpdate("Connecting");
            FormMainUpdate.LogUpdate("Connecting");
        }

        void IConnectionReport.On_Connection_Connected(object Msg)
        {
            FormMainUpdate.ConnectUpdate("Connected");
            FormMainUpdate.LogUpdate("Connected");
        }

        void IConnectionReport.On_Connection_Disconnected(string Msg)
        {
            FormMainUpdate.ConnectUpdate("Disconnected");
            FormMainUpdate.LogUpdate("Disconnected");
        }

        void IConnectionReport.On_Connection_Error(string Msg)
        {
            FormMainUpdate.ConnectUpdate("Connection_Error");
            FormMainUpdate.LogUpdate("Connection_Error");
        }

        private void btnDisConn_Click(object sender, EventArgs e)
        {
            conn = null;
            FormMainUpdate.ConnectUpdate("Disconnected");
            FormMainUpdate.LogUpdate("Disconnected");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (conn == null)
                MessageBox.Show("Please connect first!!");
            else
            {
                sendCommand(tbCmd.Text);
            }
        }

        private void btnClearMsg_Click(object sender, EventArgs e)
        {
            rtbMsg.Clear();
        }

        private void tbTimes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnAddScript_Click(object sender, EventArgs e)
        {
            if (tbCmd.Text.Trim().Equals(""))
            {
                MessageBox.Show("No command data!");
                return;
            }

            dgvCmdScript.DataSource = null;
            int seq = oCmdScript.Count + 1;
            oCmdScript.Add(new CmdScript { Seq = seq, Command = tbCmd.Text });
            dgvCmdScript.DataSource = oCmdScript;
            dgvCmdScript.Columns[0].Width = 30;
            dgvCmdScript.Columns[1].Width = 300;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (!checkSelctData())
                return;
            string msg = "Are you sure to delete this item?";
            DialogResult confirm = MessageBox.Show(msg, "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (confirm != System.Windows.Forms.DialogResult.Yes)
                return;

            int idx = dgvCmdScript.CurrentCell.RowIndex;
            int delSeq = idx + 1;
            oCmdScript.RemoveAt(idx);
            foreach (CmdScript element in oCmdScript)
            {
                if (element.Seq > delSeq)
                    element.Seq--;
            }
            oCmdScript = new BindingList<CmdScript>(oCmdScript.OrderBy(x => x.Seq).ToList());
            dgvCmdScript.DataSource = oCmdScript;
            setSelectRow(idx);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (!checkSelctData())
                return;
            int idx = dgvCmdScript.CurrentCell.RowIndex;
            try
            {
                if (idx > 0)
                {
                    CmdScript preItem = ((IEnumerable<CmdScript>)oCmdScript.ToList()).FirstOrDefault(predicate: d => d.Seq == idx);
                    CmdScript selItem = ((IEnumerable<CmdScript>)oCmdScript.ToList()).FirstOrDefault(predicate: d => d.Seq == idx + 1);
                    preItem.Seq = idx + 1; // change sequence
                    selItem.Seq = idx;
                    oCmdScript = new BindingList<CmdScript>(oCmdScript.OrderBy(x => x.Seq).ToList());
                    dgvCmdScript.DataSource = oCmdScript;
                    dgvCmdScript.ClearSelection();
                    dgvCmdScript.CurrentCell = dgvCmdScript.Rows[idx - 1].Cells[0];
                    dgvCmdScript.Rows[idx - 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (!checkSelctData())
                return;
            int idx = dgvCmdScript.CurrentCell.RowIndex;
            try
            {
                if (idx < dgvCmdScript.RowCount - 1)
                {
                    CmdScript nexItem = ((IEnumerable<CmdScript>)oCmdScript.ToList()).FirstOrDefault(predicate: d => d.Seq == idx + 2);
                    CmdScript selItem = ((IEnumerable<CmdScript>)oCmdScript.ToList()).FirstOrDefault(predicate: d => d.Seq == idx + 1);
                    nexItem.Seq = idx + 1; // change sequence
                    selItem.Seq = idx + 2;
                    oCmdScript = new BindingList<CmdScript>(oCmdScript.OrderBy(x => x.Seq).ToList());
                    dgvCmdScript.DataSource = oCmdScript;
                    //dgvCmdScript.ClearSelection();
                    //dgvCmdScript.CurrentCell = dgvCmdScript.Rows[idx + 1].Cells[0];
                    //dgvCmdScript.Rows[idx + 1].Selected = true;
                    setSelectRow(idx + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStepRun_Click(object sender, EventArgs e)
        {
            if (!checkSelctData())
                return;

            int idx = dgvCmdScript.CurrentCell.RowIndex;
            //send command
            if (conn == null)
            {
                MessageBox.Show("Please connect first!!");
                return;
            }
            else
            {
                string cmd = (string)dgvCmdScript.Rows[idx].Cells["Command"].Value;
                sendCommand(cmd);
            }

            //change index
            if (idx < dgvCmdScript.RowCount - 1)
            {
                setSelectRow(idx + 1);
            }
            else
            {
                setSelectRow(0);
            }
        }

        private Boolean checkSelctData()
        {
            Boolean result = false;
            try
            {
                if (dgvCmdScript.RowCount == 0)
                {
                    MessageBox.Show("No data exists!");
                    return result;
                }
                if (dgvCmdScript.CurrentCell == null)
                {
                    MessageBox.Show("Please select one row!");
                    return result;
                }
                result = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }

        private void setSelectRow(int idx)
        {
            dgvCmdScript.ClearSelection();
            if (dgvCmdScript.RowCount <= 0)
                return;
            else if (dgvCmdScript.RowCount == 1)
                idx = 0;//only one record 
            else if (idx >= dgvCmdScript.RowCount)
                idx = dgvCmdScript.RowCount - 1;//idx more than last 
            dgvCmdScript.CurrentCell = dgvCmdScript.Rows[idx].Cells[0];
            dgvCmdScript.Rows[idx].Selected = true;
        }

        private void sendCommand(string cmd)
        {
            try
            {
                conn.Send(cmd + ";\r");
                FormMainUpdate.LogUpdate("\n     Send => " + cmd + ";");
            }
            catch (Exception ex)
            {
                isAlarmSet = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnScriptRun_Click(object sender, EventArgs e)
        {
            if (dgvCmdScript.RowCount == 0)
            {
                MessageBox.Show("No data exists!");
                return;
            }
            if (conn == null)
            {
                MessageBox.Show("Please connect first!!");
                return;
            }
            isScriptStop = false;//set Script 執行中
            ThreadPool.QueueUserWorkItem(new WaitCallback(runScript));
        }
        private Boolean checkFin()
        {
            return lblStatus.Text.Equals("Command Completed.");
        }

        private void runScript(object data)
        {
            int repeatTimes = 0;
            int.TryParse(tbTimes.Text, out repeatTimes);
            //The efem motion is not allowed when the alarm occurs,please reset alarm first.
            int cnt = 1;
            while (cnt <= repeatTimes  && !isAlarmSet && !isScriptStop)
            {
                FormMainUpdate.LogUpdate("********  Run Script: " + cnt + "  ********");
                //for (int idx = 0; idx < dgvCmdScript.RowCount; idx++)
                foreach (CmdScript element in oCmdScript)
                {
                    string cmd = element.Command;
                    isCmdFin = false;
                    sendCommand(cmd);
                    SpinWait.SpinUntil(() => isCmdFin, 3000);// pause for motion complete
                    if (!isCmdFin)
                    {
                        MessageBox.Show("Command Timeout");
                        isAlarmSet = true;
                        break;//exit for
                    }
                    //resummn after motion complete               
                    if (isAlarmSet)
                    {
                        MessageBox.Show("Execute " + cmd + " error.");
                        break;//exit for
                    }
                    if (isScriptStop)
                    {
                        MessageBox.Show("Manual stop !!");
                        break;//exit for
                    }
                }
                cnt++;
                //tbTimes.Text = repeatTimes.ToString(); kuma update 次數
            }
        }

        private void btnScriptStop_Click(object sender, EventArgs e)
        {
            isScriptStop = true;
        }
    }
}
