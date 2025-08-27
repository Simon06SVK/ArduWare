using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
                    connectionStatus.Text = "Odpojene";
                    connectionStatus.BackColor = Color.Red;
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
            var client = new SupabaseClient();

            if (isConnected)
            {
                connectionStatus.Text = "Pripojené: " + foundPortName;
                connectionStatus.BackColor = Color.Green;
                serialPort.DataReceived += SerialDataReceived;
                serialPort.Open();
            }
            else
            {
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
            string savedPort = File.Exists("last_port.txt")
            ? File.ReadAllText("last_port.txt").Trim()
            : null;

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
                        port.Open();
                        Thread.Sleep(1500);
                        port.WriteLine("PING");
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
                    Thread.Sleep(5000); // Wait before exiting
                    Environment.Exit(1);
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
                                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "last_port.txt");
                                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                                        {
                                            File.WriteAllText(path, portName);
                                            Log(File.Exists("last_port.txt") ? "File created!" : "File not found.", Color.White);
                                        }
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
            }
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message = serialPort.ReadLine();
            Invoke(new Action(() =>
            {
                txtConsole.AppendText(message + Environment.NewLine);
            }));
        }

        /*public async Task GetUsersAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://follmgivhtgsssxdhavk.supabase.co/rest/v1/Database?select=*");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZvbGxtZ2l2aHRnc3NzeGRoYXZrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTU1MzAxMjAsImV4cCI6MjA3MTEwNjEyMH0.z8jamERe1SkGzerSv8xggA2AeDKWTk8ARkGTRyzCZKU");
            client.DefaultRequestHeaders.Add("apikey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZvbGxtZ2l2aHRnc3NzeGRoYXZrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTU1MzAxMjAsImV4cCI6MjA3MTEwNjEyMH0.z8jamERe1SkGzerSv8xggA2AeDKWTk8ARkGTRyzCZKU");

            var response = await client.GetAsync("users"); // Replace 'users' with your table name
            var json = await response.Content.ReadAsStringAsync();
            Log(json, Color.DarkGreen);
        }*/

        /*public async Task<string> LoadDatabaseAsync()
        {
            string baseUrl = "https://follmgivhtgsssxdhavk.supabase.co/rest/v1/Database?select=*";
            string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZvbGxtZ2l2aHRnc3NzeGRoYXZrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTU1MzAxMjAsImV4cCI6MjA3MTEwNjEyMH0.z8jamERe1SkGzerSv8xggA2AeDKWTk8ARkGTRyzCZKU";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Add("apikey", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("https://follmgivhtgsssxdhavk.supabase.co/rest/v1/Database?select=*");
                var json = await response.Content.ReadAsStringAsync();

                return json;
            }
        }*/

        /*public string FormatJsonToText(string json)
        {
            var array = JArray.Parse(json);
            var sb = new StringBuilder();

            foreach (var item in array)
            {
                string id = item["ID"]?.ToString();
                string title = item["Title"]?.ToString();
                string level = item["Security level"]?.ToString();

                sb.AppendLine($"{id} - {title} ({level})");
            }

            return sb.ToString();
        }*/

        private void LoadFingerprintSlots()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID",
                HeaderText = "Slot ID"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Type",
                HeaderText = "Type"
            });

            dataGridView1.DataSource = GetFingerprintSlots(); // Your slot-fetching method
        }

        public List<FingerprintSlot> GetFingerprintSlots()
        {
            var slots = new List<FingerprintSlot>();
            bool isUsed;
            string response;

            for (int i = 1; i <= 128; i++)
            {
                string type = i <= 99 ? "Permanent" : "Temporary";
                serialPort.WriteLine($"CHECK_SLOT {i}");

                Task.Delay(50).Wait(); // Wait for the sensor to process the command

                progressBarDataLoad.Value = (i * 100) / 128;

                response = serialPort.ReadLine();

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
                    Status = isUsed ? "Occupied" : "Empty",
                    Type = type
                });
            }

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
    }
}


public class SupabaseClient
{
    private static readonly string baseUrl = "https://follmgivhtgsssxdhavk.supabase.co/rest/v1/Database?select=*";
    private static readonly string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZvbGxtZ2l2aHRnc3NzeGRoYXZrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTU1MzAxMjAsImV4cCI6MjA3MTEwNjEyMH0.z8jamERe1SkGzerSv8xggA2AeDKWTk8ARkGTRyzCZKU"; // Replace with your actual anon key

    public async Task InsertUserAsync(int id, string title)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Add("apikey", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = $"{{\"id\": {id}, \"title\": \"{title}\"}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Database", content); 
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response: {response.StatusCode}\n{result}");
        }
    }
}
