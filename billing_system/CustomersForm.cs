using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;

namespace billing_system
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }

        private void LoadCustomers()
        {
            CustomersDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, CONCAT(first_name,' ',last_name) AS Name, phone_number AS 'Phone Number', email AS Email
                                                                             FROM customers");

            CustomersDataGridView.Columns["id"].Visible = false;

            CustomersDataGridView.ClearSelection();
        }

        private object GetValueByColumnName(string columnName)
        {
            return CustomersDataGridView.SelectedRows[0].Cells[columnName].Value;
        }

        private bool EmailIsUsed()
        {
            var customers = Database.ExecuteSqlCommand("SELECT * FROM customers").Rows;

            foreach (DataRow customer in customers)
                if ((string)customer["email"] == EmailTextBox.Text)
                    return true;
            return false;
        }

        private bool EmailIsValid()
        {
            try
            {
                return new MailAddress(EmailTextBox.Text).Address == EmailTextBox.Text;
            }
            catch
            {
                return false;
            }
        }


        private bool PhoneNumberIsUsed()
        {
            var customers = Database.ExecuteSqlCommand("SELECT * FROM customers").Rows;

            foreach (DataRow customer in customers)
                if ((string)customer["phone_number"] == PhoneNumberTextBox.Text)
                    return true;
            return false;
        }

        private bool PhoneNumberIsValid()
        {
            return PhoneNumberTextBox.MaskFull;
        }

        private IEnumerable<string> GetErrors()
        {
            var errors = new List<(bool Error, string Message)>
            {
                (string.IsNullOrWhiteSpace(FirstNameTextBox.Text), "First name cannot be empty."),
                (string.IsNullOrWhiteSpace(LastNameTextBox.Text), "Last name cannot be empty."),
                (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text), "Phone number cannto be empty."),
                (string.IsNullOrWhiteSpace(EmailTextBox.Text), "Email cannot be empty."),
            };

            if (!string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
                errors.Add((!PhoneNumberIsValid(), "Entered phone number is not in the correct format."));

            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
                errors.Add((!EmailIsValid(), "Entered email is not in the correct format."));

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

        private void ResetForm()
        {
            FirstNameTextBox.Text = null;
            LastNameTextBox.Text = null;
            PhoneNumberTextBox.Text = null;
            EmailTextBox.Text = null;
        }

        private bool AtLeastOneCustomerIsSelected()
        {
            if (CustomersDataGridView.SelectedRows.Count > 0) return true;
            return false;
        }

        private bool MultipleCustomersAreSelected()
        {
            if (CustomersDataGridView.SelectedRows.Count <= 1) return false;
            return true;
        }


        private void CustomersForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (EmailIsUsed())
            {
                MessageBox.Show("Entered email is already in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PhoneNumberIsUsed())
            {
                MessageBox.Show("Entered phone number is already in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"INSERT INTO customers (first_name, last_name, phone_number, email)
                                          VALUES ('{FirstNameTextBox.Text}', '{LastNameTextBox.Text}', '{PhoneNumberTextBox.Text}', '{EmailTextBox.Text}')");

            LoadCustomers();
            ResetForm();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneCustomerIsSelected())
            {
                MessageBox.Show("At least one customer must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MultipleCustomersAreSelected())
            {
                MessageBox.Show("Only one customer can be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (EmailIsUsed() && EmailTextBox.Text != (string)GetValueByColumnName("email"))
            {
                MessageBox.Show("Entered email is already in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (PhoneNumberIsUsed() && (PhoneNumberTextBox.Text != (string)GetValueByColumnName("phone number")))
            {
                MessageBox.Show("Entered phone number is already in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"UPDATE customers
                                          SET first_name = '{FirstNameTextBox.Text}', last_name = '{LastNameTextBox.Text}', phone_number = '{PhoneNumberTextBox.Text}', email = '{EmailTextBox.Text}'
                                          WHERE id = {(int)GetValueByColumnName("id")}");

            LoadCustomers();
            ResetForm();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneCustomerIsSelected())
            {
                MessageBox.Show("At least one customer must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedCustomers = CustomersDataGridView.SelectedRows;

            var selectedCustomersIds = from DataGridViewRow product in selectedCustomers
                                       select product.Cells["id"].Value.ToString();

            Database.ExecuteSqlCommand($@"DELETE FROM customers
                                          WHERE id IN ({string.Join(", ", selectedCustomersIds)})");

            LoadCustomers();
            ResetForm();
        }

        private void CustomersDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (CustomersDataGridView.SelectedRows.Count > 1 || CustomersDataGridView.SelectedRows.Count < 1)
            {
                ResetForm();
                return;
            }

            FirstNameTextBox.Text = ((string)GetValueByColumnName("name")).Split(' ')[0];
            LastNameTextBox.Text = ((string)GetValueByColumnName("name")).Split(' ')[1];
            PhoneNumberTextBox.Text = (string)GetValueByColumnName("phone number");
            EmailTextBox.Text = (string)GetValueByColumnName("email");

        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            CustomersDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, CONCAT(first_name,' ',last_name) AS Name, phone_number AS 'Phone Number', email AS Email
                                                                            FROM customers
                                                                            WHERE CONCAT(first_name,' ',last_name) LIKE '{SearchTextBox.Text}%'");
        }
    }
}
