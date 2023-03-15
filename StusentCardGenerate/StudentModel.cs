using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StusentCardGenerate
{
    class StudentModel
    {
        private string nom;
        private string post_nom;
        private string prenom;
        private string section;
        private string promotion;

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public string Post_nom
        {
            get
            {
                return post_nom;
            }

            set
            {
                post_nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;
            }
        }

        public string Section
        {
            get
            {
                return section;
            }

            set
            {
                section = value;
            }
        }

        public string Promotion
        {
            get
            {
                return promotion;
            }

            set
            {
                promotion = value;
            }
        }

        public StudentModel(string nom, string post_nom, string prenom, string section, string promotion)
        {
            this.nom = nom;
            this.post_nom = post_nom;
            this.prenom = prenom;
            this.section = section;
            this.promotion = promotion;
        }
    }
}
