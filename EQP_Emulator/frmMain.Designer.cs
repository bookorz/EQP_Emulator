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
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.tbHostIP.Text = "192.168.5.127";
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
            this.lbl_ConnectState.Size = new System.Drawing.Size(218, 30);
            this.lbl_ConnectState.TabIndex = 0;
            this.lbl_ConnectState.Text = "Disconnection";
            this.lbl_ConnectState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.GroupBox3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
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
            this.groupBox5.Location = new System.Drawing.Point(12, 102);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(944, 125);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Command Area";
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
            // 
            // tbCmd
            // 
            this.tbCmd.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCmd.Location = new System.Drawing.Point(10, 76);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(816, 36);
            this.tbCmd.TabIndex = 23;
            // 
            // cbCmd
            // 
            this.cbCmd.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCmd.FormattingEnabled = true;
            this.cbCmd.Location = new System.Drawing.Point(238, 26);
            this.cbCmd.Name = "cbCmd";
            this.cbCmd.Size = new System.Drawing.Size(141, 36);
            this.cbCmd.TabIndex = 22;
            this.cbCmd.SelectedIndexChanged += new System.EventHandler(this.cbCmd_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 28);
            this.label5.TabIndex = 21;
            this.label5.Text = "Command";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 28);
            this.label3.TabIndex = 17;
            this.label3.Text = "Type";
            // 
            // cbCmdType
            // 
            this.cbCmdType.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCmdType.FormattingEnabled = true;
            this.cbCmdType.Location = new System.Drawing.Point(70, 26);
            this.cbCmdType.Name = "cbCmdType";
            this.cbCmdType.Size = new System.Drawing.Size(68, 36);
            this.cbCmdType.TabIndex = 20;
            this.cbCmdType.SelectedIndexChanged += new System.EventHandler(this.cbCmdType_SelectedIndexChanged);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(836, 75);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(99, 39);
            this.btnSend.TabIndex = 19;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rtbMsg);
            this.groupBox6.Location = new System.Drawing.Point(12, 233);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(944, 418);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Message Area";
            // 
            // rtbMsg
            // 
            this.rtbMsg.Location = new System.Drawing.Point(6, 21);
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMsg.Size = new System.Drawing.Size(932, 391);
            this.rtbMsg.TabIndex = 0;
            this.rtbMsg.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(731, 667);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 32);
            this.button3.TabIndex = 18;
            this.button3.Text = "Clear Message";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(848, 667);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 32);
            this.button4.TabIndex = 19;
            this.button4.Text = "Export Message";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
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
    }
}

