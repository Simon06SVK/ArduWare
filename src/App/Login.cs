using System;
using System.IO;
using System.Windows.Forms;

namespace ArduWare
{
    public partial class Login : Form
    {
        static string plainPassword = "admin1234";  // Default password
        static string hashedPassword;

        private void Login_Load(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArduWare");
            string path = Path.Combine(folderPath, "config.txt");

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(folderPath); // This creates the folder if it doesn't exist
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
                File.WriteAllText(path, hashedPassword);
                MessageBox.Show("Doslo k zmene prihlasovacich udajov.\nPouzite predvolene prihlasovacie udaje.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                hashedPassword = File.ReadAllText(path).Trim();
            }
        }


        public Login()
        {
            InitializeComponent();
            txtUsername.Focus();
            this.AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            if (username == "admin" && isValid)
            {
                this.DialogResult = DialogResult.OK;
                if (password == plainPassword)
                {
                    MessageBox.Show("Pouzivate predvolene heslo. Pre vašu bezpečnosť si prosím zmeňte heslo v nastaveniach.", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MainUI main = new MainUI();
            }
            else
            {
                txtPassword.BackColor = System.Drawing.Color.LightCoral;
                MessageBox.Show("Nesprávne meno alebo heslo. Skúste znovu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.BackColor = System.Drawing.Color.White;
            }
        }
    }
}
