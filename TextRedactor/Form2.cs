using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace TextRedactor
{
    public partial class Form2 : Form
    {
        public Form2(string data1)
        {
            InitializeComponent();

            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            richTextBox1.Text = data1;
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }

            this.data = data1;


            StringBuilder ss = new StringBuilder("");
            ss.Append(data[0]);


            for (int i = 1; i < data.Length; i++)
            {
                if (!((data[i] == data [i-1]) && (data[i] == ' ')))
                {
                    ss.Append(data[i]);
                }
            }

            data = ss.ToString();
            data = data.Replace("\n\n", "\n").Replace("\0", "").Replace("	", " ").Replace("\t", "");
            this.richTextBox1.Text = data;
        }
        string data, data1;

        private void returnWithoutChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void saveThisTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            this.Close();
        }
            
    }
}
