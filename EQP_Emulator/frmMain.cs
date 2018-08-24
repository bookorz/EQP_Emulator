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
using System.IO;
using Newtonsoft.Json;
using System.Collections;

namespace EQP_Emulator
{
    public partial class frmMain : Form, IConnectionReport
    {
        SocketClient conn;
        CmdDefine define = new CmdDefine();
        BindingList<CmdScript> oCmdScript = new BindingList<CmdScript>();
        Boolean isAlarmSet = false;
        Boolean isCmdFin = true;
        Boolean isScriptRunning = false;
        Label[] p1Ary;
        Label[] p2Ary;
        Label[] p3Ary;
        Label[] p4Ary;
        Dictionary<string, Label[]> pMap = new Dictionary<string, Label[]> ();
        ComboBox[] t_cbs;

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
            btnConn_Click(null,null);
            tbCmd.Select();
            initData();
        }

        private void initData()
        {
            p1Ary = new Label[] { lbl_p101, lbl_p102, lbl_p103, lbl_p104, lbl_p105,
                                  lbl_p106, lbl_p107, lbl_p108, lbl_p109, lbl_p110,
                                  lbl_p111, lbl_p112, lbl_p113, lbl_p114, lbl_p115,
                                  lbl_p116, lbl_p117, lbl_p118, lbl_p119, lbl_p120,
                                  lbl_p121, lbl_p122, lbl_p123, lbl_p124, lbl_p125
                                };
            p2Ary = new Label[] { lbl_p201, lbl_p202, lbl_p203, lbl_p204, lbl_p205,
                                  lbl_p206, lbl_p207, lbl_p208, lbl_p209, lbl_p210,
                                  lbl_p211, lbl_p212, lbl_p213, lbl_p214, lbl_p215,
                                  lbl_p216, lbl_p217, lbl_p218, lbl_p219, lbl_p220,
                                  lbl_p221, lbl_p222, lbl_p223, lbl_p224, lbl_p225
                                };
            p3Ary = new Label[] { lbl_p301, lbl_p302, lbl_p303, lbl_p304, lbl_p305,
                                  lbl_p306, lbl_p307, lbl_p308, lbl_p309, lbl_p310,
                                  lbl_p311, lbl_p312, lbl_p313, lbl_p314, lbl_p315,
                                  lbl_p316, lbl_p317, lbl_p318, lbl_p319, lbl_p320,
                                  lbl_p321, lbl_p322, lbl_p323, lbl_p324, lbl_p325
                                };
            p4Ary = new Label[] { lbl_p401, lbl_p402, lbl_p403, lbl_p404, lbl_p405,
                                  lbl_p406, lbl_p407, lbl_p408, lbl_p409, lbl_p410,
                                  lbl_p411, lbl_p412, lbl_p413, lbl_p414, lbl_p415,
                                  lbl_p416, lbl_p417, lbl_p418, lbl_p419, lbl_p420,
                                  lbl_p421, lbl_p422, lbl_p423, lbl_p424, lbl_p425
                                };
            pMap.Add("P1", p1Ary);
            pMap.Add("P2", p2Ary);
            pMap.Add("P3", p3Ary);
            pMap.Add("P4", p4Ary);
            t_cbs = new ComboBox[] { cbP1Target, cbP2Target, cbP3Target, cbP4Target, };
            cbP1Size.SelectedIndex = 0;
            cbP2Size.SelectedIndex = 0;
            cbP3Size.SelectedIndex = 0;
            cbP4Size.SelectedIndex = 0;
        }

        private void createCommand(object sender, EventArgs e)
        {
            string strCmd = cbCmdType.Text + ":" + cbCmd.Text;
            StringBuilder cmd = new StringBuilder(strCmd);
            if (cbCmd.Text.Equals("TRANS"))
            {
                cmd.Append("/" + cbPara1.Text);
                cmd.Append(">" + cbPara2.Text);
                cmd.Append("/" + cbPara3.Text);
                cmd.Append(">" + cbPara4.Text);
            }
            else
            {
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
            }
            tbCmd.Text = cmd.ToString() + ";";
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

            createCommand(sender,e);

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
                FormMainUpdate.AlarmUpdate("Alarm set");
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
                //if (define.autoAckCmd.Contains(cmd[1]))
                //{
                //暫時收到INF一律回ACK
                    Thread.Sleep(200);//200
                    string ackMsg = replyMsg.Replace("INF:", "ACK:");
                    conn.Send(ackMsg + "\r");
                    FormMainUpdate.LogUpdate("     Send => " + ackMsg);
                //}
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
            if (!this.lbl_ConnectState.Text.Equals("Connected"))
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
            //dgvCmdScript.DataSource = oCmdScript;
            //dgvCmdScript.Columns[0].Width = 30;
            //dgvCmdScript.Columns[1].Width = 300;
            refreshScriptSet();
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
            if (!this.lbl_ConnectState.Text.Equals("Connected"))
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
                if (isScriptRunning)
                {
                    MessageBox.Show("Script is running , please stop it first!");
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
                conn.Send(cmd + "\r");
                FormMainUpdate.LogUpdate("\n     Send => " + cmd );
            }
            catch (Exception ex)
            {
                isAlarmSet = true;
                MessageBox.Show(ex.Message);
                FormMainUpdate.AlarmUpdate("Alarm set");
            }
        }

        private void btnScriptRun_Click(object sender, EventArgs e)
        {
            if (dgvCmdScript.RowCount == 0)
            {
                MessageBox.Show("No data exists!");
                return;
            }
            if (!this.lbl_ConnectState.Text.Equals("Connected"))
            {
                MessageBox.Show("Please connect first!!");
                return;
            }
            if (isAlarmSet)
            {
                MessageBox.Show("Please reset alarm first!");
                return;
            }
            setIsRunning(true);//set Script 執行中
            //isScriptRunning = true;
            //setRunBtnEnable(true);
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
            while (cnt <= repeatTimes  && !isAlarmSet && isScriptRunning)
            {
                FormMainUpdate.LogUpdate("\n**************  Run Script: " + cnt + "  **************");
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
                        FormMainUpdate.AlarmUpdate("Alarm set");
                        isAlarmSet = true;
                        break;//exit for
                    }
                    //resummn after motion complete               
                    if (isAlarmSet)
                    {
                        MessageBox.Show("Execute " + cmd + " error.");
                        break;//exit for
                    }
                    if (!isScriptRunning)
                    {
                        MessageBox.Show("Script stop !!");
                        break;//exit for
                    }
                }
                cnt++;
            }
            MessageBox.Show("Command Script done.");
            setIsRunning(false);//執行結束

        }

        private void btnScriptStop_Click(object sender, EventArgs e)
        {
            setIsRunning(false);
            FormMainUpdate.LogUpdate("\n*************   Manual Stop: Begin   *************");
            sendCommand("MOV: HOLD;");//send pause command to efem
            Thread.Sleep(200);
            sendCommand("MOV: ABORT;");//send abort command to efem
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FormMainUpdate.AlarmUpdate("Alarm clear");
            isAlarmSet = false;
            setIsRunning(false);
        }


        private void setIsRunning(Boolean isRun)
        {
            isScriptRunning = isRun;
            FormMainUpdate.SetRunBtnEnable(isRun);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvCmdScript.Rows.Count == 0)
            {
                return;
            }

            SaveFileDialog saveFileDialog1;
            StreamWriter sw;

            try
            {
                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save file";
                saveFileDialog1.InitialDirectory = ".\\";
                saveFileDialog1.Filter = "json files (*.json)|*.json";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sw = new StreamWriter(saveFileDialog1.FileName.ToString());

                    sw.WriteLine(JsonConvert.SerializeObject(oCmdScript, Formatting.Indented));

                    sw.Close();

                    MessageBox.Show("Done it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1;
            StreamReader myStream = null;

            try
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "json files (*.json)|*.json";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string line = string.Empty;

                    using (myStream = new StreamReader(openFileDialog1.FileName))
                    {
                        line = myStream.ReadToEnd();
                    }

                    oCmdScript = (BindingList<CmdScript>)Newtonsoft.Json.JsonConvert.DeserializeObject(line, (typeof(BindingList<CmdScript>)));
                    refreshScriptSet();
                    //MessageBox.Show("Done it.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void refreshScriptSet()
        {
            dgvCmdScript.DataSource = oCmdScript;
            if (dgvCmdScript.RowCount > 0)
            {
                dgvCmdScript.Columns[0].Width = 30;
                dgvCmdScript.Columns[1].Width = 300;
            }
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            try
            {
                oCmdScript.Clear();//remove list
                //if(dgvCmdScript.RowCount > 0)
                //{
                //    dgvCmdScript.Columns[0].Width = 30;
                //    dgvCmdScript.Columns[1].Width = 300;
                //}
                refreshScriptSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void slot_assign(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            switch (me.Button)
            {
                case MouseButtons.Left:
                    // Left click
                    break;
                case MouseButtons.Right:
                    // Right click
                    return;
            }
            Label label = ((Label)sender);
            Color color = label.BackColor;
            label.Text = ""; //重新 assign 時, 一律清空目的地
            if (color == SystemColors.HighlightText)
            {
                label.BackColor = Color.LightGreen;
                //label.Text = getWaferID(label);
            }
            else if (color == Color.LightGreen)
            {
                label.BackColor = SystemColors.HighlightText;
                //label.Text = "";
            }
                
        }
        //private string getWaferID(Label label)
        //{
        //    return label.Name;
        //}
        private void checkUnloadPort(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Text.Equals(""))
                return;//Do not need to check
            string result = "";
            Boolean isNotUnload = false;
            Boolean isUsed = false;
            //check target is unlaod port or not 
            string port_type = "";
            switch (cb.Text)
            {
                case "P1":
                    port_type = cbP1Type.Text;           
                    break;
                case "P2":
                    port_type = cbP2Type.Text;
                    break;
                case "P3":
                    port_type = cbP3Type.Text;
                    break;
                case "P4":
                    port_type = cbP4Type.Text;
                    break;
            }
            isNotUnload = !port_type.Equals("U")?true:false;
            //check target is in use
            foreach (ComboBox item in t_cbs)
            {
                //if cb.Name = "cbPxTarget" AND cb.Text = "Px" => Do not need to check
                if (cb.Name.Contains(cb.Text))
                    continue;
                if (cb.Name == item.Name)//the same object => skip
                    continue;
                if (cb.Text == item.Text )
                    isUsed = true;
            }
            if (isNotUnload)
                result = "Port " + cb.Text + " is not Unload port.";
            else if (isUsed)
                result = "Port " + cb.Text + " is in use.";

            if (!result.Equals(""))
            {
                MessageBox.Show(result);
                cb.SelectedIndex = -1;
            }
        }

        private void setPortType(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Label[] labels = null;
            ComboBox t_cb = null; //combobox of target port
            Color color;
            Boolean isClear = cb.Text.Equals("U");//unload port => clear slot data
            color = isClear? color = SystemColors.HighlightText: color = Color.LightGreen;
            string port_name = "";
            switch (cb.Name)
            {
                case "cbP1Type":
                    labels = p1Ary;
                    t_cb = cbP1Target;
                    port_name = "P1";
                    break;
                case "cbP2Type":
                    labels = p2Ary;
                    t_cb = cbP2Target;
                    port_name = "P2";
                    break;
                case "cbP3Type":
                    labels = p3Ary;
                    t_cb = cbP3Target;
                    port_name = "P3";
                    break;
                case "cbP4Type":
                    labels = p4Ary;
                    t_cb = cbP4Target;
                    port_name = "P4";
                    break;
            }
            switch (cb.Text)
            {
                case "L":
                    t_cb.Enabled = true;//only load port can change target
                    break;
                case "U":
                    //t_cb.Enabled = true;//Reset object must be available
                    t_cb.SelectedIndex = -1;
                    t_cb.Text = "";
                    t_cb.Enabled = false;
                    break;
                case "LU":
                    t_cb.SelectedText = port_name;
                    t_cb.Enabled = false;
                    break;
            }
            if (labels != null)
            {
                foreach(Label item in labels)
                {
                    //item.Text = isClear? "":getWaferID(item);
                    item.Text = "";
                    item.BackColor = color;
                    item.Enabled = !isClear;
                }
            }
            // If the unload condition changes, clear the dependency
            if (!isClear)
            {
                foreach (ComboBox foo in t_cbs)
                {
                    if (foo.Name.Contains(port_name))//the same object => skip
                        continue;
                    if (foo.Text == port_name)
                    {
                        foo.SelectedIndex = -1;
                        assignTarget(pMap[foo.Name.Substring(2, 2)], ""); //clear target ID
                    }
                        
                }
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            //assign port1
            if (!cbP1Type.Text.Equals("U") && !cbP1Type.Text.Equals(""))
                assignTarget(p1Ary, cbP1Target.Text);
            //assign port2
            if (!cbP2Type.Text.Equals("U") && !cbP2Type.Text.Equals(""))
                assignTarget(p2Ary, cbP2Target.Text);
            //assign port3
            if (!cbP3Type.Text.Equals("U") && !cbP3Type.Text.Equals(""))
                assignTarget(p3Ary, cbP3Target.Text);
            //assign port4
            if (!cbP4Type.Text.Equals("U") && !cbP4Type.Text.Equals(""))
                assignTarget(p4Ary, cbP4Target.Text);
            MessageBox.Show("Succesessfully Completed.");
        }
        private void assignTarget(Label[] labels, string target)
        {
            foreach(Label label in labels)
            {
                if (label.BackColor == SystemColors.HighlightText)
                    continue;//no wafer
                string t_slot = target + label.Name.Substring(6);
                label.Text = target.Equals("")?"": t_slot;
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            
            string msg = "Are you sure you want to execute the transfer job?";
            DialogResult confirm = MessageBox.Show(msg, "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (confirm != System.Windows.Forms.DialogResult.Yes)
                return;
            tabMode.SelectedIndex = 0;
            tbTimes.Text = "1";//set run times 1
            oCmdScript.Clear();//clear script
            for(int i = 0; i < 100; i++)
            {
                int seq = oCmdScript.Count + 1;
                oCmdScript.Add(new CmdScript { Seq = seq, Command = "MOV:INIT/ALL;" + seq });
            }
            refreshScriptSet();
            btnScriptRun_Click(btnScriptRun, e);
        }

        private void dgvCmdScript_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCmdScript.SelectedCells[0].ColumnIndex < 1)
                return;// not command cell
            string o_value = dgvCmdScript.SelectedCells[0].Value.ToString();
            string n_value = ShowDialog("Update", "New Command:", o_value);
            if (n_value.Equals(""))
                return;//cancel update
            else
            {
                CmdScript cmd = oCmdScript.ElementAt(dgvCmdScript.CurrentCell.RowIndex);
                cmd.Command = n_value;
                refreshScriptSet();
            }
           
        }

        public static string ShowDialog(string title, string label, string text)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = label , Width = 200 };            
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 ,Text = text };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 90, DialogResult = DialogResult.OK , Height = 35};
            textLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            confirmation.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
