using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace StudentCardGenerate
{
    public partial class EditStudent : Form
    {
        private readonly Main _parent;
        public string id, nom, post_nom, prenom, section, promotion;
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

        public void fillFields()
        {
            textName.Text = nom;
            textSecond.Text = post_nom;
            textPrename.Text = prenom;
            textSection.Text = section;
            textPromotion.Text = promotion;

            StudentController.getImage(id, picImage, "image");
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
            if (nom == "" || post_nom == "" || prenom == "" || promotion == "" || section == "")
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
                StudentModel studentmodel = new StudentModel(nom, post_nom, prenom, promotion, section);
                StudentController.updateStudent(studentmodel,id,image);
                clear();
                _parent.display();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        private void EditStudent_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            addImage();
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            addImage();
        }
        public void clear()
        {
            textName.Text = textSecond.Text = textPrename.Text = textSection.Text = textPromotion.Text = string.Empty;
        }
        public EditStudent(Main parent)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            _parent = parent;
        }
        private void btnErase_Click(object sender, EventArgs e)
        {
           clear();
        }
    }
}
