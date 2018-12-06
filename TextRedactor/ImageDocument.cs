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
    public class Document : Form
    {
       
    }
    /*
    class ImageDocument : Document
    {
        public PictureBox pBox;

        public ImageDocument Open(ImageDocument imgDoc)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = new System.IO.FileStream(openDialog.FileName, System.IO.FileMode.Open);
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                
                fs.Close();
                imgDoc.pBox.Image = img;
            }
            return imgDoc;
        }

        public ImageDocument Clear(ImageDocument imgDoc)
        {
            imgDoc.pBox.Image = null;
            return imgDoc;
        }
    }

    class TextDocument : Document
    {
        public RichTextBox rtBox;

        public TextDocument Open(TextDocument textDoc)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(openDialog.FileName, System.IO.FileMode.Open);
                    RichTextBox rtb = new RichTextBox();
                    richTextBox1.LoadFile(openFileDialog1.FileName,
                        RichTextBoxStreamType.RichText);//Пытаемся загрузить текст как ртф
                }
                catch (System.ArgumentException ex)
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    //пытаемся загрузить как обычный текст
                }

                this.Text = openFileDialog1.FileName; // Заголовок окна = имя файла
            }
            return textDoc;
        }

        public TextDocument Clear(TextDocument textDoc)
        {
            textDoc.rtBox.Text = null;
            return textDoc;
        }
    }
    */

    abstract class Creator
    {
        public abstract Document FactoryMethod();
    }

    class ConcreteCreatorImage : Creator
    {
        public override Document FactoryMethod()
        {
            return new Form3();
        }
    }

    class ConcreteCreatorText : Creator
    {
        public override Document FactoryMethod()
        {
            return Form1.getInstance();
        }
    }
}