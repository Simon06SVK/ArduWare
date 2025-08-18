using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduWare
{
    public partial class Form1 : Form
    {
        SerialPort serialPort = new SerialPort();
        public static string foundPortName;

        bool isConnected = false;

        public Form1()
        {
            InitializeComponent();
            if(isConnected)
            {
                connectionStatus.Text = "Pripojené";
                connectionStatus.BackColor = Color.Green;
            }
            else
            {
                connectionStatus.Text = "Odpojené";
                connectionStatus.BackColor = Color.Red;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            FindArduinoByHandshake();
            if (!string.IsNullOrEmpty(foundPortName))
            {
                try
                {
                    serialPort.PortName = foundPortName;
                    serialPort.BaudRate = 9600;
                    serialPort.Open();
                    isConnected = true;
                    txtConsole.AppendText("\n<=== PRIPOJENE ===>");
                    txtConsole.AppendText("\nPort: " + foundPortName);
                }
                catch (Exception ex)
                {
                    txtConsole.AppendText("\nChyba pri pripojení: " + ex.Message);
                }
            }
        }

        public string FindArduinoByHandshake()
        {
            txtConsole.AppendText("Dostupne porty: " + string.Join(", ", SerialPort.GetPortNames()));

            txtConsole.AppendText("\n\nVyhladavam port...");
            const int maxAttempts = 3;
            bool portFound = false;
            while (!portFound && maxAttempts <= 3)
            {
                foreach (string portName in SerialPort.GetPortNames())
                {
                    try
                    {
                        using (SerialPort testPort = new SerialPort(portName, 9600))
                        {
                            testPort.ReadTimeout = 500;
                            testPort.WriteTimeout = 500;
                            testPort.Open();
                            txtConsole.AppendText("Skusam port " + portName);
                            testPort.WriteLine("PING");
                            Thread.Sleep(150); // Give Arduino time to respond

                            string response = testPort.ReadLine();
                            if (response.Trim() == "ARDUINO")
                            {
                                Console.WriteLine("Zariadenie najdene na porte " + portName);
                                portFound = true;
                                foundPortName = portName;
                                return portName;
                            }
                        }
                    }
                    catch (TimeoutException)
                    {
                        // No response — try next port
                    }
                    catch (IOException)
                    {
                        // Port error — skip it
                    }
                    catch (Exception ex)
                    {
                        txtConsole.AppendText($"Chyba na porte {portName}: {ex.Message}");
                    }

                }
                Thread.Sleep(500);
            }
            txtConsole.AppendText("Zariadenie nebolo najdene");
            txtConsole.AppendText("\nSkontrolujte stav pripojenia, pripadne restartujte zariadeie a skuste znovu.");
            return null; // This will never be reached, but required for compilation
        }
    }
}
