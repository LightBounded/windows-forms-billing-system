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
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        private IEnumerable<string> GetErrors()
        {
            var errors = new List<(bool Error, string Message)>
            {
                (string.IsNullOrWhiteSpace(UsernameTextBox.Text), "Username cannot be empty."),
                (string.IsNullOrWhiteSpace(PasswordTextBox.Text), "Password cannot be empty."),
                (PasswordTextBox.Text != ConfirmPasswordTextBox.Text, "Passwords must match."),
            };


            foreach (var (Error, Message) in errors)
                if (Error)
                    yield return Message;
        }

        private bool FormIsValid()
        {
            if (GetErrors().Any())
                return false;
            return true;
        }

        private bool UserExists()
        {
            var users = Database.ExecuteSqlCommand($@"SELECT * FROM users").Rows;

            foreach (DataRow user in users)
                if ((string)user["username"] == UsernameTextBox.Text)
                    return true;
            return false;
        }

        private bool AtLeastOneUserIsSelected()
        {
            if (UsersDataGridView.SelectedRows.Count > 0) return true;
            return false;
        }

        private bool MultipleUsersAreSelected()
        {
            if (UsersDataGridView.SelectedRows.Count <= 1) return false;
            return true;
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            if (UserExists())
            {
                MessageBox.Show("User already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"INSERT INTO users (username, password, is_admin)
                                          VALUES ('{UsernameTextBox.Text}', '{PasswordTextBox.Text}', {IsAdminCheckBox.Checked})");

            LoadUsers();
            ResetForm();
        }

        private void ResetForm()
        {
            UsernameTextBox.Text = null;
            PasswordTextBox.Text = null;
            ConfirmPasswordTextBox.Text = null;
        }

        private void LoadUsers()
        {
            UsersDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, username AS Username, password AS Password, is_admin AS 'Is Admin'
                                                                         FROM users");

            UsersDataGridView.Columns["id"].Visible = false;

            UsersDataGridView.ClearSelection();
        }

        private object GetValueByColumnName(string columnName)
        {
            return UsersDataGridView.SelectedRows[0].Cells[columnName].Value;
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneUserIsSelected())
            {
                MessageBox.Show("At least one customer must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MultipleUsersAreSelected())
            {
                MessageBox.Show("Only one customer can be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UserExists() && (UsernameTextBox.Text != (string)GetValueByColumnName("phone number")))
            {
                MessageBox.Show("Entered customer already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"UPDATE users
                                          SET username = '{UsernameTextBox.Text}', password = '{PasswordTextBox.Text}', is_admin = '{IsAdminCheckBox.Checked}'
                                          WHERE id = {(int)GetValueByColumnName("id")}");

            LoadUsers();
            ResetForm();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneUserIsSelected()) return;

            var selectedUsers = UsersDataGridView.SelectedRows;

            var selectedUsersIds = from DataGridViewRow product in selectedUsers
                                   select product.Cells["id"].Value.ToString();


            Database.ExecuteSqlCommand($@"DELETE FROM users
                                          WHERE id IN ({string.Join(", ", selectedUsersIds)})");

            LoadUsers();
            ResetForm();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            UsersDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, name AS Name, price AS Price
                                                                         FROM users
                                                                         WHERE name LIKE '{SearchTextBox.Text}%'");
        }
    }
}
