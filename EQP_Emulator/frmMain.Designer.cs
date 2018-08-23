namespace EQP_Emulator
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbHostIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_ConnectState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.btnDisConn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAddScript = new System.Windows.Forms.Button();
            this.cbPara4 = new System.Windows.Forms.ComboBox();
            this.cbPara3 = new System.Windows.Forms.ComboBox();
            this.cbPara2 = new System.Windows.Forms.ComboBox();
            this.cbPara1 = new System.Windows.Forms.ComboBox();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.cbCmd = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCmdType = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.btnClearMsg = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCmd = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnStepRun = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTimes = new System.Windows.Forms.TextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnScriptStop = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.dgvCmdScript = new System.Windows.Forms.DataGridView();
            this.btnScriptRun = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbl_alarm = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCmd.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmdScript)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host IP";
            // 
            // tbHostIP
            // 
            this.tbHostIP.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHostIP.Location = new System.Drawing.Point(106, 14);
            this.tbHostIP.Name = "tbHostIP";
            this.tbHostIP.Size = new System.Drawing.Size(197, 32);
            this.tbHostIP.TabIndex = 1;
            this.tbHostIP.Text = "127.0.0.1";
            this.tbHostIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPort
            // 
            this.tbPort.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPort.Location = new System.Drawing.Point(374, 14);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(73, 32);
            this.tbPort.TabIndex = 3;
            this.tbPort.Text = "13000";
            this.tbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(309, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.lbl_alarm);
            this.GroupBox3.Controls.Add(this.GroupBox4);
            this.GroupBox3.Controls.Add(this.lbl_ConnectState);
            this.GroupBox3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox3.Location = new System.Drawing.Point(703, 13);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(232, 56);
            this.GroupBox3.TabIndex = 13;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "EFEM Connection State";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.label4);
            this.GroupBox4.Location = new System.Drawing.Point(8, 56);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(192, 56);
            this.GroupBox4.TabIndex = 5;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Quick GEM init. Result";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(8, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "Result";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ConnectState
            // 
            this.lbl_ConnectState.BackColor = System.Drawing.Color.Red;
            this.lbl_ConnectState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ConnectState.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_ConnectState.Location = new System.Drawing.Point(8, 18);
            this.lbl_ConnectState.Name = "lbl_ConnectState";
            this.lbl_ConnectState.Size = new System.Drawing.Size(109, 30);
            this.lbl_ConnectState.TabIndex = 0;
            this.lbl_ConnectState.Text = "Disconnection";
            this.lbl_ConnectState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.GroupBox3);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(944, 79);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect Area";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnConn);
            this.groupBox2.Controls.Add(this.tbPort);
            this.groupBox2.Controls.Add(this.btnDisConn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbHostIP);
            this.groupBox2.Location = new System.Drawing.Point(6, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 52);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(463, 14);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(108, 32);
            this.btnConn.TabIndex = 15;
            this.btnConn.Text = "Connect";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // btnDisConn
            // 
            this.btnDisConn.Enabled = false;
            this.btnDisConn.Location = new System.Drawing.Point(577, 14);
            this.btnDisConn.Name = "btnDisConn";
            this.btnDisConn.Size = new System.Drawing.Size(108, 32);
            this.btnDisConn.TabIndex = 16;
            this.btnDisConn.Text = "Disconnect";
            this.btnDisConn.UseVisualStyleBackColor = true;
            this.btnDisConn.Click += new System.EventHandler(this.btnDisConn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnReset);
            this.groupBox5.Controls.Add(this.btnAddScript);
            this.groupBox5.Controls.Add(this.cbPara4);
            this.groupBox5.Controls.Add(this.cbPara3);
            this.groupBox5.Controls.Add(this.cbPara2);
            this.groupBox5.Controls.Add(this.cbPara1);
            this.groupBox5.Controls.Add(this.tbCmd);
            this.groupBox5.Controls.Add(this.cbCmd);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.cbCmdType);
            this.groupBox5.Controls.Add(this.btnSend);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(944, 125);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Command Area";
            // 
            // btnAddScript
            // 
            this.btnAddScript.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddScript.Location = new System.Drawing.Point(634, 73);
            this.btnAddScript.Name = "btnAddScript";
            this.btnAddScript.Size = new System.Drawing.Size(106, 39);
            this.btnAddScript.TabIndex = 28;
            this.btnAddScript.Text = "Add To Script";
            this.btnAddScript.UseVisualStyleBackColor = true;
            this.btnAddScript.Click += new System.EventHandler(this.btnAddScript_Click);
            // 
            // cbPara4
            // 
            this.cbPara4.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPara4.FormattingEnabled = true;
            this.cbPara4.Location = new System.Drawing.Point(795, 26);
            this.cbPara4.Name = "cbPara4";
            this.cbPara4.Size = new System.Drawing.Size(131, 36);
            this.cbPara4.TabIndex = 27;
            this.cbPara4.SelectedIndexChanged += new System.EventHandler(this.createCommand);
            this.cbPara4.TextUpdate += new System.EventHandler(this.createCommand);
            // 
            // cbPara3
            // 
            this.cbPara3.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPara3.FormattingEnabled = true;
            this.cbPara3.Location = new System.Drawing.Point(658, 26);
            this.cbPara3.Name = "cbPara3";
            this.cbPara3.Size = new System.Drawing.Size(131, 36);
            this.cbPara3.TabIndex = 26;
            this.cbPara3.SelectedIndexChanged += new System.EventHandler(this.createCommand);
            this.cbPara3.TextUpdate += new System.EventHandler(this.createCommand);
            // 
            // cbPara2
            // 
            this.cbPara2.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPara2.FormattingEnabled = true;
            this.cbPara2.Location = new System.Drawing.Point(521, 26);
            this.cbPara2.Name = "cbPara2";
            this.cbPara2.Size = new System.Drawing.Size(131, 36);
            this.cbPara2.TabIndex = 25;
            this.cbPara2.SelectedIndexChanged += new System.EventHandler(this.createCommand);
            this.cbPara2.TextUpdate += new System.EventHandler(this.createCommand);
            // 
            // cbPara1
            // 
            this.cbPara1.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPara1.FormattingEnabled = true;
            this.cbPara1.Location = new System.Drawing.Point(385, 26);
            this.cbPara1.Name = "cbPara1";
            this.cbPara1.Size = new System.Drawing.Size(131, 36);
            this.cbPara1.TabIndex = 24;
            this.cbPara1.SelectedIndexChanged += new System.EventHandler(this.createCommand);
            this.cbPara1.TextUpdate += new System.EventHandler(this.createCommand);
            // 
            // tbCmd
            // 
            this.tbCmd.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCmd.Location = new System.Drawing.Point(10, 76);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(506, 36);
            this.tbCmd.TabIndex = 23;
            // 
            // cbCmd
            // 
            this.cbCmd.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCmd.FormattingEnabled = true;
            this.cbCmd.Location = new System.Drawing.Point(216, 26);
            this.cbCmd.Name = "cbCmd";
            this.cbCmd.Size = new System.Drawing.Size(163, 36);
            this.cbCmd.TabIndex = 22;
            this.cbCmd.SelectedIndexChanged += new System.EventHandler(this.cbCmd_SelectedIndexChanged);
            this.cbCmd.TextUpdate += new System.EventHandler(this.createCommand);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(130, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 23);
            this.label5.TabIndex = 21;
            this.label5.Text = "Command";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "Type";
            // 
            // cbCmdType
            // 
            this.cbCmdType.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCmdType.FormattingEnabled = true;
            this.cbCmdType.Location = new System.Drawing.Point(56, 26);
            this.cbCmdType.Name = "cbCmdType";
            this.cbCmdType.Size = new System.Drawing.Size(68, 36);
            this.cbCmdType.TabIndex = 20;
            this.cbCmdType.SelectedIndexChanged += new System.EventHandler(this.cbCmdType_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(522, 73);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(106, 39);
            this.btnSend.TabIndex = 19;
            this.btnSend.Text = "Send Command";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblStatus);
            this.groupBox6.Controls.Add(this.rtbMsg);
            this.groupBox6.Controls.Add(this.btnClearMsg);
            this.groupBox6.Location = new System.Drawing.Point(6, 137);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(479, 464);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Message Area";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(10, 424);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 23);
            this.lblStatus.TabIndex = 19;
            // 
            // rtbMsg
            // 
            this.rtbMsg.Location = new System.Drawing.Point(6, 21);
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMsg.Size = new System.Drawing.Size(461, 391);
            this.rtbMsg.TabIndex = 0;
            this.rtbMsg.Text = "";
            // 
            // btnClearMsg
            // 
            this.btnClearMsg.Location = new System.Drawing.Point(359, 424);
            this.btnClearMsg.Name = "btnClearMsg";
            this.btnClearMsg.Size = new System.Drawing.Size(108, 32);
            this.btnClearMsg.TabIndex = 18;
            this.btnClearMsg.Text = "Clear Message";
            this.btnClearMsg.UseVisualStyleBackColor = true;
            this.btnClearMsg.Click += new System.EventHandler(this.btnClearMsg_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCmd);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(973, 633);
            this.tabControl1.TabIndex = 20;
            // 
            // tabCmd
            // 
            this.tabCmd.BackColor = System.Drawing.SystemColors.Control;
            this.tabCmd.Controls.Add(this.groupBox7);
            this.tabCmd.Controls.Add(this.groupBox5);
            this.tabCmd.Controls.Add(this.groupBox6);
            this.tabCmd.Location = new System.Drawing.Point(4, 22);
            this.tabCmd.Name = "tabCmd";
            this.tabCmd.Padding = new System.Windows.Forms.Padding(3);
            this.tabCmd.Size = new System.Drawing.Size(965, 607);
            this.tabCmd.TabIndex = 0;
            this.tabCmd.Text = "Command Mode";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnStepRun);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.tbTimes);
            this.groupBox7.Controls.Add(this.btnDel);
            this.groupBox7.Controls.Add(this.btnDown);
            this.groupBox7.Controls.Add(this.btnScriptStop);
            this.groupBox7.Controls.Add(this.btnUp);
            this.groupBox7.Controls.Add(this.dgvCmdScript);
            this.groupBox7.Controls.Add(this.btnScriptRun);
            this.groupBox7.Controls.Add(this.btnExport);
            this.groupBox7.Controls.Add(this.btnImport);
            this.groupBox7.Location = new System.Drawing.Point(491, 137);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(459, 464);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Script Area";
            // 
            // btnStepRun
            // 
            this.btnStepRun.Location = new System.Drawing.Point(413, 174);
            this.btnStepRun.Name = "btnStepRun";
            this.btnStepRun.Size = new System.Drawing.Size(38, 32);
            this.btnStepRun.TabIndex = 30;
            this.btnStepRun.Text = "Step";
            this.btnStepRun.UseVisualStyleBackColor = true;
            this.btnStepRun.Click += new System.EventHandler(this.btnStepRun_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(141, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 23);
            this.label6.TabIndex = 29;
            this.label6.Text = "repeat times";
            // 
            // tbTimes
            // 
            this.tbTimes.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTimes.Location = new System.Drawing.Point(251, 24);
            this.tbTimes.Name = "tbTimes";
            this.tbTimes.Size = new System.Drawing.Size(74, 30);
            this.tbTimes.TabIndex = 26;
            this.tbTimes.Text = "1";
            this.tbTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTimes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTimes_KeyPress);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(413, 217);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(38, 32);
            this.btnDel.TabIndex = 25;
            this.btnDel.Text = "－";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(413, 260);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(38, 32);
            this.btnDown.TabIndex = 24;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnScriptStop
            // 
            this.btnScriptStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnScriptStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnScriptStop.Location = new System.Drawing.Point(395, 22);
            this.btnScriptStop.Name = "btnScriptStop";
            this.btnScriptStop.Size = new System.Drawing.Size(58, 32);
            this.btnScriptStop.TabIndex = 21;
            this.btnScriptStop.Text = "STOP";
            this.btnScriptStop.UseVisualStyleBackColor = false;
            this.btnScriptStop.Click += new System.EventHandler(this.btnScriptStop_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(413, 131);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(38, 32);
            this.btnUp.TabIndex = 23;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // dgvCmdScript
            // 
            this.dgvCmdScript.AllowUserToAddRows = false;
            this.dgvCmdScript.AllowUserToDeleteRows = false;
            this.dgvCmdScript.AllowUserToResizeColumns = false;
            this.dgvCmdScript.AllowUserToResizeRows = false;
            this.dgvCmdScript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCmdScript.Location = new System.Drawing.Point(8, 60);
            this.dgvCmdScript.Name = "dgvCmdScript";
            this.dgvCmdScript.ReadOnly = true;
            this.dgvCmdScript.RowTemplate.Height = 24;
            this.dgvCmdScript.Size = new System.Drawing.Size(399, 396);
            this.dgvCmdScript.TabIndex = 22;
            // 
            // btnScriptRun
            // 
            this.btnScriptRun.Location = new System.Drawing.Point(331, 22);
            this.btnScriptRun.Name = "btnScriptRun";
            this.btnScriptRun.Size = new System.Drawing.Size(58, 32);
            this.btnScriptRun.TabIndex = 20;
            this.btnScriptRun.Text = "RUN";
            this.btnScriptRun.UseVisualStyleBackColor = true;
            this.btnScriptRun.Click += new System.EventHandler(this.btnScriptRun_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(70, 22);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(58, 32);
            this.btnExport.TabIndex = 19;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(6, 22);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(58, 32);
            this.btnImport.TabIndex = 18;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(965, 607);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Running Mode";
            // 
            // lbl_alarm
            // 
            this.lbl_alarm.BackColor = System.Drawing.Color.LimeGreen;
            this.lbl_alarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_alarm.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_alarm.Location = new System.Drawing.Point(119, 18);
            this.lbl_alarm.Name = "lbl_alarm";
            this.lbl_alarm.Size = new System.Drawing.Size(107, 30);
            this.lbl_alarm.TabIndex = 6;
            this.lbl_alarm.Text = "Alarm clear";
            this.lbl_alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(820, 73);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(106, 39);
            this.btnReset.TabIndex = 29;
            this.btnReset.Text = "Reset Alarm";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 721);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "EQP Emulator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCmd.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmdScript)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbHostIP;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label lbl_ConnectState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnDisConn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnClearMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCmdType;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cbCmd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPara4;
        private System.Windows.Forms.ComboBox cbPara3;
        private System.Windows.Forms.ComboBox cbPara2;
        private System.Windows.Forms.ComboBox cbPara1;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.RichTextBox rtbMsg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCmd;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnScriptStop;
        private System.Windows.Forms.Button btnScriptRun;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dgvCmdScript;
        private System.Windows.Forms.Button btnAddScript;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTimes;
        private System.Windows.Forms.Button btnStepRun;
        private System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.Label lbl_alarm;
        private System.Windows.Forms.Button btnReset;
    }
}

