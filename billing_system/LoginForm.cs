using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing_system
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                errors.Add("Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
                errors.Add("Password cannot be empty.");

            if (errors.Any())
                MessageBox.Show(string.Join("\n", errors), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errors.Any();
        }

        private IEnumerable<(string Username, string Password, bool IsAdmin)> GetUsers()
        {
            var users = Database.ExecuteSqlCommand("SELECT * FROM users").Rows;
            return from DataRow user in users
                   select ((string)user["username"], (string)user["password"], Convert.ToBoolean(user["is_admin"]));
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!IsValid()) return;

            var users = GetUsers().ToList();
            var credentials = users.Select(user => (user.Username, user.Password));

            if (!credentials.Contains((UsernameTextBox.Text, PasswordTextBox.Text)))
            {
                MessageBox.Show("Invalid login credentials.");
                return;
            }

            Hide();

            var (Username, Password, IsAdmin) = users.Find(user => user.Username == UsernameTextBox.Text && user.Password == PasswordTextBox.Text);

            var mainForm = new MainForm();

            if (IsAdmin)
                mainForm.AdminIsLoggedIn = true;

            if (mainForm.ShowDialog() == DialogResult.Cancel)
            {
                UsernameTextBox.Text = null;
                PasswordTextBox.Text = null;

                Show();

                UsernameTextBox.Focus();
            }
        }
    }
}
