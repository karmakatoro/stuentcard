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
    public partial class EditStudent : Form
    {
        private readonly AddStudent _parent;
        public int id;
        public string nom, post_nom, prenom, section, promotion;

        private void btnSave_Click(object sender, EventArgs e)
        {
            _parent.save();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            _parent.addImage();
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            _parent.addImage();
        }

        public EditStudent(AddStudent parent)
        {
            InitializeComponent();
            _parent = parent;
        }
        private void btnErase_Click(object sender, EventArgs e)
        {
            _parent.clear();
        }
    }
}
