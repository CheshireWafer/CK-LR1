using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace СК
{
    public partial class Form3 : Form
    {
        public Form3()
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
            string str1 = textBox1.Text;
            string length = ""; //длина кол-во символов сообщения
            string temp = "";
            int k; // счетчик
            int i;
            int ending = 0;
            int beginning = 0; // отсчет
            //int limit = Convert.ToInt32(textBox3.Text);
            for (k = 0; k < str1.Length; k++)
            {
                beginning++;
                if (str1[k] == '.')
                {
                    length = str1[0..k];
                    break;
                }
            }

            ending = Convert.ToInt32(length) - 1;

            for (i = 0; i < str1.Length; i++)
            { 
                if (str1[i] == ' ')
                {
                    if (str1[i + 1] == ' ')
                    {
                        temp += "0";
                        i += 2;
                    }
                    else
                        temp += "1";
                }
            }

            var encrypted = Enumerable.Range(0, temp.Length / 8).Select(i => Convert.ToByte(temp.Substring(i * 8, 8), 2)).ToArray();
            var str = Encoding.UTF8.GetString(encrypted);

            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;

            textBox2.Text = str[0..ending];
        }
    }
}