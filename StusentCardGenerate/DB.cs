using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace StudentCardGenerate
{
    class DB
    {
        public static MySqlConnection getConnection()
        {
            string query = "datasource=localhost;port=3306;username=root;password=;database=db_student";
            MySqlConnection con = new MySqlConnection(query);
            try
            {
                con.Open();
            }
            catch (MySqlException)
            {

                MessageBox.Show("Could not connect to the database", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }
    }
}
