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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        public string Date { get; set; } 

        public string Customer { get; set; }

        public string PaymentMethod { get; set; }

        public string Subtotal { get; set; }

        public string Total { get; set; }

        public DataGridViewRowCollection Products { get; set; }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            DateTextBox.Text = Date;
            CustomerTextBox.Text = Customer;
            PaymentMethodTextBox.Text = PaymentMethod;

            foreach (DataGridViewRow product in Products)
                ProductsDataGridView.Rows.Add(product.Cells["id"], product.Cells["product"].Value, product.Cells["quantity"].Value, product.Cells["price"].Value);

            SubtotalTextBox.Text = Subtotal;
            TotalTextBox.Text = Total;
        }
    }
}
