using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgToASCII
{
    public partial class Form3 : Form
    {
        public Form3(char[] ch)
        {
            InitializeComponent();
            oldchs = ch;
        }

        public bool IsClickOk = false;
        private char[] newchs;
        private char[] oldchs;
        private void button1_Click(object sender, EventArgs e)
        {
            string chs = textBox1.Text;
            newchs= new char[chs.Length];
            for (int i = 0; i < chs.Length; i++)
            {
                newchs[i]= chs[i];
            }
            IsClickOk = true;
            this.Close();
        }
        public char[] getChars() 
        {
            if (IsClickOk)
            {
                return newchs;
            }
            else 
            {
                return oldchs;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string tempS="";
            foreach (char item in oldchs)
            {
                tempS += item.ToString();
            }
            textBox1.Text = tempS;
        }
    }
}
