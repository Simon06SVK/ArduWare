using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArduWare
{
    public partial class Settings : Form
    {
        string hashedPassword;

        public Settings()
        {
            InitializeComponent();
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArduWare");
            string path = Path.Combine(folderPath, "config.txt");

            hashedPassword = File.ReadAllText(path).Trim();
            Console.WriteLine(hashedPassword);

            txtNewPassword.TextChanged += ValidatePasswords;
            txtVerifyPassword.TextChanged += ValidatePasswords;

        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(txtCurrentPassword.Text, hashedPassword);
            if (!isCurrentPasswordValid)
            {
                MessageBox.Show("Aktualne heslo je nespravne.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Clear();
                return;
            }
            else
            {
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtNewPassword.Text);
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ArduWare");
                string path = Path.Combine(folderPath, "config.txt");

                File.WriteAllText(path, hashedPassword);
                MessageBox.Show("Heslo bolo úspešne zmenené.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtVerifyPassword.Clear();
                this.Close();
            }
        }

        private void ValidatePasswords(object sender, EventArgs e)
        {
            bool isValid =
                txtNewPassword.Text.Length >= 5 &&
                txtNewPassword.Text == txtVerifyPassword.Text;

            txtVerifyPassword.BackColor =
                txtVerifyPassword.Text == txtNewPassword.Text ? Color.White : Color.MistyRose;


            labelValidation.Text = txtNewPassword.Text.Length < 5 ? "Heslo musi obsahovat aspon 5 znakov" : "";
            labelValidation.Text = (txtNewPassword.Text != txtVerifyPassword.Text) ? "Hesla sa nezhoduju" : "";

            buttonConfirm.Enabled = isValid;
        }
    }
}
