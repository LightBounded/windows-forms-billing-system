using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace billing_system
{
    static class Database
    {
        private readonly static string ConnectionString = $@"server=localhost;database=billing_system;uid=root;pwd=root";

        private readonly static MySqlConnection Connection = new MySqlConnection(ConnectionString);


        public static DataTable ExecuteSqlCommand(string queryString)
        {
            var command = new MySqlCommand(queryString, Connection);
            var dataTable = new DataTable();
            try
            {
                Connection.Open();
                new MySqlDataAdapter(command).Fill(dataTable);
                Connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

    }
}
