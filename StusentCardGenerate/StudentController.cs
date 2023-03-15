using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace StudentCardGenerate
{
    class StudentController
    {
        public static void addStudent(StudentModel student, byte[] img)
        {
            string query = "INSERT INTO t_student (nom,post_nom,prenom,section,promotion,image) VALUES (@nom,@post_nom,@prenom,@section,@promotion,@image)";
            MySqlConnection con = DB.getConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = student.Nom;
            cmd.Parameters.Add("@post_nom", MySqlDbType.VarChar).Value = student.Post_nom;
            cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = student.Prenom;
            cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = student.Section;
            cmd.Parameters.Add("@promotion", MySqlDbType.VarChar).Value = student.Promotion;
            cmd.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(student.Nom + " added successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Student not inserted!!! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
        public static void updateStudent(StudentModel student, string id, byte[] img)
        {
            string query = "UPDATE t_student SET nom = @nom,post_nom = @post_nom, prenom = @prenom, section = @section, promotion = @promotion, image = @image WHERE id= @id";
            MySqlConnection con = DB.getConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = student.Nom;
            cmd.Parameters.Add("@post_nom", MySqlDbType.VarChar).Value = student.Post_nom;
            cmd.Parameters.Add("@prenom", MySqlDbType.VarChar).Value = student.Prenom;
            cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = student.Section;
            cmd.Parameters.Add("@promotion", MySqlDbType.VarChar).Value = student.Promotion;
            cmd.Parameters.Add("@image", MySqlDbType.LongBlob).Value = img;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(student.Nom + " updated successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Student not updated!!! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }
        public static void deleteStudent(string studname, string id)
        {
            string query = "DELETE FROM t_student WHERE id = @id";
            MySqlConnection con = DB.getConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(studname + " deleted successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Student not deleted!!! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void displayAndSearch(string query, DataGridView dtv)
        {
            try
            {
                MySqlConnection con = DB.getConnection();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataAdapter msdp = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                msdp.Fill(table);
                dtv.DataSource = table;
                con.Close();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("An error occured!!! \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void getImage(string id, PictureBox image, string row)
        {
            try
            {
                MySqlConnection con = DB.getConnection();
                string query = "SELECT image FROM t_student WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                MySqlDataReader data;
                data = cmd.ExecuteReader();


                while (data.Read())
                {

                    byte[] img = (byte[])(data[row]);
                    if (img == null)
                    {
                        image.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        image.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                con.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("" + ex);
            }

        }
    }
}
