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
    abstract class Document { }

    class ImageDocument : Document
    {
        public PictureBox pBox;
    }

    abstract class OperationWithImage
    {
        abstract public ImageDocument operation(ImageDocument imgDoc);
    }

    class OpenImage : OperationWithImage
    {

        public override ImageDocument operation(ImageDocument imgDoc)
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
    }

    class ClearImage : OperationWithImage
    {
        public override ImageDocument operation(ImageDocument imgDoc)
        {
            imgDoc.pBox.Image = null;
            return imgDoc;
        }
    }
}