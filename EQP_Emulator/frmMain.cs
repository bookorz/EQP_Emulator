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
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using log4net;
using log4net.Config;
using System.Text.RegularExpressions;

namespace EQP_Emulator
{
    public partial class frmMain : Form, IConnectionReport
    {
        SocketClient conn;
        CmdDefine define = new CmdDefine();
        //private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ILog log = LogManager.GetLogger(typeof(frmMain));

        Boolean isCmdFin = true;
        Boolean isPause = false;
        Boolean isScriptRunning = false;
        Label[] p1Ary;
        Label[] p2Ary;
        Label[] p3Ary;
        Label[] p4Ary;
        Dictionary<string, Label[]> pMap = new Dictionary<string, Label[]> ();
        ComboBox[] t_cbs;
        int intCmdTimeOut = 300000;//default 5 mins
        int ackTimeOut = 5000; // default 5 seconds
        int ackSleepTime = 200; // default 0.2 seconds
        string currentCmd = "";

        public frmMain()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            foreach (string element in define.cmdType)
            {
                cbCmdType.Items.Add(element);
            }
            initData();
            btnConn_Click(null, null);
            //this.TopMost = true;
            //this.Focus();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
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
            t_cbs = new ComboBox[] { cbP1Target, cbP2Target, cbP3Target, cbP4Target };
            cbP1Size.SelectedIndex = 0;
            cbP2Size.SelectedIndex = 0;
            cbP3Size.SelectedIndex = 0;
            cbP4Size.SelectedIndex = 0;
            cbport_ml.SelectedIndex = 0;
            cbAlign_ma.SelectedIndex = 0;
            cbRobot_mr.SelectedIndex = 0;
            cbP1Type.SelectedIndex = 1;
            cbP2Type.SelectedIndex = 1;
            cbP3Type.SelectedIndex = 1;
            cbP4Type.SelectedIndex = 1;

            tbTimes.MaxLength = 6;
            tbHostIP.Text = ConfigurationManager.AppSettings["host_ip"];
            tbCmd.Select();
            if (ConfigurationManager.AppSettings["Robot1_enable"].Equals("false"))
            {
                cbR1Arm1.Enabled = false;
                cbR1Arm2.Enabled = false;
                cbR1Arm3.Enabled = false;
            }
            if (ConfigurationManager.AppSettings["Robot2_enable"].Equals("false"))
            {
                cbR2Arm1.Enabled = false;
                cbR2Arm2.Enabled = false;
                cbR2Arm3.Enabled = false;
            }
            cbA1.Enabled = ConfigurationManager.AppSettings["Aligner1_enable"].Equals("false") ? false : true;
            cbA2.Enabled = ConfigurationManager.AppSettings["Aligner2_enable"].Equals("false") ? false : true;
            cbLLA.Enabled = ConfigurationManager.AppSettings["LLA_enable"].Equals("false") ? false : true;
            cbLLB.Enabled = ConfigurationManager.AppSettings["LLB_enable"].Equals("false") ? false : true;
            cbLLC.Enabled = ConfigurationManager.AppSettings["LLC_enable"].Equals("false") ? false : true;
            cbLLD.Enabled = ConfigurationManager.AppSettings["LLD_enable"].Equals("false") ? false : true;
            p1gb.Enabled = ConfigurationManager.AppSettings["PORT1_Enable"].Equals("false") ? false : true;
            p2gb.Enabled = ConfigurationManager.AppSettings["PORT2_Enable"].Equals("false") ? false : true;
            p3gb.Enabled = ConfigurationManager.AppSettings["PORT3_Enable"].Equals("false") ? false : true;
            p4gb.Enabled = ConfigurationManager.AppSettings["PORT4_Enable"].Equals("false") ? false : true;
            intCmdTimeOut = Int32.Parse(ConfigurationManager.AppSettings["cmd_timeout"]);
            ackTimeOut = Int32.Parse(ConfigurationManager.AppSettings["ack_timeout"]);
            ackSleepTime = Int32.Parse(ConfigurationManager.AppSettings["ack_sleep_time"]);

            cbErrMsg.Items.AddRange(define.getErrMsgList());
            cbErrPos.Items.AddRange(define.getErrPosList());
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
            FormMainUpdate.LogUpdate("Reveive <= " + replyMsg);
            //if (replyMsg.StartsWith("NAK") || replyMsg.StartsWith("CAN") || replyMsg.StartsWith("ABS"))
            if (replyMsg.StartsWith("ABS"))
            {
                FormMainUpdate.AlarmUpdate(true);
                setIsRunning(false);//ABS stop script
            }else if (replyMsg.StartsWith("CAN") || replyMsg.StartsWith("NAK"))
            {
                setIsRunning(false);//CAN  or  NAK stop script
            }

            if (replyMsg.StartsWith("INF") || replyMsg.StartsWith("ABS"))
            {
                string[] cmd = replyMsg.Split(new char[] { ':', '/' });
                //收到INF,ABS 一律自動回ACK
                string ackMsg = replyMsg.Replace("INF:", "ACK:").Replace("ABS:", "ACK:");
                Thread.Sleep(ackSleepTime);
                sendCommand(ackMsg);
                if (!currentCmd.Equals("") && replyMsg.EndsWith(currentCmd))
                {
                    isCmdFin = true;
                }
            }
            if (replyMsg.StartsWith("INF:RESTR"))
            {
                isPause = false;
            } else if (replyMsg.StartsWith("INF:ABORT"))
            {
                isPause = false;
                setIsRunning(false);// ABORT 
            }
            else if (replyMsg.StartsWith("INF:ERROR/CLEAR"))
            {
                FormMainUpdate.AlarmUpdate(false);
                //isAlarmSet = false;
                setIsRunning(false); //ERROR CLEAR
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
            //isAlarmSet = true;
            FormMainUpdate.AlarmUpdate(true);
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
            if(tbCmd.Text.Trim().Equals(""))
                FormMainUpdate.ShowMessage("Command text is empty.");
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
                FormMainUpdate.ShowMessage("No command data!");
                return;
            }

            dgvCmdScript.DataSource = null;
            Command.addScriptCmd(tbCmd.Text);
            //int seq = oCmdScript.Count + 1;
            //oCmdScript.Add(new CmdScript { Seq = seq, Command = tbCmd.Text });
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
            Command.oCmdScript.RemoveAt(idx);
            foreach (CmdScript element in Command.oCmdScript)
            {
                if (element.Seq > delSeq)
                    element.Seq--;
            }
            Command.oCmdScript = new BindingList<CmdScript>(Command.oCmdScript.OrderBy(x => x.Seq).ToList());
            dgvCmdScript.DataSource = Command.oCmdScript;
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
                    CmdScript preItem = Command.getCmdList().FirstOrDefault(predicate: d => d.Seq == idx);
                    CmdScript selItem = Command.getCmdList().FirstOrDefault(predicate: d => d.Seq == idx + 1);
                    preItem.Seq = idx + 1; // change sequence
                    selItem.Seq = idx;
                    Command.oCmdScript = new BindingList<CmdScript>(Command.oCmdScript.OrderBy(x => x.Seq).ToList());
                    dgvCmdScript.DataSource = Command.oCmdScript;
                    dgvCmdScript.ClearSelection();
                    dgvCmdScript.CurrentCell = dgvCmdScript.Rows[idx - 1].Cells[0];
                    dgvCmdScript.Rows[idx - 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                FormMainUpdate.ShowMessage(ex.Message + ":" + ex.ToString());
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
                    CmdScript nexItem = Command.getCmdList().FirstOrDefault(predicate: d => d.Seq == idx + 2);
                    CmdScript selItem = Command.getCmdList().FirstOrDefault(predicate: d => d.Seq == idx + 1);
                    nexItem.Seq = idx + 1; // change sequence
                    selItem.Seq = idx + 2;
                    Command.oCmdScript = new BindingList<CmdScript>(Command.oCmdScript.OrderBy(x => x.Seq).ToList());
                    dgvCmdScript.DataSource = Command.oCmdScript;
                    setSelectRow(idx + 1);
                }
            }
            catch (Exception ex)
            {
                FormMainUpdate.ShowMessage(ex.Message + ":" + ex.ToString());
            }
        }

        private void btnStepRun_Click(object sender, EventArgs e)
        {
            if (!checkSelctData())
                return;

            int idx = dgvCmdScript.CurrentCell.RowIndex;
            //send command
            //if (!this.lbl_ConnectState.Text.Equals("Connected"))
            //{
            //    FormMainUpdate.ShowMessage("Please connect first!!");
            //    return;
            //}
            //else
            //{
                string cmd = (string)dgvCmdScript.Rows[idx].Cells["Command"].Value;
                sendCommand(cmd);
            //}

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
                    FormMainUpdate.ShowMessage("No data exists!"); 
                    return result;
                }
                if (dgvCmdScript.CurrentCell == null)
                {
                    FormMainUpdate.ShowMessage("Please select one row!"); 
                    return result;
                }
                if (isScriptRunning)
                {
                    FormMainUpdate.ShowMessage("Script is running , please stop it first!"); 
                    return result;
                }
                result = true;
            }
            catch (Exception e)
            {
                FormMainUpdate.ShowMessage(e.Message);
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
                if (FormMainUpdate.isAlarmSet && !cmd.StartsWith("ACK") && !cmd.Equals("SET:ERROR/CLEAR;"))
                {
                    FormMainUpdate.LogUpdate("Do not execute the following instructions in the abnormal state:" + cmd + "\n");
                }
                else if (!this.lbl_ConnectState.Text.Equals("Connected") && !cmd.Equals("ACK:READY/COMM;"))
                {
                    FormMainUpdate.ShowMessage("Please connect first!!");
                }
                else
                {
                    conn.Send(cmd + "\r");
                    log.Info(cmd);
                    FormMainUpdate.LogUpdate("     Send => " + cmd);
                }
            }
            catch (Exception ex)
            {
                FormMainUpdate.ShowMessage(ex.Message + ":" + ex.ToString());
                FormMainUpdate.AlarmUpdate(true);
            }
        }

        private void btnScriptRun_Click(object sender, EventArgs e)
        {
            if (dgvCmdScript.RowCount == 0)
            {
                FormMainUpdate.ShowMessage("No data exists!");
                return;
            }
            if (!this.lbl_ConnectState.Text.Equals("Connected"))
            {
                FormMainUpdate.ShowMessage("Please connect first!!");
                return;
            }
            if (FormMainUpdate.isAlarmSet)
            {
                FormMainUpdate.ShowMessage("Please reset alarm first!");
                return;
            }
            setIsRunning(true);//set Script 執行中
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
            while (cnt <= repeatTimes  && !FormMainUpdate.isAlarmSet && isScriptRunning)
            {
                FormMainUpdate.LogUpdate("\n**************  Run Script: " + cnt + "  **************");
                foreach (CmdScript element in Command.oCmdScript)
                {
                    SpinWait.SpinUntil(() => !isPause, 3600000);// wait for pause 
                    if (isPause)
                    {
                        FormMainUpdate.ShowMessage("Pause Timeout");
                        FormMainUpdate.AlarmUpdate(true);
                        return;//exit for
                    }
                    if (!isScriptRunning)
                    {
                        FormMainUpdate.ShowMessage("Script stop !!");
                        return;//exit for
                    }

                    string cmd = element.Command;
                    isCmdFin = false;
                    FormMainUpdate.LogUpdate("\n**************  Script Commnad Start  **************");
                    sendCommand(cmd);
                    currentCmd = cmd.Replace("MOV","").Replace("SET", "").Replace("GET", "");    
                    SpinWait.SpinUntil(() => isCmdFin, intCmdTimeOut);// wait for command complete       
                    if (!isCmdFin)
                    {
                        FormMainUpdate.ShowMessage("Command Timeout");
                        FormMainUpdate.AlarmUpdate(true);
                        return;//exit for
                    }
                    //resummn after motion complete               
                    if (FormMainUpdate.isAlarmSet)
                    {
                        FormMainUpdate.ShowMessage("Execute " + cmd + " error.");
                        return;//exit for
                    }
                    currentCmd = ""; //clear command
                    FormMainUpdate.LogUpdate("**************  Script Commnad Finish  **************");
                }
                cnt++;
            }
            FormMainUpdate.ShowMessage("Command Script done.");
            setIsRunning(false);//執行結束

        }

        private void btnScriptStop_Click(object sender, EventArgs e)
        {
            FormMainUpdate.AlarmUpdate(false);
            isPause = false;
            FormMainUpdate.LogUpdate("\n*************   Manual Stop     *************");

            //sendCommand("MOV: HOLD;");//send pause command to efem
            //Thread.Sleep(200);
            //sendCommand("MOV: ABORT;");//send abort command to efem
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            sendCommand("SET:ERROR/CLEAR;");
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

                    sw.WriteLine(JsonConvert.SerializeObject(Command.oCmdScript, Formatting.Indented));

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

                    Command.oCmdScript = (BindingList<CmdScript>)Newtonsoft.Json.JsonConvert.DeserializeObject(line, (typeof(BindingList<CmdScript>)));
                    refreshScriptSet();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void refreshScriptSet()
        {
            dgvCmdScript.DataSource = Command.oCmdScript;
            if (dgvCmdScript.RowCount > 0)
            {
                dgvCmdScript.Columns[0].Width = 45;
                dgvCmdScript.Columns[1].Width = 300;
            }
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            try
            {
                Command.oCmdScript.Clear();//remove list
                refreshScriptSet();
            }
            catch (Exception ex)
            {
                FormMainUpdate.ShowMessage(ex.Message + ":" + ex.ToString());
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
            }
            else if (color == Color.LightGreen)
            {
                label.BackColor = SystemColors.HighlightText;
                //label.Text = "";
            }
                
        }
        private void checkUnloadPort(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Text.Equals(""))
                return;//Do not need to check
            string result = "";
            Boolean isNotUnload = false;
            Boolean isUsed = false;
            Boolean isPortEnable = true;
            //check target is unlaod port or not 
            string port_type = "";
            switch (cb.Text)
            {
                case "P1":
                    port_type = cbP1Type.Text;
                    isPortEnable = cbP1Type.Enabled;
                    break;
                case "P2":
                    port_type = cbP2Type.Text;
                    isPortEnable = cbP2Type.Enabled;
                    break;
                case "P3":
                    port_type = cbP3Type.Text;
                    isPortEnable = cbP3Type.Enabled;
                    break;
                case "P4":
                    port_type = cbP4Type.Text;
                    isPortEnable = cbP4Type.Enabled;
                    break;
            }
            if (port_type.Equals("LU") && cb.Name.Contains(cb.Text))
                return;//Do not need to check, It will be set automatically.

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
            else if (!isPortEnable)
                result = "Port " + cb.Text + " is not avalible.";

            if (!result.Equals(""))
            {
                FormMainUpdate.ShowMessage(result);
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
                    t_cb.SelectedIndex = -1;
                    //t_cb.Text = "";
                    t_cb.Enabled = true;//only load port can change target
                    break;
                case "U":
                    t_cb.SelectedIndex = -1;
                    //t_cb.Text = "";
                    t_cb.Enabled = false;
                    break;
                case "LU":
                    //t_cb.SelectedText = port_name;
                    t_cb.SelectedItem = port_name;
                    t_cb.Enabled = false;
                    break;
            }
            if (labels != null)
            {
                foreach(Label item in labels)
                {
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
            FormMainUpdate.ShowMessage("Succesessfully Completed.");
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
            initEFEMConfig();
            //tabMode.SelectedIndex = 0;
            tbTimes.Text = "1";//set run times 1
            Command.oCmdScript.Clear();//clear script
            //create script
            createScript();
            // run script
            btnScriptRun_Click(btnScriptRun, e); 
        }

        private void initEFEMConfig()
        {
            EFEM.isR1Arm1Enable = cbR1Arm1.Checked;
            EFEM.isR1Arm2Enable = cbR1Arm2.Checked;
            EFEM.isR1Arm3Enable = cbR1Arm3.Checked;
            EFEM.isAlign1Enable = cbA1.Checked;
            EFEM.isAlign2Enable = cbA2.Checked;
            EFEM.isAlign1Empty = cbA1.Checked;
            EFEM.isAlign2Empty = cbA2.Checked;
            EFEM.isLLAEnable = cbLLA.Checked;
            EFEM.isLLBEnable = cbLLB.Checked;
            EFEM.isLLCEnable = cbLLC.Checked;
            EFEM.isLLDEnable = cbLLD.Checked;
            EFEM.isLLAEmpty = cbLLA.Checked;
            EFEM.isLLBEmpty = cbLLB.Checked;
            EFEM.isLLCEmpty = cbLLC.Checked;
            EFEM.isLLDEmpty = cbLLD.Checked;
        }

        private void createScript()
        {
            // arm use case
            string armCase = (cbR1Arm1.Checked ? "1" : "0") + (cbR1Arm2.Checked ? "1" : "0") + (cbR1Arm3.Checked ? "1" : "0");
            foreach (ComboBox t_port in t_cbs)
            {
                // No destination, no processing
                if (t_port.Text.Equals(""))
                    continue;
                // No arms, no processing
                if (armCase.Equals("000"))
                    continue;

                //--------------------  Process Port Start --------------------  //
                string port_id = t_port.Name.Substring(2, 2);
                Label[] wafers = pMap[t_port.Name.Substring(2, 2)];

                int s_idx = 0;
                int idx1 = 0;//first wafer index
                int idx2 = 0;//second wafer index
                while (s_idx < wafers.Length && s_idx >= 0)
                {
                    
                    Label wafer_1st = null;
                    Label wafer_2nd = null;
                    idx1 = getNextWaferIdx(wafers, s_idx);
                    if (idx1 == -1)
                        break; // No wafer, no processing,  exit while
                    else
                    {
                        wafer_1st = wafers[idx1];
                        idx2 = getNextWaferIdx(wafers, idx1+1);//find next wafer
                        if (idx2 != -1)
                        {
                            wafer_2nd = wafers[idx2];
                        }
                    }

                    switch (armCase)
                    {
                        case "100":
                        case "010":
                            //----------  Process 1st Wafer Start----------  //
                            s_idx = processSingleArm(wafer_1st, port_id, idx1);
                            break;
                            //----------  Process 1st Wafer End ----------  //
                        case "110":
                            //單取滿兩片後結束
                            if(wafer_2nd == null)
                                s_idx = processSingleArm(wafer_1st, port_id, idx1);
                            else
                                s_idx = process2SingleArm(wafer_1st, wafer_2nd, port_id, idx1, idx2);                            
                            break;
                        case "111":
                            if (wafer_2nd == null)
                                s_idx = processSingleArm(wafer_1st, port_id, idx1);
                            else if(idx2 != idx1 + 1)
                                s_idx = process2SingleArm(wafer_1st, wafer_2nd, port_id, idx1, idx2);
                            else
                                s_idx = processDoubleArm(wafer_1st, wafer_2nd, port_id, idx1, idx2);
                            break;
                    }    
                }
                //--------------------  Process Port End  --------------------  //
            }
            refreshScriptSet();
        }

        private int processDoubleArm(Label wafer_1st, Label wafer_2nd, string port_id, int idx1, int idx2)
        {
            string slot_no1 = (idx1 + 1).ToString().PadLeft(2, '0');
            string wafer_id1 = port_id + "_W" + slot_no1;
            EFEM.UpdateWaferInfo(port_id + slot_no1, wafer_id1);
            //GetWafer(wafer_id1);//from port

            string slot_no2 = (idx2 + 1).ToString().PadLeft(2, '0');
            string wafer_id2 = port_id + "_W" + slot_no2;
            EFEM.UpdateWaferInfo(port_id + slot_no2, wafer_id2);
            GetWafer2Arm(wafer_id1, wafer_id2);//from port

            if (cbA1.Checked || cbA2.Checked)
            {
                PutAlign(wafer_id2);
                if (EFEM.isAlign1Empty || EFEM.isAlign2Empty)
                {
                    PutAlign(wafer_id1);
                    GetWafer(wafer_id2);
                    GetWafer(wafer_id1);
                }
                else
                {
                    GetWafer(wafer_id2);
                    PutAlign(wafer_id1);
                    GetWafer(wafer_id1);
                }
            }
            if (cbLLA.Checked || cbLLB.Checked || cbLLC.Checked || cbLLD.Checked)
            {
                PutLoadLock(wafer_id2);
                if (EFEM.isLLAEmpty || EFEM.isLLBEmpty || EFEM.isLLCEmpty || EFEM.isLLDEmpty)
                {
                    PutLoadLock(wafer_id1);
                    GetWafer(wafer_id2);
                    GetWafer(wafer_id1);
                }
                else
                {
                    GetWafer(wafer_id2);
                    PutLoadLock(wafer_id1);
                    GetWafer(wafer_id1);
                }
            }
            PutWafer2Arm(wafer_2nd, wafer_id2, wafer_1st,  wafer_id1 );
            return idx2 + 1; // set next start index for scan wafer
        }

        private int process2SingleArm(Label wafer_1st, Label wafer_2nd, string port_id, int idx1, int idx2)
        {
            string slot_no1 = (idx1 + 1).ToString().PadLeft(2, '0');
            string wafer_id1 = port_id + "_W" + slot_no1;
            EFEM.UpdateWaferInfo(port_id + slot_no1, wafer_id1);
            GetWafer(wafer_id1);//from port

            string slot_no2 = (idx2 + 1).ToString().PadLeft(2, '0');
            string wafer_id2 = port_id + "_W" + slot_no2;
            EFEM.UpdateWaferInfo(port_id + slot_no2, wafer_id2);
            GetWafer(wafer_id2);//from port

            if (cbA1.Checked || cbA2.Checked)
            {
                PutAlign(wafer_id1);
                if(EFEM.isAlign1Empty || EFEM.isAlign2Empty){
                    PutAlign(wafer_id2);
                    GetWafer(wafer_id1);
                    GetWafer(wafer_id2);
                }
                else
                {
                    GetWafer(wafer_id1);
                    PutAlign(wafer_id2);
                    GetWafer(wafer_id2);
                }
            }
            if (cbLLA.Checked || cbLLB.Checked || cbLLC.Checked || cbLLD.Checked)
            {
                PutLoadLock(wafer_id1);
                if (EFEM.isLLAEmpty || EFEM.isLLBEmpty || EFEM.isLLCEmpty || EFEM.isLLDEmpty )
                {
                    PutLoadLock(wafer_id2);
                    GetWafer(wafer_id1);
                    GetWafer(wafer_id2);
                }
                else
                {
                    GetWafer(wafer_id1);
                    PutLoadLock(wafer_id2);
                    GetWafer(wafer_id2);
                }
            }
            PutWafer(wafer_1st.Text, wafer_id1); // PutWafer to unload port
            PutWafer(wafer_2nd.Text, wafer_id2); // PutWafer to unload port
            return idx2 + 1; // set next start index for scan wafer
        }

        private int processSingleArm(Label wafer_1st, string port_id, int idx1)
        {
            string slot_no1 = (idx1 + 1).ToString().PadLeft(2, '0');
            string wafer_id1 = port_id + "_W" + slot_no1;
            EFEM.UpdateWaferInfo(port_id + slot_no1, wafer_id1);
            GetWafer(wafer_id1);//from port
            if (cbA1.Checked || cbA2.Checked)
            {
                PutAlign(wafer_id1);//PutWafer2Align
                GetWafer(wafer_id1);//GetWafer from aligner
            }
            if (cbLLA.Checked || cbLLB.Checked || cbLLC.Checked || cbLLD.Checked)
            {
                PutLoadLock(wafer_id1);//PutWafer2LoadLock
                GetWafer(wafer_id1);//GetWafer from load lock
            }
            PutWafer(wafer_1st.Text, wafer_id1); // PutWafer to unload port
            return idx1 + 1; // set next start index for scan wafer
        }

        public int getNextWaferIdx(Label[] wafers, int s_index)
        {
            int result = -1;
            try
            {
                for(int i = s_index; i< wafers.Length; i++)
                {
                    if (!wafers[i].Text.Equals(""))
                    {
                        result = i;
                        break;//find wafer , exit for
                    }                        
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public void GetWafer2Arm(string wafer_id1, string wafer_id2)
        {
            string source = EFEM.wafers[wafer_id2];
            if (source.Equals(""))
                return;
            string arm = "ARM3";
            Command.Load(source, arm);
            EFEM.UpdateWaferInfo("R1A2", wafer_id1);
            EFEM.UpdateWaferInfo("R1A1", wafer_id2);
        }

        public void GetWafer(string wafer_id)
        {
            string source = EFEM.wafers[wafer_id];
            if (source.Equals(""))
                return;
            string arm = "";
            string location = "";
            if (EFEM.isR1Arm1Enable && EFEM.isR1Arm1Empty)
            {
                arm = "ARM1";
                location = "R1A1";
            }
            else if (EFEM.isR1Arm2Enable && EFEM.isR1Arm2Empty)
            {
                arm = "ARM2";
                location = "R1A2";
            }                
            else
                return; // do nothing
            if (source.StartsWith("ALIGN"))
                Command.Clamp(source, "OFF");
            Command.Load(source, arm);
            if(source.StartsWith("ALIGN"))
                Command.Home(source);
            EFEM.UpdateWaferInfo(location, wafer_id);
        }
        
        public void PutWafer(string destination, string wafer_id)
        {
            string source = EFEM.wafers[wafer_id];
            if (source.Equals(""))
                return;
            string arm = source.EndsWith("A1") ? "ARM1" : "ARM2";
            Command.Unload(destination, arm);
            EFEM.UpdateWaferInfo(destination, wafer_id);
        }

        public void PutWafer2Arm(Label waferUp, string wafer_id_up, Label waferDown, string wafer_id_down)
        {
            Command.Unload(waferUp.Text, "ARM3");
            EFEM.UpdateWaferInfo(waferUp.Text, wafer_id_up);
            EFEM.UpdateWaferInfo(waferDown.Text, wafer_id_down);
        }

        public void PutAlign(string wafer_id)
        {
            string source = EFEM.wafers[wafer_id];
            if (source.Equals(""))
                return;
            string arm = source.EndsWith("A1")? "ARM1" : "ARM2";
            string destination = "";
            if (EFEM.isAlign1Enable && EFEM.isAlign1Empty)
            {
                destination = "ALIGN1";
            }
            else if (EFEM.isAlign2Enable && EFEM.isAlign2Empty)
            {
                destination = "ALIGN2";
            }
            else
                return; // do nothing
            // clamp before put
            if (destination.StartsWith("ALIGN"))
                Command.Clamp(destination, "ON");
            Command.Unload(destination, arm);
            // clamp before align
            //if (source.StartsWith("ALIGN"))
            //   Command.Clamp(source, "ON");
            Command.Align(destination, "120000");//120 angle
            EFEM.UpdateWaferInfo(destination, wafer_id);
        }
        public void PutLoadLock(string wafer_id)
        {
            string source = EFEM.wafers[wafer_id];
            if (source.Equals(""))
                return;
            string arm = source.EndsWith("A1") ? "ARM1" : "ARM2";
            string destination = "";
            if (EFEM.isLLAEnable && EFEM.isLLAEmpty)
            {
                destination = "LLA01";
            }
            else if (EFEM.isLLBEnable && EFEM.isLLBEmpty)
            {
                destination = "LLB01";
            }
            else if (EFEM.isLLCEnable && EFEM.isLLCEmpty)
            {
                destination = "LLC01";
            }
            else if (EFEM.isLLDEnable && EFEM.isLLDEmpty)
            {
                destination = "LLD01";
            }
            else
                return; // do nothing
            Command.Unload(destination, arm);
            EFEM.UpdateWaferInfo(destination, wafer_id);
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
                CmdScript cmd = Command.oCmdScript.ElementAt(dgvCmdScript.CurrentCell.RowIndex);
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

        private void sendManualCmd(object sender, EventArgs e)
        {
            string port = cbport_ml.SelectedItem.ToString();
            string arm = rdbArm1_mr.Checked? "ARM1":"ARM2";
            string robot = cbRobot_mr.SelectedItem.ToString();
            string aligner = cbAlign_ma.SelectedItem.ToString();

            switch (((Button)sender).Name)
            {
                //****************** load port ******************
                case "btnMap_ml"://load port mapping
                    sendCommand("MOV:WAFSH/" + port + ";");
                    break;
                case "btnLoad_ml"://load port load
                    sendCommand("MOV:OPEN/" + cbport_ml.SelectedItem + ";");
                    break;
                case "btnUnload_ml"://load port unload
                    sendCommand("MOV:UNDOCK/" + cbport_ml.SelectedItem + ";");
                    break;
                case "btnInit_ml"://load port init
                    sendCommand("MOV:INIT/" + cbport_ml.SelectedItem + ";");
                    break;

                //****************** robot ******************
                case "btnOrg_mr"://robot org
                    sendCommand("MOV:ORGSH/" + cbRobot_mr.SelectedItem + ";");
                    break;
                case "btnClamp_mr"://robot clamp on
                    sendCommand("SET:CLAMP/" + arm + "/ON;");
                    break;
                case "btnUnClamp_mr"://robot clamp off
                    sendCommand("SET:CLAMP/" + arm + "/OFF;");
                    break;
                case "btnInit_mr"://robot init
                    sendCommand("MOV:INIT/" + robot + ";");
                    break;

                //****************** aligner ******************
                case "btnInit_ma":// aligner init
                    sendCommand("MOV:INIT/" + aligner + ";");
                    break;
                case "btnOrg_ma":// aligner ORG
                    sendCommand("MOV:ORGSH/" + aligner + ";");
                    break;
                case "btnHome_ma":// aligner init
                    sendCommand("MOV:HOME/" + aligner + ";");
                    break;
                case "btnUnClamp_ma":// aligner init
                    sendCommand("SET:CLAMP/" + aligner + "/OFF;");
                    break;
                case "btnClamp_ma":// aligner init
                    sendCommand("SET:CLAMP/" + aligner + "/ON;");
                    break;
            }
        }
        
        private void cbR1Arm3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbR1Arm3.Checked)
            {
                cbR1Arm1.Checked = true;
                cbR1Arm2.Checked = true;
            }
        }
        
        private void cbR2Arm3_Click(object sender, EventArgs e)
        {
            if (cbR2Arm3.Checked)
            {
                cbR2Arm1.Checked = true;
                cbR2Arm2.Checked = true;
            }
        }
        
        private void cbR1Arm1_Click(object sender, EventArgs e)
        {
            if (!cbR1Arm1.Checked || !cbR1Arm2.Checked)
                cbR1Arm3.Checked = false;
        }

        private void cbR1Arm2_Click(object sender, EventArgs e)
        {
            if (!cbR1Arm1.Checked || !cbR1Arm2.Checked)
                cbR1Arm3.Checked = false;
        }

        private void cbR2Arm1_Click(object sender, EventArgs e)
        {
            if (!cbR2Arm1.Checked || !cbR2Arm2.Checked)
                cbR2Arm3.Checked = false;
        }

        private void cbR2Arm2_Click(object sender, EventArgs e)
        {
            if (!cbR2Arm1.Checked || !cbR2Arm2.Checked)
                cbR2Arm3.Checked = false;
        }

        private void cbR2Arm3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbR2Arm3.Checked)
            {
                cbR2Arm1.Checked = true;
                cbR2Arm2.Checked = true;
            }
        }
        
        private void cbErrMsg_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrMsg msg = define.getErrMsgInfo(cbErrMsg.Text);
            if (msg.isNeedMaintain)
                rbNeedPMYes.Checked = true;
            else
                rbNeedPMNo.Checked = true;
            tbErrDesc.Text = msg.desc;
            tbRecoverMethod.Text = msg.recover_method.ToString();
        }

        private void cbErrPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrPosition pos = define.getErrPosInfo(cbErrPos.Text);
            tbErrPosDecs.Text = pos.desc;
        }


        private void runCommands(object data)
        {
            string[] cmds = (string[]) data;
            FormMainUpdate.LogUpdate("\n**************  Commnads Start  ****************");
            int i = 1;
            foreach (string cmd in cmds)
            {
                isCmdFin = false;
                currentCmd = cmd.Replace("MOV", "").Replace("SET", "").Replace("GET", "");
                FormMainUpdate.LogUpdate("**************  Commnad " + i +" Start  ***********");
                sendCommand(cmd);
                SpinWait.SpinUntil(() => isCmdFin, intCmdTimeOut);// wait for command complete 
                FormMainUpdate.LogUpdate("**************  Commnad " + i + " End  ************");
                i++;
            }
            FormMainUpdate.LogUpdate("**************  Commnads End  ****************\n");
        }

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            System.Threading.WaitCallback thread = new WaitCallback(runCommands);
            switch (tbRecoverMethod.Text.Trim())
            {
                case "":
                case "0":
                    MessageBox.Show("No Error");
                    break;
                case "1":
                    //MessageBox.Show("Reset Error:Start");
                    ThreadPool.QueueUserWorkItem(thread, new string[] { "SET:ERROR/CLEAR;" });
                    MessageBox.Show("Reset Error:End");
                    break;
                case "2":
                    //MessageBox.Show("Reset Error => Init => Origin search: Start");
                    ThreadPool.QueueUserWorkItem(thread, new string[] { "SET:ERROR/CLEAR;", "MOV:INIT/ALL;", "MOV:ORGSH/ALL;" });
                    MessageBox.Show("Reset Error => Init => Origin search: End");
                    break;
                case "3":
                    MessageBox.Show("Turn on the power again or reboot system program.");
                    break;
            }

            #region kuma dummy code
            //dummy data
            string inf = "INF:ERROR/NOTHING/;";
            string error = inf.Replace("INF:ERROR/", "").Replace(";", "");

            //check track string format
            EFEM.Error = (error.Replace("/", "").Length == error.Length - 1) ? error.Split('/') : new string[2];

            //update GUI
            cbErrMsg.SelectedIndex = -1;
            cbErrMsg.SelectedItem = EFEM.Error[0];
            cbErrPos.SelectedIndex = -1;
            cbErrPos.SelectedItem = EFEM.Error[1];
            #endregion

        }

        private void rbEvtAllOn_Click(object sender, EventArgs e)
        {
            this.rbEvtFfuOn.Checked = true;
            this.rbEvtSysOn.Checked = true;
            this.rbEvtMapdtOn.Checked = true;
            this.rbEvtTransOn.Checked = true;
            this.rbEvtPrsOn.Checked = true;
            this.rbEvtPortOn.Checked = true;
            this.sendCommand("SET:EVENT/ALL/ON;");
        }

        private void rbEvtAllOff_Click(object sender, EventArgs e)
        {
            this.rbEvtFfuOff.Checked = true;
            this.rbEvtSysOff.Checked = true;
            this.rbEvtMapdtOff.Checked = true;
            this.rbEvtTransOff.Checked = true;
            this.rbEvtPrsOff.Checked = true;
            this.rbEvtPortOff.Checked = true;
            this.sendCommand("SET:EVENT/ALL/OFF;");
        }

        private void rbEvt_Click(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            string type = "";
            string setting = "";
            // Get Event type
            if (btn.Name.StartsWith("rbEvtFfu"))
            {
                type = "FFU";
            }else if (btn.Name.StartsWith("rbEvtSys"))
            {
                type = "SYSTEM";
            }
            else if(btn.Name.StartsWith("rbEvtMapdt"))
            {
                type = "MAPDT";
            }
            else if (btn.Name.StartsWith("rbEvtTrans"))
            {
                type = "TRANSREQ";
            }
            else if (btn.Name.StartsWith("rbEvtPrs"))
            {
                type = "PRS";
            }
            else if (btn.Name.StartsWith("rbEvtPort"))
            {
                type = "PORT";
            }
            //Get Event Setting
            setting = btn.Text.Trim();
            if (setting.Equals("ON"))
                rbEvtAllOff.Checked = false;
            else if (setting.Equals("OFF"))
                rbEvtAllOn.Checked = false;
            string cmd = "SET:EVENT/" + type + "/"+ setting  + ";";
            this.sendCommand(cmd);
        }

        private void rbSignal_Click(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            string type = "";
            string setting = "";
            string device = "";
            // Get Event type
            if (btn.Name.StartsWith("rbSignalRed"))
            {
                device = "STOWER";
                type = "RED";
            }
            else if (btn.Name.StartsWith("rbSignalYellow"))
            {
                device = "STOWER";
                type = "YELLOW";
            }
            else if (btn.Name.StartsWith("rbSignalGreen"))
            {
                device = "STOWER";
                type = "GREEN";
            }
            else if (btn.Name.StartsWith("rbSignalBlue"))
            {
                device = "STOWER";
                type = "BLUE";
            }
            else if (btn.Name.StartsWith("rbSignalBuzzer1"))
            {
                device = "STOWER";
                type = "BUZZER1";
            }
            else if (btn.Name.StartsWith("rbSignalBuzzer2"))
            {
                device = "STOWER";
                type = "BUZZER2";
            }
            else if (btn.Name.StartsWith("rbSignalLoad"))
            {
                type = "LOAD";
            }
            else if (btn.Name.StartsWith("rbSignalUnLoad"))
            {
                type = "UNLOAD";
            }
            else if (btn.Name.StartsWith("rbSignalAccess"))
            {
                type = "OP ACCESS";
            }
            //Get port 
            if(btn.Name.StartsWith("rbSignalLoad") || btn.Name.StartsWith("rbSignalUnLoad") || btn.Name.StartsWith("rbSignalAccess"))
            {
                if(rbSignalP1.Checked)
                    device = "P1";
                else if (rbSignalP2.Checked)
                    device = "P2";
                else if (rbSignalP3.Checked)
                    device = "P3";
                else if (rbSignalP4.Checked)
                    device = "P4";
            }
            //Get Event Setting
            setting = btn.Text.Trim();
            string cmd = "SET:SIGOUT/" + device + "/" + type + "/" + setting + ";";
            this.sendCommand(cmd);
        }

        private void btnSetSize_Click(object sender, EventArgs e)
        {
            if (cbSizeTarget.SelectedIndex < 0)
            {
                cbSizeTarget.Focus();
                MessageBox.Show("Please designates a target first.");
                return;
            }
            else
            {
                //set target
                string device = "";
                device = cbSizeTarget.Text;

                //set size
                string size = "";
                if (rbSizeNone.Checked)
                    size = rbSizeNone.Text;
                else if (rbSizeNa.Checked)
                    size = rbSizeNa.Text;
                else if (rbSize200.Checked)
                    size = rbSize200.Text;
                else if (rbSize300.Checked)
                    size = rbSize300.Text;
                string cmd = "SET:SIZE/" + device + "/" + size + ";";
                this.sendCommand(cmd);
            }

        }

        private void rbE84_Click(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            string device = "";
            string setting = "";

            // Get device
            if (btn.Name.StartsWith("rbE84All"))
            {
                device = "ALL";
            }
            else if (btn.Name.StartsWith("rbE84P1"))
            {
                device = "P1";
            }
            else if (btn.Name.StartsWith("rbE84P2"))
            {
                device = "P2";
            }
            else if (btn.Name.StartsWith("rbE84P3"))
            {
                device = "P3";
            }
            else if (btn.Name.StartsWith("rbE84P4"))
            {
                device = "P4";
            }

            //Get Event Setting
            setting = btn.Text.Trim();
            if (!device.Equals("ALL") && setting.Equals("AUTO"))
                rbE84AllManual.Checked = false;
            else if (!device.Equals("ALL") && setting.Equals("MANUAL"))
                rbE84AllAuto.Checked = false;
            else if (btn.Name.Equals("rbE84AllAuto"))
            {
                rbE84P1Auto.Checked = true;
                rbE84P2Auto.Checked = true;
                rbE84P3Auto.Checked = true;
                rbE84P4Auto.Checked = true;
            }
            else if (btn.Name.Equals("rbE84AllManual"))
            {
                rbE84P1Manual.Checked = true;
                rbE84P2Manual.Checked = true;
                rbE84P3Manual.Checked = true;
                rbE84P4Manual.Checked = true;
            }

            string cmd = "SET:MODE/" + device + "/" + setting + ";";
            this.sendCommand(cmd);
        }

        private void btnStateRefresh_Click(object sender, EventArgs e)
        {
            //dummy data
            string version = "1.0.0.1(2018-01-01)";
            string track = "200/NONE/????";
            string prsn = "SNO1|STATUS1,SNO2|STATUS2,SNO3|STATUS3,SNO4|STATUS4,SNO5|STATUS5,SNO6|STATUS6,SNO7|STATUS7,SNO8|STATUS8";
            string ffun = "FNO1|STATUS1,FNO2|STATUS2,FNO3|STATUS3,FNO4|STATUS4,FNO5|STATUS5,FNO6|STATUS6,FNO7|STATUS7,FNO8|STATUS8,FNO9|STATUS9,FNO10|STATUS10," +
                          "FNO11|STATUS11,FNO12|STATUS12,FNO13|STATUS13,FNO14|STATUS14,FNO15|STATUS15,FNO16|STATUS16,FNO17|STATUS17,FNO18|STATUS18,FNO19|STATUS19,FNO20|STATUS20";
            //parse data
            EFEM.parseFFUn(ffun);
            EFEM.parsePRSn(prsn);
            EFEM.VER = version;
            //check track string format
            EFEM.TRACK = (track.Replace("/","").Length == track.Length - 2) ? track.Split('/'): new string[3];

            //update GUI
            this.tbStateVer.Text = EFEM.VER;

            this.tbStateTrack1.Text = EFEM.TRACK[0];
            this.tbStateTrack2.Text = EFEM.TRACK[1];
            this.tbStateTrack3.Text = EFEM.TRACK[2];

            this.tbStatePrs1.Text = EFEM.PRSn[0];
            this.tbStatePrs2.Text = EFEM.PRSn[1];
            this.tbStatePrs3.Text = EFEM.PRSn[2];
            this.tbStatePrs4.Text = EFEM.PRSn[3];
            this.tbStatePrs5.Text = EFEM.PRSn[4];
            this.tbStatePrs6.Text = EFEM.PRSn[5];
            this.tbStatePrs7.Text = EFEM.PRSn[6];
            this.tbStatePrs8.Text = EFEM.PRSn[7];

            this.tbStateFFU1.Text = EFEM.FFUn[0];
            this.tbStateFFU2.Text = EFEM.FFUn[1];
            this.tbStateFFU3.Text = EFEM.FFUn[2];
            this.tbStateFFU4.Text = EFEM.FFUn[3];
            this.tbStateFFU5.Text = EFEM.FFUn[4];
            this.tbStateFFU6.Text = EFEM.FFUn[5];
            this.tbStateFFU7.Text = EFEM.FFUn[6];
            this.tbStateFFU8.Text = EFEM.FFUn[7];
            this.tbStateFFU9.Text = EFEM.FFUn[8];
            this.tbStateFFU10.Text = EFEM.FFUn[9];
            this.tbStateFFU11.Text = EFEM.FFUn[10];
            this.tbStateFFU12.Text = EFEM.FFUn[11];
            this.tbStateFFU13.Text = EFEM.FFUn[12];
            this.tbStateFFU14.Text = EFEM.FFUn[13];
            this.tbStateFFU15.Text = EFEM.FFUn[14];
            this.tbStateFFU16.Text = EFEM.FFUn[15];
            this.tbStateFFU17.Text = EFEM.FFUn[16];
            this.tbStateFFU18.Text = EFEM.FFUn[17];
            this.tbStateFFU19.Text = EFEM.FFUn[18];
            this.tbStateFFU20.Text = EFEM.FFUn[19];

        }

        private void btnErrorRefresh_Click(object sender, EventArgs e)
        {
            //dummy data
            string inf = "INF:ERROR/CLAMPON/ROBOT1_ARM1;";
            string error = inf.Replace("INF:ERROR/", "").Replace(";","");

            //check track string format
            EFEM.Error = (error.Replace("/", "").Length == error.Length - 1) ? error.Split('/') : new string[2];

            //update GUI
            cbErrMsg.SelectedIndex = -1;
            cbErrMsg.SelectedItem = EFEM.Error[0];
            cbErrPos.SelectedIndex = -1;
            cbErrPos.SelectedItem = EFEM.Error[1];
        }

        private string getSignal(string type)
        {
            string result = "";

            switch (type)
            {
                case "sysDio":
                    result = "101010101010????????????????????"; //32 bit
                    break;
                case "sysLight":
                    result = "01011?0101?00????????????????????";//32 bit
                    break;
                case "portDio":
                    result = "0101????0101010?????????????????";//32 bit
                    break;
                case "portLight":
                    result = "1101?1101???????????????????????";//32 bit
                    break;
            }
            int random1 = new Random().Next(2) % 2;  // 測試用, 記得取消
            int random2 = random1 + 1 % 2 ;  // 測試用, 記得取消
            result = result.Replace("1", "K").Replace("0", random2.ToString()).Replace("K", random1.ToString());// 測試用, 記得取消
            return result;
        }

        private void btnSignalQuery_Click(object sender, EventArgs e)
        {
            //EQP_Emulator.Properties.Resources.on
            string sysDio    = getSignal("sysDio"); 
            string sysLight  = getSignal("sysLight");
            string portDio   = getSignal("portDio");
            string portLight = getSignal("portLight");

            char[] sysData1 = sysDio.ToCharArray();
            char[] sysData2 = sysLight.ToCharArray();
            char[] portData1 = portDio.ToCharArray();
            char[] portData2 = portLight.ToCharArray();

            Bitmap on = EQP_Emulator.Properties.Resources.on;
            Bitmap off = EQP_Emulator.Properties.Resources.off;
            Bitmap blink = EQP_Emulator.Properties.Resources.blink;
            Bitmap reserved = null;

            //sysrem data 1 : DIO
            pbSysDIO0.Image = sysData1[0].Equals('1') ? on : off;//System vacuum source pressure 1
            pbSysDIO1.Image = sysData1[1].Equals('1') ? on : off;//System vacuum source pressure 2
            pbSysDIO2.Image = sysData1[2].Equals('1') ? on : off;//System compressed air pressure 1
            pbSysDIO3.Image = sysData1[3].Equals('1') ? on : off;//System compressed air pressure 2
            pbSysDIO4.Image = sysData1[4].Equals('1') ? on : off;//Differential pressure sensor setting 1
            pbSysDIO5.Image = sysData1[5].Equals('1') ? on : off;//Differential pressure sensor setting 2
            pbSysDIO6.Image = sysData1[6].Equals('1') ? on : off;//FFU alarm 
            pbSysDIO7.Image = sysData1[7].Equals('1') ? on : off;//Ionizer alarm
            pbSysDIO8.Image = sysData1[8].Equals('1') ? on : off;//Mode switch 
            pbSysDIO9.Image = sysData1[9].Equals('1') ? on : off;//Drive power
            pbSysDIO10.Image = sysData1[10].Equals('1') ? on : off;//Door open/close status D
            pbSysDIO11.Image = sysData1[11].Equals('1') ? on : off;//Area sensor/ Bar interlock

            //sysrem data 2 : Signal tower
            pbSysLight0.Image = sysData2[0].Equals('1') ? on : off;//RED Lights
            pbSysLight1.Image = sysData2[1].Equals('1') ? on : off;//YELLOW Lights
            pbSysLight2.Image = sysData2[2].Equals('1') ? on : off;//GREEN Lights
            pbSysLight3.Image = sysData2[3].Equals('1') ? on : off;//BLUE Lights
            pbSysLight4.Image = reserved;//reserved
            pbSysLight5.Image = sysData2[5].Equals('1') ? blink : off;//RED Lights blink
            pbSysLight6.Image = sysData2[6].Equals('1') ? blink : off;//YELLOW Lights blink
            pbSysLight7.Image = sysData2[7].Equals('1') ? blink : off;//GREEN Lights blink
            pbSysLight8.Image = sysData2[8].Equals('1') ? blink : off;//BLUE Lights blink
            pbSysLight9.Image = reserved;//reserved
            pbSysLight10.Image = sysData2[10].Equals('1') ? on : off; ;//BUZZER1
            pbSysLight11.Image = sysData2[11].Equals('1') ? on : off; ;//BUZZER2

            //port data 1 : DIO
            pbPortDIO0.Image = portData1[0].Equals('1') ? on : off;//FOUP proper placement
            pbPortDIO1.Image = portData1[1].Equals('1') ? on : off;//FOUP presence
            pbPortDIO2.Image = portData1[2].Equals('1') ? on : off;//ACCESS SW 
            pbPortDIO3.Image = portData1[3].Equals('1') ? on : off;//FOUP Lock
            pbPortDIO4.Image = portData1[4].Equals('1') ? on : off;//Cover Close (reserved)
            pbPortDIO5.Image = portData1[5].Equals('1') ? on : off;//Cover Lock(reserved)
            pbPortDIO8.Image = portData1[8].Equals('1') ? on : off;//Info pad A
            pbPortDIO9.Image = portData1[9].Equals('1') ? on : off;//Info pad B
            pbPortDIO10.Image = portData1[10].Equals('1') ? on : off;//Info pad C
            pbPortDIO11.Image = portData1[11].Equals('1') ? on : off;//Info pad D
            pbPortDIO12.Image = portData1[12].Equals('1') ? on : off;//Adapter presence
            pbPortDIO13.Image = portData1[13].Equals('1') ? on : off;//Adapter cable connection status
            pbPortDIO14.Image = portData1[14].Equals('1') ? on : off;//Adapter valid

            //port data 2 : Port indicator light
            pbPortLight0.Image = portData2[0].Equals('1') ? on : off;//PRESENCE
            pbPortLight1.Image = portData2[1].Equals('1') ? on : off;//PLACEMENT
            pbPortLight2.Image = portData2[2].Equals('1') ? on : off;//LOAD
            pbPortLight3.Image = portData2[3].Equals('1') ? on : off;//UNLOAD
            pbPortLight4.Image = reserved;//reserved (MANUAL MODE )
            pbPortLight5.Image = portData2[5].Equals('1') ? on : off;//ERROR
            pbPortLight6.Image = portData2[6].Equals('1') ? on : off;//CLAMP/UNCLAMP
            pbPortLight7.Image = portData2[7].Equals('1') ? on : off;//DOCK/UNDOCK
            pbPortLight8.Image = portData2[8].Equals('1') ? on : off;//ACCESS SW
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            btnHold.Visible = false;
            btnAbort.Visible = true;
            btnRestart.Visible = true;
            isPause = true;
            sendCommand("MOV:HOLD;");//send pause command to efem
        }
        private void btnAbort_Click(object sender, EventArgs e)
        {
            btnHold.Visible = true;
            btnAbort.Visible = false;
            btnRestart.Visible = false;
            sendCommand("MOV:ABORT;");//send abort command to efem
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnHold.Visible = true;
            btnAbort.Visible = false;
            btnRestart.Visible = false;
            sendCommand("MOV:RESTR;");//send abort command to efem
        }

        private void btnInitAll_Click(object sender, EventArgs e)
        {
            sendCommand("MOV:INIT/ALL;");
        }

        private void showStatus_MouseEnter(object sender, EventArgs e)
        {
            string hint = this.hint.GetToolTip((Control)sender);
            slStatus.Text = hint;
            slStatus.BackColor = SystemColors.Info;
        }

        private void clearStatus_MouseLeave(object sender, EventArgs e)
        {
            slStatus.Text = "";
            slStatus.BackColor = SystemColors.Control;
        }
    }
}
