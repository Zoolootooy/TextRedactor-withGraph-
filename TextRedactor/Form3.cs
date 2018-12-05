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
    public partial class Form3 : Form
    {
        ImageDocument img = new ImageDocument();
        private static Form3 instance; //ссылка на конкретный экземпляр
        private static int MaxCountWindow = 10; //максимальное число экземпляров
        private static int countWindow = 0; //текущее число экземпляров
        protected Form3() //защищенный конструктор
        {
            InitializeComponent();
            img.pBox = pictureBox1;
        }
        public static Form3 getInstance() //метод, позволяющий создать экземпляр
        {
            if (countWindow < MaxCountWindow)
            {
                instance = new Form3();
                countWindow++;
            }
            else MessageBox.Show("That's the max number of windows: " + MaxCountWindow, "Ошибка");
            return instance;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationWithImage operations = new OpenImage();
            operations.operation(img);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationWithImage operations = new ClearImage();
            operations.operation(img);
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            countWindow--;
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
