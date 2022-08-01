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
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
        }

        private void LoadProducts()
        {
            ProductsDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, name AS Name, price AS Price
                                                                            FROM products");

            ProductsDataGridView.Columns["id"].Visible = false;

            ProductsDataGridView.ClearSelection();
        }

        private object GetValueByColumnName(string columnName)
        {
            return ProductsDataGridView.SelectedRows[0].Cells[columnName].Value;
        }

        private bool ProductExists()
        {
            var products = Database.ExecuteSqlCommand("SELECT * FROM products").Rows;

            foreach (DataRow product in products)
                if ((string)product["name"] == NameTextBox.Text)
                    return true;
            return false;
        }

        private IEnumerable<string> GetErrors()
        {
            var errors = new List<(bool Error, string Message)>
            {
                (string.IsNullOrWhiteSpace(NameTextBox.Text), "Name cannot be empty."),
                (string.IsNullOrWhiteSpace(PriceTextBox.Text), "Price cannot be empty."),
            };

            if (!string.IsNullOrWhiteSpace(PriceTextBox.Text))
                errors.Add((!decimal.TryParse(PriceTextBox.Text, out _), "Entered price is invalid."));

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
            NameTextBox.Text = null;
            PriceTextBox.Text = null;
            ProductsDataGridView.ClearSelection();
        }

        private bool AtLeastOneProductIsSelected()
        {
            if (ProductsDataGridView.SelectedRows.Count > 0) return true;
            return false;
        }

        private bool MultipleProductsAreSelected()
        {
            if (ProductsDataGridView.SelectedRows.Count <= 1) return false;
            return true;
        }


        private void ProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ProductExists())
            {
                MessageBox.Show("Entered product already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database.ExecuteSqlCommand($@"INSERT INTO products (name, price)
                                          VALUES ('{NameTextBox.Text}', {PriceTextBox.Text})");

            LoadProducts();
            ResetForm();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneProductIsSelected())
            {
                MessageBox.Show("At least one product must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MultipleProductsAreSelected())
            {
                MessageBox.Show("Only one product can be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ProductExists() && NameTextBox.Text != (string)GetValueByColumnName("name"))
            {
                MessageBox.Show("Entered product already exists");
                return;
            }

            Database.ExecuteSqlCommand($@"UPDATE products
                                          SET name = '{NameTextBox.Text}', price = {PriceTextBox.Text}
                                          WHERE id = {(int)GetValueByColumnName("id")}");

            LoadProducts();
            ResetForm();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!AtLeastOneProductIsSelected())
            {
                MessageBox.Show("At least one product must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedProducts = ProductsDataGridView.SelectedRows;

            var selectedProductIds = from DataGridViewRow product in selectedProducts
                                     select product.Cells["id"].Value.ToString();


            Database.ExecuteSqlCommand($@"DELETE FROM products
                                          WHERE id IN ({string.Join(", ", selectedProductIds)})");

            LoadProducts();
            ResetForm();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void PriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            ProductsDataGridView.DataSource = Database.ExecuteSqlCommand($@"SELECT id, name AS Name, price AS Price
                                                                            FROM products
                                                                            WHERE name LIKE '{SearchTextBox.Text}%'");
        }

        private void ProductsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ProductsDataGridView.SelectedRows.Count > 1 || ProductsDataGridView.SelectedRows.Count < 1)
            {
                ResetForm();
                return;
            }

            NameTextBox.Text = (string)GetValueByColumnName("name");
            PriceTextBox.Text = ((decimal)GetValueByColumnName("price")).ToString();
        }
    }
}
