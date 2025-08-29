using System;
using System.Windows.Forms;

namespace ArduWare
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtUsername.Focus();
            this.AcceptButton = btnLogin;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (username == "admin" && password == "arduware123")
            {
                this.DialogResult = DialogResult.OK;
                MainUI main = new MainUI();
            }
            else
            {
                MessageBox.Show("Nesprávne meno alebo heslo. Skúste znovu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
