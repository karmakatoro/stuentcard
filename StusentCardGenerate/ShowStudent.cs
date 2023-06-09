﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using QRCoder;
using System.Runtime.InteropServices;
namespace StusentCardGenerate
{
    public partial class ShowStudent : Form
    {
        private readonly StudentCardGenerate.Main _parent;
        public  string id, nom, post_nom, prenom, section, promotion;
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


        private void print(Panel panel)
        {
            PrinterSettings printersettings = new PrinterSettings();
            getPrintArea(panel);
            printPreviewDialog.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printPreviewDialog.ShowDialog(); 
        }
        private Bitmap bitmap;
        private void getPrintArea(Panel panel)
        {
            bitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bitmap, new Rectangle(0, 0, panel.Width, panel.Height));

        }
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rectangle = e.PageBounds;
            e.Graphics.DrawImage(bitmap, (rectangle.Width) - (this.panCard.Width * 2), (this.panCard.Location.X /2));
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            print(panCard);
        }

        private void panMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panCard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShowStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.Opacity = 1;
        }

        public ShowStudent(StudentCardGenerate.Main parent)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            _parent = parent;
        }
        public void initInfos()
        {
            textName.Text = nom;
            textSecond.Text = post_nom;
            textPrename.Text = prenom;
            textPromotion.Text = promotion;
            textSection.Text = section;

            textCardId.Text = "00000" + id;
            textCardNames.Text = nom + " "+ " "+post_nom +" "+ prenom;
            textCardPromotion.Text = promotion;
            textCardSection.Text = section;
            textCardRollNumber.Text = "21IGGJ"+id;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string hashString(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
           
            using (var sha = new System.Security.Cryptography.SHA1Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;

            }
        }
        private void ShowStudent_Load(object sender, EventArgs e)
        {
            _parent.Opacity = 0.9;
            StudentCardGenerate.StudentController.getImage(id, picImage, "image");
            StudentCardGenerate.StudentController.getImage(id, picCardImage, "image");

            int idHash = id.GetHashCode();
            string studCode = hashString(Convert.ToString(idHash));
            QRCoder.QRCodeGenerator QG = new QRCoder.QRCodeGenerator();
            var MyData = QG.CreateQrCode("21IGGJ"+id, QRCodeGenerator.ECCLevel.H);
            var code = new QRCoder.QRCode(MyData);
            picCardQR.Image = code.GetGraphic(50);
        }
    }
}
