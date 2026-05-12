using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab4_Timp
{
    /// <summary>
    /// Главная форма приложения, реализующая функциональность клиента и сервера.
    /// </summary>
    public partial class MainForm : Form
    {
        private TcpClient? client;
        private NetworkStream? clientStream;

        private TcpListener? server;
        private NetworkStream? serverStream;

        private bool serverRunning;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainForm"/>.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            LoadDrives();
            StartServer();
        }

        /// <summary>
        /// Загружает список логических дисков в комбобокс.
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
        /// Загружает список файлов и каталогов по указанному пути.
        /// </summary>
        /// <param name="path">Путь к каталогу.</param>
        private void LoadFiles(string path)
        {
            try
            {
                listFiles.Items.Clear();

                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);

                foreach (string directory in dirs)
                {
                    listFiles.Items.Add(directory);
                }

                foreach (string file in files)
                {
                    listFiles.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                txtClientLog.AppendText($"Ошибка загрузки файлов: {ex.Message}.\r\n");
            }
        }

        private void cmbPath_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string? path = cmbPath.SelectedItem as string;

            if (path != null)
            {
                LoadFiles(path);
            }
        }

        private void listFiles_DoubleClick(object? sender, EventArgs e)
        {
            if (listFiles.SelectedItem is not string path)
            {
                return;
            }

            if (Directory.Exists(path))
            {
                cmbPath.Text = path;
                LoadFiles(path);
            }
        }

        /// <summary>
        /// Запускает сервер и начинает ожидание подключений.
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
        /// Обрабатывает подключение клиента к серверу.
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
                // Ошибка игнорируется.
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
                    Invoke(new Action(() =>
                    {
                        txtServerLog.AppendText("Клиент запросил отключение.\r\n");
                    }));

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
                // Ошибка игнорируется.
            }
        }

        private void btnServerDisconnect_Click(object? sender, EventArgs e)
        {
            try
            {
                serverRunning = false;
                server?.Stop();

                txtServerLog.AppendText($"Сервер отключён {DateTime.Now}.\r\n");
            }
            catch
            {
                // Ошибка игнорируется.
            }
        }

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
                // Ошибка игнорируется.
            }
        }

        private void btnSendToServer_Click(object? sender, EventArgs e)
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

        private void btnSendToClient_Click(object? sender, EventArgs e)
        {
            txtClientLog.AppendText("Кнопка 'Передать клиенту' нажата. Функция не реализована.\r\n");
        }

        private void btnDisconnect_Click(object? sender, EventArgs e)
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
                // Ошибка игнорируется.
            }
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void listFiles_SelectedIndexChanged(object? sender, EventArgs e)
        {
        }

        private void groupClient_Enter(object? sender, EventArgs e)
        {
        }

        private void txtClientLog_TextChanged(object? sender, EventArgs e)
        {
        }

        private void groupServer_Enter(object? sender, EventArgs e)
        {
        }

        private void txtServerLog_TextChanged(object? sender, EventArgs e)
        {
        }

        private void txtIp_TextChanged(object? sender, EventArgs e)
        {
        }

        private void lblIp_Click(object? sender, EventArgs e)
        {
        }
    }
}