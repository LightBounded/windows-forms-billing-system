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
    public partial class AddProductForm : Form
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        public (int Id, string Name, int Quantity, decimal Price) ProductToAdd { get; set; }

        private object GetValueByColumnName(string columnName)
        {
            return ProductsDataGridView.SelectedRows[0].Cells[columnName].Value;
        }

        private void LoadProducts()
        {
            ProductsDataGridView.DataSource = Database.ExecuteSqlCommand("SELECT id, name AS Name, price AS Price FROM products");
            ProductsDataGridView.ClearSelection();
        }

        private void AddProductsForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private IEnumerable<string> GetErrors()
        {
            var errors = new List<(bool Error, string Message)>
            {
                (ProductsDataGridView.SelectedRows.Count < 1, "One product must be selected."),
                ((int)QuantityNumericUpDown.Value < 1, "Quantity must be greater than 0.")
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            var id = (int)GetValueByColumnName("id");
            var name = (string)GetValueByColumnName("name");
            var quantity = (int)QuantityNumericUpDown.Value;
            var price = (decimal)GetValueByColumnName("price") * quantity;

            if (!FormIsValid())
            {
                MessageBox.Show(string.Join("\n", GetErrors()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProductToAdd = (id, name, quantity, price);
            DialogResult = DialogResult.OK;
        }
    }
}
