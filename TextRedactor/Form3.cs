using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TextRedactor
{
    public partial class Form3 : Document
    {
        public Form3() //защищенный конструктор
        {
            InitializeComponent();
        }

        


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image file(*.bmp)|*.bmp|Image file (*.jpg)|*.jpg|All files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(openFileDialog1.FileName,
                    System.IO.FileMode.Open);
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                fs.Close();
                pictureBox1.Image = img;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
