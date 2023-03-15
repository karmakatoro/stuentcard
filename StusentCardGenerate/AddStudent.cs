﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentCardGenerate
{
    public partial class AddStudent : Form
    {
        private readonly Main _parent;
        public int id;
        public string nom, post_nom, prenom, section, promotion;

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
        public void save()
        {
            string nom = textName.Text.Trim();
            string post_nom = textSecond.Text.Trim();
            string prenom = textPrename.Text.Trim();
            string promotion = textPromotion.Text.Trim();
            string section = textSection.Text.Trim();
            byte[] image = imgProcess();
            if (nom == "" || post_nom == "" || prenom == "" || promotion == "")
            {
                MessageBox.Show("All fields are required");
            }
        }
        private void btnErase_Click(object sender, EventArgs e)
        {
            clear();
        }

        public AddStudent(Main parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
