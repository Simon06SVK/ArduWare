using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace ArduWare
{
    public partial class MainUI : System.Windows.Forms.Form
    {
        SerialPort serialPort = new SerialPort();
        public static string foundPortName;
        bool isConnected = false;

        private bool isScanning = false;
        private CancellationTokenSource cts;

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!isScanning)
            {
                // Start scanning
                isScanning = true;
                buttonConnect.Text = "Prerusit";
                cts = new CancellationTokenSource();
                if (isConnected)
                {
                    try
                    {
                        serialPort.Close();
                        isConnected = false;
                        connectionStatus.Text = "Odpojené";
                        connectionStatus.BackColor = Color.Red;
                        buttonConnect.Text = "Spojenie";
                        Log("\n<=== ODPOJENE ===>\n", Color.White);
                    }


                    catch (OperationCanceledException)
                    {
                        Log("\nVyhladavanie prerusene uzivatelom\n", Color.Red);
                    }

                    catch (Exception ex)
                    {
                        Log("\nChyba pri odpojení: \n" + ex.Message, Color.White);
                    }
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    connectionStatus.Text = "Pripájam";
                    connectionStatus.BackColor = Color.DarkOrange;

                    await Task.Run(() => FindArduinoByHandshake(cts.Token));

                    if (!string.IsNullOrEmpty(foundPortName))
                    {
                        try
                        {
                            serialPort.PortName = foundPortName;
                            serialPort.BaudRate = 9600;
                            serialPort.Open();
                            isConnected = true;

                            Cursor = Cursors.Default;
                            buttonConnect.Text = "Pripojené";
                            connectionStatus.Text = "Pripojené: " + foundPortName;
                            connectionStatus.BackColor = Color.Green;

                            Log("\n<=== PRIPOJENE ===>\n", Color.White);

                            LoadFingerprintSlots();

                        }
                        catch (Exception ex)
                        {
                            Log("\nChyba pri pripojení: \n" + ex.Message, Color.White);
                        }
                    }
                    else
                    {
                        connectionStatus.Text = "Odpojene";
                        connectionStatus.BackColor = Color.Red;
                    }
                }
            }
            else
            {
                // Stop scanning
                cts.Cancel();
                buttonConnect.Text = "Spojenie";
                isScanning = false;
            }
        }


        public MainUI()
        {
            InitializeComponent();

            if (isConnected)
            {
                connectionStatus.Text = "Pripojené: " + foundPortName;
                connectionStatus.BackColor = Color.Green;
                serialPort.DataReceived += SerialDataReceived;
                serialPort.Open();
            }
            else
            {
                Console.WriteLine("Not connected");
                connectionStatus.Text = "Odpojene";
                connectionStatus.BackColor = Color.Red;
            }
        }

        private void FindArduinoByHandshake(CancellationToken token)
        {
            Log("\n\nVyhladavam port, cakajte prosim...\n", Color.White);
            int maxAttempts = 3;
            int attempt = 0;
            bool portFound = false;
            string savedPort = null;
            string path;
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArduWare");

            if (!Directory.Exists(folderPath))
            {
                path = Path.Combine(folderPath, "port.txt");
                savedPort = File.ReadAllText(path);
            }

            if (!string.IsNullOrEmpty(savedPort))
            {
                Log($"Nájdený uložený port: {savedPort}", Color.White);

                try
                {
                    if (token.IsCancellationRequested)
                        return;

                    using (var port = new SerialPort(savedPort, 9600))
                    {

                        port.ReadTimeout = 2000;
                        port.DtrEnable = true;
                        port.Open();

                        Thread.Sleep(1500);

                        string response = port.ReadLine();

                        if (response.Contains("ARDUINO"))
                        {
                            //txtConsole.AppendText("Arduino nájdené na uloženom porte: " + savedPort);
                            foundPortName = savedPort;
                        }
                        else
                        {
                            //txtConsole.AppendText("Nesprávna odpoveď z uloženého portu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log("\nChyba pri použití uloženého portu: \n" + ex.Message, Color.White);
                    Log("Pokračujem v hľadaní dostupných portov...\n", Color.White);
                    return; // Retry finding the port
                }
                return;
            }
            else
            {
                // Get all available serial ports
                string[] ports = SerialPort.GetPortNames();
                Log("Dostupné porty: " + string.Join(", ", ports), Color.White);


                if (ports.Length == 0)
                {
                    Log("\nNeboli najdene ziadne dostupne porty.\n", Color.White);
                    return; // This will never be reached, but required for compilation
                }

                while (attempt < maxAttempts && !portFound)
                {


                    foreach (string portName in ports)
                    {
                        if (token.IsCancellationRequested)
                            throw new OperationCanceledException();
                        try
                        {
                            using (var port = new SerialPort(portName, 9600))
                            {
                                port.ReadTimeout = 3000;
                                port.DtrEnable = true; // Dôležité: aktivuje reset pri otvorení portu
                                port.Open();

                                Log($"Port {portName} otvorený, čakám na odpoveď...\n", Color.White);

                                // Čakaj na automatickú odpoveď z Arduina
                                string response = port.ReadLine();
                                //txtConsole.AppendText($"Odpoveď z {portName}: '{response}'");
                                port.Close();
                                if (response.Contains("ARDUINO"))
                                {
                                    Log($"Zariadenie najdene na porte {portName}\n", Color.White);
                                    try
                                    {
                                        folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArduWare");
                                        if(!Directory.Exists(folderPath))
                                            Directory.CreateDirectory(folderPath);
                                        path = Path.Combine(folderPath, "port.txt");
                                        File.WriteAllText(path, portName);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to create or clear file: {ex.Message}");
                                    }

                                    portFound = true;
                                    foundPortName = portName;
                                }
                                else
                                {
                                    Log($"Nespravna odpoved z {portName}: {response}\n", Color.White);
                                }
                            }
                        }
                        catch (TimeoutException)
                        {
                            Log($"\nTimeout na porte {portName}\n", Color.White);
                        }
                        catch (IOException)
                        {
                            Log($"\nIO chyba na porte {portName}\n", Color.White);
                        }
                        catch (Exception ex)
                        {
                            Log($"\n[{ex.GetType().Name}] na porte {portName}: {ex.Message}\n", Color.White);
                        }

                        txtConsole.Invoke((MethodInvoker)(() =>
                        {
                            buttonConnect.Text = "Spojenie";
                            isScanning = false;
                        }));

                    }

                    attempt++;
                    Thread.Sleep(1000); // Wait before retrying
                }
                if (!portFound)
                {
                    Log("\nZariadenie nebolo najdene, skontrolujte stav pripojenia...\n", Color.White);
                    return; // This will never be reached, but required for compilation
                }
                return; // This will never be reached, but required for compilation
            }
        }

        private void Log(string message, Color color)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.Invoke(new Action(() => Log(message, color)));
            }
            else
            {
                txtConsole.SelectionStart = txtConsole.TextLength;
                txtConsole.SelectionLength = 0;
                txtConsole.SelectionColor = color;
                txtConsole.AppendText(message + Environment.NewLine);
                txtConsole.SelectionColor = txtConsole.ForeColor;
                txtConsole.ScrollToCaret();
            }
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string message = serialPort.ReadExisting(); // Non-blocking
                Invoke(new Action(() =>
                {
                    txtConsole.AppendText(message + Environment.NewLine);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serial read error: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFingerprintSlots()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                ReadOnly = true,
                DataPropertyName = "ID",
                HeaderText = "ID"
            });

            

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                ReadOnly = true,
                DataPropertyName = "Status",
                HeaderText = "Stav"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Type",
                HeaderText = "Typ"
            });
            
            /*dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Meno"
            });*/

            dataGridView1.DataSource = GetFingerprintSlots(); // Your slot-fetching method
        }

        public List<FingerprintSlot> GetFingerprintSlots()
        {
            var slots = new List<FingerprintSlot>();
            bool isUsed;
            string response;

            for (int i = 1; i <= 128; i++)
            {
                string type = i <= 5 ? "Admin" : "Pouzivatel";
                serialPort.WriteLine($"CHECK_SLOT {i}");

                //statusRX.BackColor = Color.Lime;
                Task.Delay(100).Wait(); // Wait for the sensor to process the command
                //statusRX.BackColor = Color.Silver;
                //Task.Delay(50).Wait(); // Wait for the sensor to process the command

                progressBarDataLoad.Value = (i * 100) / 128;

                response = serialPort.ReadLine().Trim();

                if (response == "yes")
                {
                    isUsed = true; // Query AS608 sensor
                }
                else
                {
                    if (response == "no")
                    {
                        isUsed = false; // Query AS608 sensor
                    }
                    else
                    {
                        Log($"Chyba pri kontrole slotu {i}: {response}", Color.Red);
                        continue; // Skip to the next slot if there's an error
                    }
                }

                slots.Add(new FingerprintSlot
                {
                    ID = i,
                    Status = isUsed ? "Obsadene" : "Prazdne",
                    Type = type
                });
            }
            progressBarDataLoad.Hide();

            return slots;
        }

        public class FingerprintSlot
        {
            public int ID { get; set; }
            public string Status { get; set; } // "Empty" or "Occupied"
            public string Type { get; set; }   // "Permanent" or "Temporary"
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            var result = helpForm.ShowDialog();
        }

        private void buttonCheckUpdates_Click(object sender, EventArgs e)
        {
            WinSparkle.win_sparkle_check_update_with_ui();
        }

        private void buttonAddFP_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                Console.WriteLine(rowIndex+1);
                string statusValue = dataGridView1.CurrentRow.Cells[1].Value.ToString().ToLower();

                if (statusValue == "obsadene")
                {
                    var result = MessageBox.Show("Slot obsadeny. Chcete ho prepisat?", "Upozornenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        serialPort.WriteLine($"pridaj {rowIndex+1}");
                    }
                    else
                        return;
                }
                else
                {
                    serialPort.WriteLine($"pridaj {rowIndex+1}");
                }
            }
        }

        private void buttonRemoveFP_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                Console.WriteLine(rowIndex+1);
                string statusValue = dataGridView1.CurrentRow.Cells[1].Value.ToString().ToLower();

                if (statusValue == "obsadene")
                {
                    var result = MessageBox.Show("Naozaj chcete odstranit tento odtlacok?", "Upozornenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            serialPort.WriteLine($"odstran {rowIndex + 1}");
                            Console.WriteLine($"odstran {rowIndex + 1}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Chyba pri odstranovani: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                        return;
                }
                else
                {
                    MessageBox.Show("Vybrany slot je prazdny.", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.StartPosition = FormStartPosition.CenterScreen;
            settingsForm.Show();

        }

        private void buttonReloadData_Click(object sender, EventArgs e)
        {
            progressBarDataLoad.Show();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GetFingerprintSlots(); // Your slot-fetching method
        }
    }
}