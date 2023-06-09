﻿using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace StudentCardGenerate
{
    public partial class Main : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );
        AddStudent addstudent;
        EditStudent editstudent;
        StusentCardGenerate.ShowStudent showstudent;
        public Main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            addstudent = new AddStudent(this);
            editstudent = new EditStudent(this);
            showstudent = new StusentCardGenerate.ShowStudent(this);
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
            DialogResult dialogresult = MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
            if(e.ColumnIndex == 0){
                showstudent.id = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                showstudent.nom = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                showstudent.post_nom = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                showstudent.prenom = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                showstudent.section = dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                showstudent.promotion = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                showstudent.initInfos();
                showstudent.ShowDialog();
                
            }
            else if(e.ColumnIndex == 1)
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
                DialogResult dialog = MessageBox.Show("Are you sure to delete " + studname + " " + studpost_name + "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            string toSearch = textSearch.Text.Trim();
            string query = "SELECT id,nom,post_nom,prenom,section,promotion FROM t_student WHERE nom LIKE'%" + toSearch + "%' OR post_nom LIKE'%" + toSearch + "%' OR prenom LIKE'%" + toSearch + "%'";
            StudentController.displayAndSearch(query, dataGridView);
        }
    }
}
