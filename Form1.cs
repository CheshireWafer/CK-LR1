using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СК
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных",
                Filter = "Текстовые файлы (*.txt, *.doc, *.docx)|*.txt;*.doc;*.docx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            if (filePath != "")
            {
                string str1 = System.IO.File.ReadAllText(@filePath);
                textBox1.Clear();
                textBox1.ReadOnly = true;
                textBox1.ScrollBars = ScrollBars.Vertical;
                textBox1.Text = str1;
            }
            else MessageBox.Show("Выберите текстовый файл.", "Ошибка", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных",
                Filter = "Текстовые файлы (*.txt, *.doc, *.docx)|*.txt;*.doc;*.docx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            if (filePath != "")
            {
                string str2 = System.IO.File.ReadAllText(@filePath);
                textBox2.Clear();
                textBox2.ReadOnly = true;
                textBox2.ScrollBars = ScrollBars.Vertical;
                textBox2.Text = str2;
            }
            else MessageBox.Show("Выберите текстовый файл.", "Ошибка", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string length = Convert.ToString(str2.Length+1);
            string temp = "";
            int spaceBars = 0;
            int i;
            int k = 0; //счётчик для контейнера
            int m = 0; //счётчик для сообщения

            for (i = 0; i < str1.Length; i++)
            {
                if (str1[i] == ' ')
                {
                    spaceBars++;
                }
            }

            StringBuilder crypted = new StringBuilder();
            foreach (byte b in System.Text.Encoding.Default.GetBytes(textBox2.Text))
            {
                crypted.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            
            if(spaceBars < Convert.ToString(crypted).Length)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                MessageBox.Show($"Количество интервалов недостаточно для скрытия информации! Количество интервалов в контейнере: {spaceBars}. Количество битов файла-сообщения: {Convert.ToString(crypted).Length}.", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {

                temp += length + ".";

                for (i = 0; i < str1.Length; i++)
                {
                    if (str1[i] == ' ')
                    {
                        temp += str1[k..i];
                        k = i;
                        if (m < crypted.Length)
                        {
                            if (crypted[m] == '0')
                                temp += " ";
                            m++;
                        }
                    }
                }
                temp += str1[k..i];

                //for (i = 0; i < str1.Length; i++)
                //{
                //    if (Char.IsLetter(str1, i))
                //        temp += str1[i];
                //    else if (str1[i] == ' ')
                //    {
                //        if (crypted[m] == '0')
                //            temp += " ";
                //        temp += " ";
                //        m++;
                //    }
                //}

                textBox3.Text = Convert.ToString(temp);
                textBox3.ReadOnly = true;
                textBox3.ScrollBars = ScrollBars.Vertical;

                SaveFileDialog saveFile1 = new SaveFileDialog
                {
                    DefaultExt = "*.txt, *.doc, *.docx",
                    Filter = "Text files|*.txt; *.doc; *.docx"
                };

                if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
                {
                    using (StreamWriter sw = new StreamWriter(saveFile1.FileName, true))
                    {
                        sw.WriteLine(Convert.ToString(temp));
                        sw.Close();
                    }
                }
            }
        }
    }
}