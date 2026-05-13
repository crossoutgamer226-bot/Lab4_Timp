using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab4_Timp
{
    /// <summary>
    /// Главная форма приложения, реализующая функциональность клиента и сервера,
    /// а также навигацию по файловой системе.
    /// </summary>
    public partial class MainForm : Form
    {
        private TcpClient? client;
        private NetworkStream? clientStream;

        private TcpListener? server;
        private NetworkStream? serverStream;

        private bool serverRunning;

        // Стек истории переходов назад.
        private readonly Stack<string> backHistory = new Stack<string>();

        // Стек истории переходов вперёд.
        private readonly Stack<string> forwardHistory = new Stack<string>();

        // Текущий путь.
        private string currentPath = string.Empty;

        /// <summary>
        /// Инициализирует форму, загружает диски и запускает сервер.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            LoadDrives();
            StartServer();

            // Установка начального пути.
            string[] drives = Directory.GetLogicalDrives();

            if (drives.Length > 0)
            {
                currentPath = drives[0];
                OpenDirectory(currentPath, addToHistory: false);
            }
        }

        /// <summary>
        /// Загружает список логических дисков в ComboBox.
        /// </summary>
        private void LoadDrives()
        {
            cmbPath.Items.Clear();

            foreach (string drive in Directory.GetLogicalDrives())
            {
                cmbPath.Items.Add(drive);
            }

            if (cmbPath.Items.Count > 0)
            {
                cmbPath.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Открывает каталог, обновляет список файлов и историю переходов.
        /// </summary>
        /// <param name="path">Путь к каталогу.</param>
        /// <param name="addToHistory">Добавлять ли путь в историю.</param>
        private void OpenDirectory(string path, bool addToHistory = true)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    txtClientLog.AppendText($"Каталог не существует: {path}.\r\n");
                    return;
                }

                listFiles.Items.Clear();

                foreach (string dir in Directory.GetDirectories(path))
                {
                    listFiles.Items.Add(dir);
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    listFiles.Items.Add(file);
                }

                if (addToHistory && currentPath != string.Empty)
                {
                    backHistory.Push(currentPath);
                }

                currentPath = path;
                txtPath.Text = path;

                forwardHistory.Clear();
            }
            catch (Exception ex)
            {
                txtClientLog.AppendText($"Ошибка: {ex.Message}.\r\n");
            }
        }

        /// <summary>
        /// Обработчик выбора диска.
        /// </summary>
        private void CmbPath_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbPath.SelectedItem is string path)
            {
                OpenDirectory(path);
            }
        }

        /// <summary>
        /// Обработчик двойного клика по элементу списка файлов.
        /// </summary>
        private void ListFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (listFiles.SelectedItem is not string path)
            {
                return;
            }

            if (Directory.Exists(path))
            {
                OpenDirectory(path);
            }
            else if (File.Exists(path))
            {
                ConnectClient();

                byte[] data = Encoding.UTF8.GetBytes(path);
                clientStream?.Write(data, 0, data.Length);

                txtClientLog.AppendText($"Отправлено серверу: {path}.\r\n");
            }
        }

        /// <summary>
        /// Обработчик ввода пути вручную.
        /// </summary>
        private void TxtPath_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = txtPath.Text.Trim();

                if (Directory.Exists(path))
                {
                    OpenDirectory(path);
                }
                else
                {
                    txtClientLog.AppendText($"Каталог не найден: {path}.\r\n");
                }
            }
        }

        // -----------------------------
        // НАВИГАЦИЯ
        // -----------------------------

        /// <summary>
        /// Переход назад по истории.
        /// </summary>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (backHistory.Count == 0)
            {
                return;
            }

            forwardHistory.Push(currentPath);

            string previous = backHistory.Pop();
            OpenDirectory(previous, addToHistory: false);
        }

        /// <summary>
        /// Переход вперёд по истории.
        /// </summary>
        private void BtnForward_Click(object sender, EventArgs e)
        {
            if (forwardHistory.Count == 0)
            {
                return;
            }

            backHistory.Push(currentPath);

            string next = forwardHistory.Pop();
            OpenDirectory(next, addToHistory: false);
        }

        /// <summary>
        /// Переход на уровень вверх.
        /// </summary>
        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentPath))
            {
                return;
            }

            string? parent = Directory.GetParent(currentPath)?.FullName;

            if (parent != null)
            {
                OpenDirectory(parent);
            }
        }

        // -----------------------------
        // СЕРВЕР
        // -----------------------------

        /// <summary>
        /// Запускает сервер.
        /// </summary>
        private void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 9000);
                server.Start();
                serverRunning = true;

                txtServerLog.AppendText($"Сервер включён {DateTime.Now}.\r\n");

                server.BeginAcceptTcpClient(OnClientConnected, null);
            }
            catch (Exception ex)
            {
                txtServerLog.AppendText($"Ошибка запуска сервера: {ex.Message}.\r\n");
            }
        }

        /// <summary>
        /// Обрабатывает подключение клиента.
        /// </summary>
        private void OnClientConnected(IAsyncResult ar)
        {
            try
            {
                if (server == null)
                {
                    return;
                }

                TcpClient connectedClient = server.EndAcceptTcpClient(ar);
                serverStream = connectedClient.GetStream();

                Invoke(new Action(() =>
                {
                    txtServerLog.AppendText($"Клиент подключился {DateTime.Now}.\r\n");
                }));

                string drives = string.Join(",", Directory.GetLogicalDrives());
                byte[] data = Encoding.UTF8.GetBytes(drives);

                serverStream.Write(data, 0, data.Length);

                byte[] buffer = new byte[4096];

                serverStream.BeginRead(buffer, 0, buffer.Length, OnServerDataReceived, buffer);

                server.BeginAcceptTcpClient(OnClientConnected, null);
            }
            catch
            {
                // Игнорируем ошибки.
            }
        }

        /// <summary>
        /// Обрабатывает данные, полученные сервером.
        /// </summary>
        private void OnServerDataReceived(IAsyncResult ar)
        {
            try
            {
                if (serverStream == null)
                {
                    return;
                }

                byte[]? buffer = ar.AsyncState as byte[];

                if (buffer == null)
                {
                    return;
                }

                int bytes = serverStream.EndRead(ar);

                if (bytes <= 0)
                {
                    return;
                }

                string received = Encoding.UTF8.GetString(buffer, 0, bytes);

                Invoke(new Action(() =>
                {
                    txtServerLog.AppendText($"Сервер получил: {received}.\r\n");
                }));

                if (received == "exit")
                {
                    serverStream.Close();
                    serverStream = null;
                    return;
                }

                if (Directory.Exists(received))
                {
                    string[] items = Directory.GetFileSystemEntries(received);
                    string response = string.Join(",", items);

                    byte[] data = Encoding.UTF8.GetBytes(response);
                    serverStream.Write(data, 0, data.Length);
                }
                else if (File.Exists(received))
                {
                    string text = File.ReadAllText(received);
                    byte[] data = Encoding.UTF8.GetBytes(text);

                    serverStream.Write(data, 0, data.Length);
                }

                serverStream.BeginRead(buffer, 0, buffer.Length, OnServerDataReceived, buffer);
            }
            catch
            {
                // Игнорируем ошибки.
            }
        }

        /// <summary>
        /// Отправляет список дисков клиенту.
        /// </summary>
        private void SendDrivesToClient()
        {
            if (serverStream == null)
            {
                txtServerLog.AppendText("Нет подключённого клиента.\r\n");
                return;
            }

            string drives = string.Join(",", Directory.GetLogicalDrives());
            byte[] data = Encoding.UTF8.GetBytes(drives);

            serverStream.Write(data, 0, data.Length);

            txtServerLog.AppendText("Сервер отправил список дисков клиенту.\r\n");
        }

        private void BtnSendToClient_Click(object? sender, EventArgs e)
        {
            SendDrivesToClient();
        }

        // -----------------------------
        // КЛИЕНТ
        // -----------------------------

        /// <summary>
        /// Подключает клиента к серверу.
        /// </summary>
        private void ConnectClient()
        {
            if (client != null && client.Connected)
            {
                return;
            }

            client = new TcpClient();
            client.Connect(txtIp.Text, 9000);

            clientStream = client.GetStream();

            txtClientLog.AppendText($"Клиент подключён {DateTime.Now}.\r\n");

            byte[] buffer = new byte[4096];

            clientStream.BeginRead(buffer, 0, buffer.Length, OnClientDataReceived, buffer);
        }

        /// <summary>
        /// Обрабатывает данные, полученные клиентом.
        /// </summary>
        private void OnClientDataReceived(IAsyncResult ar)
        {
            try
            {
                if (clientStream == null)
                {
                    return;
                }

                byte[]? buffer = ar.AsyncState as byte[];

                if (buffer == null)
                {
                    return;
                }

                int bytes = clientStream.EndRead(ar);

                if (bytes <= 0)
                {
                    return;
                }

                string received = Encoding.UTF8.GetString(buffer, 0, bytes);

                Invoke(new Action(() =>
                {
                    txtClientLog.AppendText($"Клиент получил: {received}.\r\n");
                }));

                clientStream.BeginRead(buffer, 0, buffer.Length, OnClientDataReceived, buffer);
            }
            catch
            {
                // Игнорируем ошибки.
            }
        }

        /// <summary>
        /// Отправляет выбранный путь серверу.
        /// </summary>
        private void BtnSendToServer_Click(object? sender, EventArgs e)
        {
            if (listFiles.SelectedItem is not string path)
            {
                txtClientLog.AppendText("Не выбран файл или каталог.\r\n");
                return;
            }

            ConnectClient();

            byte[] data = Encoding.UTF8.GetBytes(path);
            clientStream?.Write(data, 0, data.Length);

            txtClientLog.AppendText($"Отправлено серверу: {path}.\r\n");
        }

        /// <summary>
        /// Отключает клиента.
        /// </summary>
        private void BtnDisconnect_Click(object? sender, EventArgs e)
        {
            try
            {
                if (clientStream != null)
                {
                    byte[] exitMsg = Encoding.UTF8.GetBytes("exit");
                    clientStream.Write(exitMsg, 0, exitMsg.Length);
                }

                client?.Close();
                client = null;

                txtClientLog.AppendText($"Клиент отключён {DateTime.Now}.\r\n");
            }
            catch
            {
                // Игнорируем ошибки.
            }
        }

        /// <summary>
        /// Закрывает приложение.
        /// </summary>
        private void BtnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // -----------------------------
        // ПУСТЫЕ ОБРАБОТЧИКИ
        // -----------------------------

        private void ListFiles_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        private void GroupClient_Enter(object? sender, EventArgs e)
        {
        }

        private void TxtClientLog_TextChanged(object? sender, EventArgs e)
        {
        }

        private void GroupServer_Enter(object? sender, EventArgs e)
        {
        }

        private void TxtServerLog_TextChanged(object? sender, EventArgs e)
        {
        }

        private void LblIp_Click(object? sender, EventArgs e)
        {
        }

        private void TxtIp_TextChanged(object? sender, EventArgs e)
        {
        }

        private void BtnServerDisconnect_Click(object? sender, EventArgs e)
        {
        }

        private void TxtPath_TextChanged(object? sender, EventArgs e)
        {
        }
    }
}
