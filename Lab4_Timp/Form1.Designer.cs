namespace Lab4_Timp
{
    /// <summary>
    /// Форма MainForm. Содержит элементы интерфейса и их инициализацию.
    /// </summary>
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        /// Освобождает ресурсы, используемые формой.
        /// </summary>
        /// <param name="disposing">True, если требуется освободить управляемые ресурсы.</param>
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
        /// Метод, автоматически создающий элементы интерфейса формы.
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
            btnBack = new Button();
            btnForward = new Button();
            btnUp = new Button();
            txtPath = new TextBox();

            groupClient.SuspendLayout();
            groupServer.SuspendLayout();
            SuspendLayout();

            // 
            // listFiles
            // 
            listFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listFiles.FormattingEnabled = true;
            listFiles.ItemHeight = 15;
            listFiles.Location = new Point(10, 75);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(320, 424);
            listFiles.TabIndex = 1;

            // Подключение событий.
            listFiles.SelectedIndexChanged += ListFiles_SelectedIndexChanged;
            listFiles.DoubleClick += ListFiles_DoubleClick;

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
            groupClient.Enter += GroupClient_Enter;

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
            txtClientLog.TextChanged += TxtClientLog_TextChanged;

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
            groupServer.Enter += GroupServer_Enter;

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
            txtServerLog.TextChanged += TxtServerLog_TextChanged;

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
            btnSendToServer.Click += BtnSendToServer_Click;

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
            btnSendToClient.Click += BtnSendToClient_Click;

            // 
            // lblIp
            // 
            lblIp.AutoSize = true;
            lblIp.Location = new Point(10, 508);
            lblIp.Name = "lblIp";
            lblIp.Size = new Size(53, 15);
            lblIp.TabIndex = 4;
            lblIp.Text = "IP-адрес";
            lblIp.Click += LblIp_Click;

            // 
            // txtIp
            // 
            txtIp.Location = new Point(69, 508);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(120, 23);
            txtIp.TabIndex = 5;
            txtIp.Text = "127.0.0.1";
            txtIp.TextChanged += TxtIp_TextChanged;

            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(12, 543);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(110, 30);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Отключиться";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += BtnDisconnect_Click;

            // 
            // btnServerDisconnect
            // 
            btnServerDisconnect.Location = new Point(200, 507);
            btnServerDisconnect.Name = "btnServerDisconnect";
            btnServerDisconnect.Size = new Size(130, 30);
            btnServerDisconnect.TabIndex = 7;
            btnServerDisconnect.Text = "Сервер отключить";
            btnServerDisconnect.UseVisualStyleBackColor = true;
            btnServerDisconnect.Click += BtnServerDisconnect_Click;

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
            btnExit.Click += BtnExit_Click;

            // 
            // cmbPath
            // 
            cmbPath.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPath.FormattingEnabled = true;
            cmbPath.Location = new Point(10, 10);
            cmbPath.Name = "cmbPath";
            cmbPath.Size = new Size(320, 23);
            cmbPath.TabIndex = 9;
            cmbPath.SelectedIndexChanged += CmbPath_SelectedIndexChanged;

            // 
            // btnBack
            // 
            btnBack.Location = new Point(10, 40);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 10;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBack_Click;

            // 
            // btnForward
            // 
            btnForward.Location = new Point(90, 40);
            btnForward.Name = "btnForward";
            btnForward.Size = new Size(75, 23);
            btnForward.TabIndex = 11;
            btnForward.Text = "Вперёд";
            btnForward.UseVisualStyleBackColor = true;
            btnForward.Click += BtnForward_Click;

            // 
            // btnUp
            // 
            btnUp.Location = new Point(170, 40);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(75, 23);
            btnUp.TabIndex = 12;
            btnUp.Text = "Вверх";
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += BtnUp_Click;

            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(10, 70);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(320, 23);
            txtPath.TabIndex = 13;
            txtPath.KeyDown += TxtPath_KeyDown;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 647);
            Controls.Add(txtPath);
            Controls.Add(btnUp);
            Controls.Add(btnForward);
            Controls.Add(btnBack);
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
        private Button btnBack;
        private Button btnForward;
        private Button btnUp;
        private TextBox txtPath;
    }
}
