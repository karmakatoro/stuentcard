﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
namespace StudentCardGenerate
{
    public partial class AddStudent : Form
    {
        private readonly Main _parent;
        public int id;
        public string nom, post_nom, prenom, section, promotion;
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

        public void toUpdate()
        {
            
        }
        public void clear()
        {
            textName.Text = textSecond.Text = textPrename.Text = textSection.Text = textPromotion.Text = string.Empty;
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            addImage();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            addImage();
        }

        public void addImage()
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                picImage.Image = Image.FromFile(openfiledialog.FileName);
            }
        }
        public byte[] imgProcess()
        {
            MemoryStream memorystream = new MemoryStream();
            picImage.Image.Save(memorystream, picImage.Image.RawFormat);
            byte[] img = memorystream.ToArray();
            return img;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            _parent.Opacity = 0.9;
        }

        private void AddStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.Opacity = 1;
        }

        public void save()
        {
            string nom = textName.Text.Trim();
            string post_nom = textSecond.Text.Trim();
            string prenom = textPrename.Text.Trim();
            string promotion = textPromotion.Text.Trim();
            string section = textSection.Text.Trim();
            byte[] image = imgProcess();
            if (nom == "" || post_nom == "" || prenom == "" || promotion == "" || section == "")
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
                StudentModel studentmodel = new StudentModel(nom,post_nom,prenom,promotion,section);
                StudentController.addStudent(studentmodel, image);
                clear();
                _parent.display();
            }
        }
        private void btnErase_Click(object sender, EventArgs e)
        {
            clear();
        }

        public AddStudent(Main parent)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            _parent = parent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
    }
}
