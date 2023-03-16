using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StusentCardGenerate
{
    public partial class ShowStudent : Form
    {
        private readonly StudentCardGenerate.Main _parent;
        public string id, nom, post_nom, prenom, section, promotion;
        Bitmap toPrint;
        public void screenShot()
        {
            
            toPrint = new Bitmap(this.Height, this.Width);
            this.DrawToBitmap(toPrint, new Rectangle(0, 0, this.Width, this.Height));
        }
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rectangle = e.PageBounds;
            e.Graphics.DrawImage(toPrint, rectangle);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            screenShot();
            printPreviewDialog.Show();
        }

        public ShowStudent(StudentCardGenerate.Main parent)
        {
            InitializeComponent();
            _parent = parent;
        }
        public void initInfos()
        {
            textName.Text = nom;
            textSecond.Text = post_nom;
            textPrename.Text = prenom;
            textPromotion.Text = promotion;
            textSection.Text = section;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowStudent_Load(object sender, EventArgs e)
        {
            StudentCardGenerate.StudentController.getImage(id, picImage, "image");
        }
    }
}
