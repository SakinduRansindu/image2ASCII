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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private bool ResizeClick = false;
        public bool IsUserClickedOnResize() 
        {
            return ResizeClick;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                hight = (int)higthNum.Value;
                width = (int)widthNum.Value;
                ResizeClick = true;
                this.Close();
            }
            catch (Exception exc) 
            {
                MessageBox.Show(exc.Message);
            }
            
        }

        private int hight;
        private int width;

        public object[] GetResolutions() 
        {
            return new object[] { hight, width };
        }
    }
}
