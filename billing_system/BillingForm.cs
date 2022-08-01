using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace billing_system
{
    public partial class BillingForm : Form
    {
        public BillingForm()
        {
            InitializeComponent();
        }

        private void AddProductsButton_Click(object sender, EventArgs e)
        {
            var addProductForm = new AddProductForm();

            if (addProductForm.ShowDialog() == DialogResult.OK)
            {
                var product = addProductForm.ProductToAdd;
                ProductsDataGridView.Rows.Add(product.Id, product.Name, product.Quantity, product.Price);
            }
        }

        private void ResetForm()
        {
            CustomersComboBox.SelectedIndex = -1;
            PaymentMethodComboBox.SelectedIndex = -1;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            ProductsDataGridView.Rows.Remove(ProductsDataGridView.SelectedRows[0]);
        }

        private void CalculateTotals()
        {
            if (ProductsDataGridView.Rows.Count < 1)
            {
                SubtotalTextBox.Text = "0.00";
                TotalTextBox.Text = "0.00";
                return;
            }
            var subtotal = ProductsDataGridView.Rows.Cast<DataGridViewRow>().Sum(product => (decimal)product.Cells["price"].Value);
            var total = subtotal * 1.07M;

            SubtotalTextBox.Text = subtotal.ToString();
            TotalTextBox.Text = total.ToString();
        }

   

        private void ProductsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateTotals();
        }

        private void ProductsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotals();
        }

        private IEnumerable<string> GetErrors()
        {
            var errors = new List<(bool Error, string Message)>
            {
                (CustomersComboBox.SelectedIndex == -1, "A customer must be selected."),
                (PaymentMethodComboBox.SelectedIndex == -1, "A payment method must be selected."),
                (ProductsDataGridView.Rows.Count < 1, "At least one product must be added."),
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

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"INSERT INTO orders (payment_method, date, total, customer_id)
                                          VALUES ('{PaymentMethodComboBox.GetItemText(PaymentMethodComboBox.SelectedItem)}', '{DateTime.Now:yyyy-MM-dd}', {decimal.Parse(TotalTextBox.Text)}, {CustomersComboBox.SelectedValue})");


            var orderId = Database.ExecuteSqlCommand($@"SELECT MAX(id) AS ID FROM orders").Rows[0]["id"];

            foreach (DataGridViewRow product in ProductsDataGridView.Rows)
                Database.ExecuteSqlCommand($@"INSERT INTO `orders-products` (order_id, product_id)
                                              VALUES ({(int)orderId}, {(int)product.Cells["id"].Value})");



            var reportForm = new ReportForm()
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Customer = CustomersComboBox.GetItemText(CustomersComboBox.SelectedItem),
                PaymentMethod = PaymentMethodComboBox.GetItemText(PaymentMethodComboBox.SelectedItem),
                Products = ProductsDataGridView.Rows,
                Subtotal = SubtotalTextBox.Text,
                Total = TotalTextBox.Text
            };
            reportForm.ShowDialog();

            ResetForm();
        }

        private void LoadCustomers()
        {
            CustomersComboBox.DataSource = Database.ExecuteSqlCommand($@"SELECT id, CONCAT(first_name,' ',last_name) AS 'Name' 
                                                                         FROM customers");

            CustomersComboBox.DisplayMember = "name";
            CustomersComboBox.ValueMember = "id";

            CustomersComboBox.SelectedIndex = -1;
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void ClearProductsButton_Click(object sender, EventArgs e)
        {
            ProductsDataGridView.Rows.Clear();
        }
    }
}
