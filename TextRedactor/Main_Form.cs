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
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();


        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document mdiChild = Form1.getInstance();
            mdiChild.MdiParent = this;
            mdiChild.Show();
        }

        private void closeAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] form = MdiChildren;
            foreach (Form f in form)
            {
                f.Close();
            }
        }

        private void newGraphWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document mdiChild = new Form3();
            mdiChild.MdiParent = this;
            mdiChild.Show();
        }
    }
}
