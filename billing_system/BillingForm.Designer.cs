
namespace billing_system
{
    partial class BillingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProductsDataGridView = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomersComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddProductButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SubtotalTextBox = new System.Windows.Forms.TextBox();
            this.PaymentMethodComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.RemoveProductButton = new System.Windows.Forms.Button();
            this.ClearProductsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductsDataGridView
            // 
            this.ProductsDataGridView.AllowUserToAddRows = false;
            this.ProductsDataGridView.AllowUserToDeleteRows = false;
            this.ProductsDataGridView.AllowUserToResizeColumns = false;
            this.ProductsDataGridView.AllowUserToResizeRows = false;
            this.ProductsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.product,
            this.quantity,
            this.price});
            this.ProductsDataGridView.Location = new System.Drawing.Point(37, 140);
            this.ProductsDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProductsDataGridView.Name = "ProductsDataGridView";
            this.ProductsDataGridView.ReadOnly = true;
            this.ProductsDataGridView.RowHeadersVisible = false;
            this.ProductsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductsDataGridView.Size = new System.Drawing.Size(501, 283);
            this.ProductsDataGridView.TabIndex = 4;
            this.ProductsDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ProductsDataGridView_RowsAdded);
            this.ProductsDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ProductsDataGridView_RowsRemoved);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // product
            // 
            this.product.HeaderText = "Product";
            this.product.Name = "product";
            this.product.ReadOnly = true;
            // 
            // quantity
            // 
            this.quantity.HeaderText = "Quantity";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            // 
            // price
            // 
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // CustomersComboBox
            // 
            this.CustomersComboBox.FormattingEnabled = true;
            this.CustomersComboBox.Location = new System.Drawing.Point(167, 31);
            this.CustomersComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomersComboBox.Name = "CustomersComboBox";
            this.CustomersComboBox.Size = new System.Drawing.Size(371, 29);
            this.CustomersComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 18;
            this.label1.Text = "Customer";
            // 
            // TotalTextBox
            // 
            this.TotalTextBox.Location = new System.Drawing.Point(471, 484);
            this.TotalTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TotalTextBox.Name = "TotalTextBox";
            this.TotalTextBox.ReadOnly = true;
            this.TotalTextBox.Size = new System.Drawing.Size(67, 29);
            this.TotalTextBox.TabIndex = 22;
            this.TotalTextBox.Text = "0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(423, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "Total";
            // 
            // AddProductButton
            // 
            this.AddProductButton.Location = new System.Drawing.Point(182, 442);
            this.AddProductButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddProductButton.Name = "AddProductButton";
            this.AddProductButton.Size = new System.Drawing.Size(172, 28);
            this.AddProductButton.TabIndex = 5;
            this.AddProductButton.Text = "Add Product";
            this.AddProductButton.UseVisualStyleBackColor = true;
            this.AddProductButton.Click += new System.EventHandler(this.AddProductsButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(37, 442);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(83, 28);
            this.ResetButton.TabIndex = 8;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 21);
            this.label3.TabIndex = 30;
            this.label3.Text = "Subtotal";
            // 
            // SubtotalTextBox
            // 
            this.SubtotalTextBox.Location = new System.Drawing.Point(471, 443);
            this.SubtotalTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubtotalTextBox.Name = "SubtotalTextBox";
            this.SubtotalTextBox.ReadOnly = true;
            this.SubtotalTextBox.Size = new System.Drawing.Size(67, 29);
            this.SubtotalTextBox.TabIndex = 29;
            this.SubtotalTextBox.Text = "0.00";
            // 
            // PaymentMethodComboBox
            // 
            this.PaymentMethodComboBox.FormattingEnabled = true;
            this.PaymentMethodComboBox.Items.AddRange(new object[] {
            "Cash\t",
            "Credit",
            "Debit"});
            this.PaymentMethodComboBox.Location = new System.Drawing.Point(167, 81);
            this.PaymentMethodComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PaymentMethodComboBox.Name = "PaymentMethodComboBox";
            this.PaymentMethodComboBox.Size = new System.Drawing.Size(371, 29);
            this.PaymentMethodComboBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 21);
            this.label5.TabIndex = 34;
            this.label5.Text = "Payment Method";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(455, 535);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(83, 28);
            this.SubmitButton.TabIndex = 9;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // RemoveProductButton
            // 
            this.RemoveProductButton.Location = new System.Drawing.Point(182, 483);
            this.RemoveProductButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RemoveProductButton.Name = "RemoveProductButton";
            this.RemoveProductButton.Size = new System.Drawing.Size(81, 28);
            this.RemoveProductButton.TabIndex = 7;
            this.RemoveProductButton.Text = "Remove";
            this.RemoveProductButton.UseVisualStyleBackColor = true;
            this.RemoveProductButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ClearProductsButton
            // 
            this.ClearProductsButton.Location = new System.Drawing.Point(273, 482);
            this.ClearProductsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearProductsButton.Name = "ClearProductsButton";
            this.ClearProductsButton.Size = new System.Drawing.Size(81, 28);
            this.ClearProductsButton.TabIndex = 35;
            this.ClearProductsButton.Text = "Clear";
            this.ClearProductsButton.UseVisualStyleBackColor = true;
            this.ClearProductsButton.Click += new System.EventHandler(this.ClearProductsButton_Click);
            // 
            // BillingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 583);
            this.Controls.Add(this.ClearProductsButton);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PaymentMethodComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SubtotalTextBox);
            this.Controls.Add(this.RemoveProductButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.AddProductButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TotalTextBox);
            this.Controls.Add(this.CustomersComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductsDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BillingForm";
            this.Text = "Billing";
            this.Load += new System.EventHandler(this.BillingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProductsDataGridView;
        private System.Windows.Forms.ComboBox CustomersComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TotalTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddProductButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SubtotalTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn product;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.ComboBox PaymentMethodComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Button RemoveProductButton;
        private System.Windows.Forms.Button ClearProductsButton;
    }
}