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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public bool AdminIsLoggedIn { get; set; }

        private void ProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            if (new ProductsForm().ShowDialog() == DialogResult.Cancel)
                Show();
        }

        private void CustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            if (new CustomersForm().ShowDialog() == DialogResult.Cancel)
                Show();
        }

        private void BillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            if (new BillingForm().ShowDialog() == DialogResult.Cancel)
                Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!AdminIsLoggedIn)
                usersToolStripMenuItem.Visible = false;
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();

            var usersForm = new UsersForm();

            if (usersForm.ShowDialog() == DialogResult.Cancel)
                Show();
        }
    }
}
