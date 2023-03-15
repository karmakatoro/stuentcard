using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentCardGenerate
{
    public partial class Main : Form
    {
        AddStudent addstudent;
        EditStudent editstudent;
        public Main()
        {
            InitializeComponent();
            addstudent = new AddStudent(this);
            editstudent = new EditStudent(this);
        }
        public void display()
        {
            string query = "SELECT id,nom,post_nom,prenom,section,promotion FROM t_student";
            StudentController.displayAndSearch(query, dataGridView);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dialogresult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            addstudent.clear();
            addstudent.ShowDialog();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                
                editstudent.id = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                editstudent.nom = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                editstudent.post_nom = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                editstudent.prenom = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                editstudent.section = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                editstudent.promotion = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                editstudent.fillFields();
                editstudent.ShowDialog();
            }else if(e.ColumnIndex == 2)
            {
                string studid = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                string studname = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                string studpost_name = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString(); ;
                DialogResult dialog = MessageBox.Show("Are you sure to delete " + studname + " " + studpost_name + "?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialog == DialogResult.Yes)
                {
                    StudentController.deleteStudent(studname, studid);
                    display();
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            display();
        }
    }
}
