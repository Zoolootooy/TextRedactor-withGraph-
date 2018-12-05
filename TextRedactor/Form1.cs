using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace TextRedactor
{
    public partial class Form1 : Form
    {
        private static Form1 instance; //ссылка на конкретный экземпляр
        private static int MaxCountWindow = 10; //максимальное число экземпляров
        private static int countWindow = 0; //текущее число экземпляров
        protected Form1() //защищенный конструктор
        {
            InitializeComponent();
        }
        public static Form1 getInstance() //метод, позволяющий создать экземпляр
        {
            if (countWindow < MaxCountWindow)
            {
                instance = new Form1();
                countWindow++;
            }
            else MessageBox.Show("That's the max number of windows: " + MaxCountWindow, "Ошибка");
            return instance;
        }

        string sVowCyr = "аоиёеэыуюяАОИЁЕЭЫУЮЯ", 
            sConCyr = "цкнгшщзхфвпрлджчсмтбйЦКНГШЩЗХФВПРЛДЖЧСМТБЙ",
            sVowLat = "aeiouyAEIOUY", 
            sConLat = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ", 
            sDigits = "1234567890",
            sSpecSym = "/“”[](){}‘’@#$%^&*-+|=~_",
            sPunctuation = ".,:;!?—", 
            sCyrillic = "аоиёеэыуюяцкнгшщзхфвпрлджчсмтбйЦКНГШЩЗХФВПРЛДЖЧСМТБЙАОИЁЕЭЫУЮЯ",
            sLatin = "aeiouqwrtypsdfghjklzxcvbnmQWRTYPSDFGHJKLZXCVBNMAEIOU";

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private long FileSize()
        {
            StreamWriter write_text;  //Класс для записи в файл
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            FileInfo file = new FileInfo(pathTimeFile);
            write_text = file.AppendText(); //Дописываем инфу в файл, если файла не существует он создастся
            write_text.WriteLine(richTextBox1.Text); //Записываем в файл текст из текстового поля
            write_text.Close(); // Закрываем файл
            long size = file.Length-2;
            File.Delete(pathTimeFile);
            return size;
        }

        private long symbolsC()
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile, 
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }

            long charCount = 0;
            using (StreamReader sr = new StreamReader(pathTimeFile, 
                System.Text.Encoding.Unicode))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        charCount++;
                    }
                }
            }

            File.Delete(pathTimeFile);
            return charCount;
        }

        private int nullstrC()
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            int c = 0;
            using (StreamReader sr = new StreamReader(pathTimeFile,
                System.Text.Encoding.Unicode))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == String.Empty)
                    {
                        c++;
                    }
                }
            }
            File.Delete(pathTimeFile);
            return c;
        }

        private int indentC()
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            int c = 0;
            using (StreamReader sr = new StreamReader(pathTimeFile,
                System.Text.Encoding.Unicode))
            {
                string line;
                int f = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    StringBuilder ss = new StringBuilder("");
                    ss.Append(line);
                    int len = 0;
                    len = ss.Length;
                    for (int i = 0; i < len; i++)
                    {
                        if (ss[i] == ' ')
                        {
                            f++;
                        }
                        else f = 0;
                        if (f == 5)
                        {
                            c++;
                            f = 0;
                        }
                    }
                }
            }
            File.Delete(pathTimeFile);
            return c;
        }

        private int symbolsLikeThat(string str)
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
               "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            int c = 0;
            using (StreamReader sr = new StreamReader(pathTimeFile,
                System.Text.Encoding.Unicode))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    StringBuilder ss = new StringBuilder("");
                    StringBuilder ss1 = new StringBuilder("");
                    ss.Append(line);
                    ss1.Append(str);
                    int len = 0;
                    len = ss.Length;
                    int len1 = 0;
                    len1 = ss1.Length;
                    for (int i = 0; i < len; i++)
                    {
                        for (int j = 0; j < len1; j++)
                        {
                            if (ss[i] == ss1[j])
                            {
                                c++;
                            }
                        }
                    }
                }
            }
            File.Delete(pathTimeFile);
            return c;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
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
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                this.Text = saveFileDialog1.FileName;
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void CheckMenuCharacterStyle()
        {
            if (richTextBox1.SelectionFont.Bold == true)
                boldToolStripMenuItem.Checked = true;
            else boldToolStripMenuItem.Checked = false;

            if (richTextBox1.SelectionFont.Italic == true)
                italicToolStripMenuItem.Checked = true;
            else italicToolStripMenuItem.Checked = false;

            if (richTextBox1.SelectionFont.Underline == true)
                underlineToolStripMenuItem.Checked = true;
            else underlineToolStripMenuItem.Checked = false;

            

            if (richTextBox1.SelectionFont.Strikeout == true)
                strikeoutToolStripMenuItem.Checked = true;
            else strikeoutToolStripMenuItem.Checked = false;
        }

        private void styleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Bold == true)
                    newFontStyle = FontStyle.Regular;
                else
                    newFontStyle = FontStyle.Bold;

                richTextBox1.SelectionFont = new Font(
                    currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuCharacterStyle();
            }
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;
                CheckMenuCharacterStyle();

                if (richTextBox1.SelectionFont.Italic == true)
                    newFontStyle = FontStyle.Regular;
                else
                    newFontStyle = FontStyle.Italic;

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuCharacterStyle();
            }
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Font currentFont = richTextBox1.SelectionFont;
            System.Drawing.FontStyle newFontStyle;
            CheckMenuCharacterStyle();

            if (richTextBox1.SelectionFont.Underline == true)
                newFontStyle = FontStyle.Regular;
            else
                newFontStyle = FontStyle.Underline;

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily, currentFont.Size, newFontStyle);
            CheckMenuCharacterStyle();
        }

        

        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Font currentFont = richTextBox1.SelectionFont;
            System.Drawing.FontStyle newFontStyle;
            CheckMenuCharacterStyle();

            if (richTextBox1.SelectionFont.Strikeout == true)
                newFontStyle = FontStyle.Regular;
            else
                newFontStyle = FontStyle.Strikeout;

            richTextBox1.SelectionFont = new Font(
               currentFont.FontFamily, currentFont.Size, newFontStyle);
            CheckMenuCharacterStyle();
        }
        

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Size of this file: " + Convert.ToString(FileSize() / 1024) + "Kb");
        }

        private void symbolCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of symbols: " + Convert.ToString(richTextBox1.TextLength));
        }

        private void amountOfIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of indents: " + Convert.ToString(indentC()));
        }

        private void amountOfNullStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of null strings: " + Convert.ToString(nullstrC()));
        }


        private void amountOfAuthorsPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of author pages: " + Convert.ToString(indentC()/1800));
        }
        

        private void amountOfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of vowel letters for cyrillic: " + Convert.ToString(symbolsLikeThat(sVowCyr)));
        }

        private void amountOfConsonantLettersForCyrillicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of consonant letters for cyrillic: " + Convert.ToString(symbolsLikeThat(sConCyr)));
        }

        

        private void amountOfVowelLettersForLatinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of vowel letters for latin: " + Convert.ToString(symbolsLikeThat(sVowLat)));
        }
        

        private void amountOfConsonantLettersForLatinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of consonant letters for latin: " + Convert.ToString(symbolsLikeThat(sConLat)));
        }

        

        private void amountOfDigitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of digits: " + Convert.ToString(symbolsLikeThat(sDigits)));
        }

        

        private void amountOfSpecialSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of Special symbols: " + Convert.ToString(symbolsLikeThat(sSpecSym)));
        }

        private void amountOfPunctuationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of punctuation: " + Convert.ToString(symbolsLikeThat(sPunctuation)));
        }

        private void amountOfCyrillicLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of cyrillic letters: " + Convert.ToString(symbolsLikeThat(sCyrillic)));
        }

        private void amountOfLatinLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount of latin letters: " + Convert.ToString(symbolsLikeThat(sLatin)));
        }

        private void allStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Size of this file: " + Convert.ToString(FileSize() / 1024) + "Kb" + "\n" +
                "Amount of symbols: " + Convert.ToString(richTextBox1.TextLength) +"\n" +
                "Amount of indents: " + Convert.ToString(indentC()) +"\n" +
                "Amount of null strings: " + Convert.ToString(nullstrC()) +"\n" +
                "Amount of author pages: " + Convert.ToString(indentC() / 1800) +"\n" +
                "Amount of vowel letters for cyrillic: " + Convert.ToString(symbolsLikeThat(sVowCyr)) +"\n" +
                "Amount of consonant letters for cyrillic: " + Convert.ToString(symbolsLikeThat(sConCyr)) +"\n" +
                "Amount of vowel letters for latin: " + Convert.ToString(symbolsLikeThat(sVowLat)) +"\n" +
                "Amount of consonant letters for latin: " + Convert.ToString(symbolsLikeThat(sConLat)) +"\n" +
                "Amount of digits: " + Convert.ToString(symbolsLikeThat(sDigits)) + "\n" +
                "Amount of Special symbols: " + Convert.ToString(symbolsLikeThat(sSpecSym)) + "\n" +
                "Amount of punctuation: " + Convert.ToString(symbolsLikeThat(sPunctuation)) + "\n" +
                "Amount of cyrillic letters: " + Convert.ToString(symbolsLikeThat(sCyrillic)) + "\n" +
                "Amount of latin letters: " + Convert.ToString(symbolsLikeThat(sLatin))
                );
        }

        private void deleteExcessSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this.richTextBox1.Text);
            f.ShowDialog();
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            richTextBox1.Text = File.ReadAllText(pathTimeFile);
            File.Delete(pathTimeFile);
        }

        //________________________
        private string FunckRegex(string s)
        {
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            string file = File.ReadAllText(pathTimeFile, Encoding.Unicode);
            string s1 = Convert.ToString(Regex.Match(file, s));
            File.Delete(pathTimeFile);
            return s1;
        }

        

        private void firstPlaceWithSurnameNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg1 = @"[A-ZА-Я]{1}[a-zа-я]{1,20} [A-ZА-Я]{1}[a-zа-я]{1,20}";//Имя-Фамилия кирил-лат
            string s1 = FunckRegex(reg1);
            MessageBox.Show("First Surname Name: " + s1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            countWindow--;
        }

        private void firstPlaceWithSurnameAndInitialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg2 = @"[A-ZА-Я]{1}[a-zа-я]{1,20} [A-ZА-Я]\.[A-ZА-Я]\.";//Фамилия инициалы
            string s2 = FunckRegex(reg2);
            MessageBox.Show("First Surname Initials: " + s2);
        }


        private void checkForAvailabilityCKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg3 = @"\s(char|using|if|else|break|string|int|float|double|class|void|public|private)";
            string s3 = FunckRegex(reg3);
            MessageBox.Show("C# keywords: " + s3);
        }


        private void checkForAvailabilityAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg4 = @"[а-я]{2,3}\.\s[А-Я]{1}[а-я]{1,15}\,\s{0,1}[а-я]{0,1}\.{0,1}\s[0-9]{1,4}\,\s[а-я]{0,2}\.{0,1}\s{0,1}[0-9]{0,4}\,{0,1}\s[а-я]{1}\.\s[А-Я]{1}[а-я]{2,15}\,\s[0-9]{5}";
            string s4 = FunckRegex(reg4);
            MessageBox.Show("Adress: "+ s4);
        }


        private void checkForAvailabilityInetAdressFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg5 = @"[a-z]{3,30}\.edu.ua|a-z]{3,30}\.net.ua|[a-z]{3,30}\.com.ua|[a-z]{3,30}\.in.ua| [a-z]{3,30}\.org.ua";
            string s5 = FunckRegex(reg5);
            MessageBox.Show("Domain zone: " + s5);
        }


        private void checkForAvailabilityIntegerConstantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg6 = @"\‘[0-9]{1,15}\’|\“[0-9]{1,15}\”";
            string s6 = FunckRegex(reg6);
            MessageBox.Show("Integer constant: " + s6);
        }

        private void checkForAvailabilityRealConstantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg7 = @"\’[0-9]{1,9}\.[0-9]{1,9}\’|\”[0-9]{1,9}\.[0-9]{1,9}\”";
            string s7 = FunckRegex(reg7);
            MessageBox.Show("Real constant: " + s7);
        }

        private void checkForAvailabilityComplexConstantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg8 = @"\‘[0-9]{1,9}\+[a-z]{1}\’|\”[0-9]{1,9}\-[0-9]{1,9}\*[a-z]{1}\”";
            string s8 = FunckRegex(reg8);
            MessageBox.Show("Complex constant: " + s8);
        }

        private void searchAllCorrectEmailAdressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string reg9 = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)";
            reg9 = @"([a-zA-Z0-9_\-\.])+@([a-zA-Z0-9_\-])+\.([a-zA-Z0-9_\-\.]){2,3}";
            string s9="";
            
            string pathTimeFile = AppDomain.CurrentDomain.BaseDirectory +
                "onlyformyneeds.txt";
            using (StreamWriter sw = new StreamWriter(pathTimeFile,
                false, System.Text.Encoding.Unicode))
            {
                sw.WriteLine(richTextBox1.Text);
            }
            string file = File.ReadAllText(pathTimeFile, Encoding.Unicode);

            MatchCollection matches = Regex.Matches(file, reg9);
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    s9 = s9 + "\n" + capture.Value;
                }
            }
            File.Delete(pathTimeFile);
            MessageBox.Show("Emails: " + s9);
        }
    }
}
