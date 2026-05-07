namespace Lab4_Timp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listFiles = new ListBox();
            groupClient = new GroupBox();
            txtClientLog = new TextBox();
            groupServer = new GroupBox();
            txtServerLog = new TextBox();
            btnSendToServer = new Button();
            btnSendToClient = new Button();
            lblIp = new Label();
            txtIp = new TextBox();
            btnDisconnect = new Button();
            btnServerDisconnect = new Button();
            btnExit = new Button();
            cmbPath = new ComboBox();
            groupClient.SuspendLayout();
            groupServer.SuspendLayout();
            SuspendLayout();
            // 
            // listFiles
            // 
            listFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listFiles.FormattingEnabled = true;
            listFiles.ItemHeight = 15;
            listFiles.Location = new Point(10, 45);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(320, 454);
            listFiles.TabIndex = 1;
            listFiles.SelectedIndexChanged += listFiles_SelectedIndexChanged;
            // 
            // groupClient
            // 
            groupClient.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            groupClient.Controls.Add(txtClientLog);
            groupClient.Location = new Point(340, 10);
            groupClient.Name = "groupClient";
            groupClient.Size = new Size(300, 625);
            groupClient.TabIndex = 2;
            groupClient.TabStop = false;
            groupClient.Text = "Клиентская сторона";
            groupClient.Enter += groupClient_Enter;
            // 
            // txtClientLog
            // 
            txtClientLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtClientLog.Location = new Point(10, 20);
            txtClientLog.Multiline = true;
            txtClientLog.Name = "txtClientLog";
            txtClientLog.ReadOnly = true;
            txtClientLog.ScrollBars = ScrollBars.Vertical;
            txtClientLog.Size = new Size(280, 595);
            txtClientLog.TabIndex = 0;
            txtClientLog.TextChanged += txtClientLog_TextChanged;
            // 
            // groupServer
            // 
            groupServer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupServer.Controls.Add(txtServerLog);
            groupServer.Location = new Point(650, 10);
            groupServer.Name = "groupServer";
            groupServer.Size = new Size(300, 625);
            groupServer.TabIndex = 1;
            groupServer.TabStop = false;
            groupServer.Text = "Серверная сторона";
            groupServer.Enter += groupServer_Enter;
            // 
            // txtServerLog
            // 
            txtServerLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtServerLog.Location = new Point(10, 20);
            txtServerLog.Multiline = true;
            txtServerLog.Name = "txtServerLog";
            txtServerLog.ReadOnly = true;
            txtServerLog.ScrollBars = ScrollBars.Vertical;
            txtServerLog.Size = new Size(280, 595);
            txtServerLog.TabIndex = 0;
            txtServerLog.TextChanged += txtServerLog_TextChanged;
            // 
            // btnSendToServer
            // 
            btnSendToServer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSendToServer.Location = new Point(10, 605);
            btnSendToServer.Name = "btnSendToServer";
            btnSendToServer.Size = new Size(130, 30);
            btnSendToServer.TabIndex = 1;
            btnSendToServer.Text = "Передать серверу";
            btnSendToServer.UseVisualStyleBackColor = true;
            btnSendToServer.Click += btnSendToServer_Click;
            // 
            // btnSendToClient
            // 
            btnSendToClient.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSendToClient.Location = new Point(200, 605);
            btnSendToClient.Name = "btnSendToClient";
            btnSendToClient.Size = new Size(130, 30);
            btnSendToClient.TabIndex = 3;
            btnSendToClient.Text = "Передать клиенту";
            btnSendToClient.UseVisualStyleBackColor = true;
            btnSendToClient.Click += btnSendToClient_Click;
            // 
            // lblIp
            // 
            lblIp.AutoSize = true;
            lblIp.Location = new Point(10, 508);
            lblIp.Name = "lblIp";
            lblIp.Size = new Size(53, 15);
            lblIp.TabIndex = 4;
            lblIp.Text = "IP-адрес";
            lblIp.Click += lblIp_Click;
            // 
            // txtIp
            // 
            txtIp.Location = new Point(69, 508);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(120, 23);
            txtIp.TabIndex = 5;
            txtIp.Text = "127.0.0.1";
            txtIp.TextChanged += txtIp_TextChanged;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(12, 543);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(110, 30);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Отключиться";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnServerDisconnect
            // 
            btnServerDisconnect.Location = new Point(200, 507);
            btnServerDisconnect.Name = "btnServerDisconnect";
            btnServerDisconnect.Size = new Size(130, 30);
            btnServerDisconnect.TabIndex = 7;
            btnServerDisconnect.Text = "Сервер отключить";
            btnServerDisconnect.UseVisualStyleBackColor = true;
            btnServerDisconnect.Click += btnServerDisconnect_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExit.Location = new Point(230, 543);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(100, 30);
            btnExit.TabIndex = 8;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // cmbPath
            // 
            cmbPath.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPath.FormattingEnabled = true;
            cmbPath.Location = new Point(10, 10);
            cmbPath.Name = "cmbPath";
            cmbPath.Size = new Size(320, 23);
            cmbPath.TabIndex = 9;
            cmbPath.SelectedIndexChanged += cmbPath_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 647);
            Controls.Add(cmbPath);
            Controls.Add(btnExit);
            Controls.Add(btnServerDisconnect);
            Controls.Add(btnDisconnect);
            Controls.Add(txtIp);
            Controls.Add(lblIp);
            Controls.Add(btnSendToClient);
            Controls.Add(btnSendToServer);
            Controls.Add(groupServer);
            Controls.Add(groupClient);
            Controls.Add(listFiles);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Программа для обмена данными между компьютерами";
            groupClient.ResumeLayout(false);
            groupClient.PerformLayout();
            groupServer.ResumeLayout(false);
            groupServer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listFiles;
        private GroupBox groupClient;
        private TextBox txtClientLog;
        private GroupBox groupServer;
        private TextBox txtServerLog;
        private Button btnSendToServer;
        private Button btnSendToClient;
        private Label lblIp;
        private TextBox txtIp;
        private Button btnDisconnect;
        private Button btnServerDisconnect;
        private Button btnExit;
        private ComboBox cmbPath;
    }
}
