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

namespace EQP_Emulator
{
    public partial class frmMain : Form, IConnectionReport
    {
        SocketClient conn;
        CmdDefine define = new CmdDefine();
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
        }

        private void createCommand(object sender, EventArgs e)
        {
            string strCmd = cbCmdType.Text + ":" + cbCmd.Text;
            StringBuilder cmd = new StringBuilder(strCmd) ;
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

            if (define.cmdParams1.TryGetValue(strCmd,out p1Ary))
            {
                foreach (string element in p1Ary)
                {
                    cbPara1.Items.Add(element);
                }
                cbPara1.Visible = true;
                cbPara1.SelectedIndex = 0;
            }
            //else
            //{
            //    Console.WriteLine("cmdParams1 No such key: {0}", strCmd);
            //}

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
            //string[] p1Ary = define.cmdParams1[strCmd];
            //if (p1Ary != null)
            //{
            //    foreach (string element in p1Ary)
            //    {
            //        cbPara1.Items.Add(element);
            //    }
            //    cbPara1.Visible = false;
            //}

            //string[] p2Ary = define.cmdParams2[strCmd];
            //if (p2Ary != null)
            //{
            //    foreach (string element in p2Ary)
            //    {
            //        cbPara2.Items.Add(element);
            //    }
            //    cbPara2.Visible = false;
            //}

            //string[] p3Ary = define.cmdParams3[strCmd];
            //if (p3Ary != null)
            //{
            //    foreach (string element in p3Ary)
            //    {
            //        cbPara3.Items.Add(element);
            //    }
            //    cbPara3.Visible = false;
            //}

            //string[] p4Ary = define.cmdParams4[strCmd];
            //if (p4Ary != null)
            //{
            //    foreach (string element in p4Ary)
            //    {
            //        cbPara4.Items.Add(element);
            //    }
            //    cbPara4.Visible = false;
            //}
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
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            conn = new SocketClient(tbHostIP.Text, int.Parse(tbPort.Text), this);
            conn.Start();
        }

        void IConnectionReport.On_Connection_Message(object Msg)
        {
            FormMainUpdate.LogUpdate((string) Msg);
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
            //conn;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (conn == null)
                MessageBox.Show("Please connect first!!");
            else
            {
                conn.Send(tbCmd.Text + ";\r");
                rtbMsg.AppendText(tbCmd.Text + ";\\r\n");
            }
        }
    }
}
