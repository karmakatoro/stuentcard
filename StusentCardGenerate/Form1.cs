﻿using System;
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
        public Main()
        {
            InitializeComponent();
            addstudent = new AddStudent(this);
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
    }
}
