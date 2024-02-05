using System.Timers;

namespace ImgToASCII
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        pixalConverter pxConv;
        private char[] chs = { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };
        private void button2_Click(object sender, EventArgs e)
        {
            if (btm != null)
            {
                cancelbtn.Enabled = true;
                convertbtn.Enabled = false;
                openbtn.Enabled = false;
                pxConv = new pixalConverter(btm,chs);
                Thread t = new Thread(pxConv.Start);
                t.Start();
                timer.Start();
                
            }
            else 
            {
                MessageBox.Show("Open an image First.", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateProgressBar(pxConv.getProgress());
            if (pxConv.workdone) {
                UpdateTextImg(pxConv.getASCII_IMG());
                UpdateProgressBar(0);
                ChangeBtnStates();
                timer.Stop();
            }
        }

        public delegate void ChangeBtnStatesDele();
        private void ChangeBtnStates() 
        {
            if (this.cancelbtn.InvokeRequired || this.convertbtn.InvokeRequired)
            {
                ChangeBtnStatesDele btnchange = new ChangeBtnStatesDele(ChangeBtnStates);
                this.cancelbtn.Invoke(btnchange);
            }
            else 
            {
                cancelbtn.Enabled = false;
                convertbtn.Enabled = true;
                openbtn.Enabled = true;
            }
        }

        delegate void UpdateTextImgDele(String s);
        private void UpdateTextImg(String s)
        {
            if (this.textimg.InvokeRequired)
            {
                UpdateTextImgDele dele = new UpdateTextImgDele(UpdateTextImg);
                this.textimg.Invoke(dele, new object[]{ s});
            }
            else 
            {
                textimg.Text = s;
            }
        }

        delegate void UpdateProgressBarDele(Int16 pro);
        private void UpdateProgressBar(Int16 pro)
        {
            if (this.progressBar1.InvokeRequired)
            {
                UpdateProgressBarDele d = new UpdateProgressBarDele(UpdateProgressBar);
                this.progressBar1.Invoke(d,new object[]{ pro });

            }
            else 
            {
                progressBar1.Value = pro;
            }
        }

        OpenFileDialog imgSelect = new OpenFileDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            imgSelect.Filter = "png image|*.png|jpg image|*.jpg";
            imgSelect.ShowDialog();
        }

        Bitmap btm=null;
        private void ImgSelect_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            
            string path = imgSelect.FileName;
            imgLocationlbl.Text = path;
            btm = new Bitmap(path);
            button1.Enabled = true;
            UpdateImgDetails();
                
        }

        private void UpdateImgDetails() 
        {
            pictureBox1.Image = btm;
            heightlbl.Text = btm.Height.ToString();
            widthlbl.Text = btm.Width.ToString();
            pixelslbl.Text = (btm.Height * btm.Width).ToString();
        }

        System.Timers.Timer timer;
        private void Form1_Load(object sender, EventArgs e)
        {
            imgSelect.FileOk += ImgSelect_FileOk;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void widthlbl_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pxConv!=null) { pxConv.CancelProcess(); }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            if (pxConv != null) 
            {
                pxConv.CancelProcess();
                while (!pxConv.workdone) 
                {
                    Thread.Sleep(100);
                }
                timer.Stop();
                progressBar1.Value = 0;
                cancelbtn.Enabled = false;
                convertbtn.Enabled = true;
                openbtn.Enabled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textimg.ZoomFactor = ((float)trackBar1.Value/(float)10);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            int uhight = 200;
            int uwidth = 200;
            if (f2.IsUserClickedOnResize()) 
            {
                uhight = Convert.ToInt16(f2.GetResolutions()[0]);
                uwidth = Convert.ToInt16(f2.GetResolutions()[1]);
            }
            ImageResizer rz = new ImageResizer();
            using (Bitmap btmTmp = new Bitmap(rz.resize(btm,uhight,uwidth))) 
            {
                btm.Dispose();
                btm = new Bitmap(btmTmp);
            }
            pictureBox1.Image = btm;
            UpdateImgDetails();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(chs);
            f3.ShowDialog();
            chs = f3.getChars();
        }
    }

    public class pixalConverter 
    {
        const string PreChar = " ";
        Bitmap img;
        private bool isCanceled = false;
        public pixalConverter(Bitmap _img, char[] _ch ) 
        {
            ch = _ch;
            img = new Bitmap(_img) ;
            extractDetails();
        }

        private int hight;
        private int width;
        private int AbsProgress;
        private int currentProgress = 0;
        private int britnessLevels;
        float tmp;
        private void extractDetails()
        {
            hight = img.Height;
            width = img.Width;
            AbsProgress = hight * width;
            britnessLevels = ch.Length;
            tmp= (float)1.0 / (float)britnessLevels;

        }

        public void Start() 
        {
            convert();
        }

        public void CancelProcess() 
        {
            isCanceled = true;
        }

        public bool workdone=false;
        private String asciiImg = "";

        private void convert() 
        {
            for (int y = 0; y < hight-1; y++)
            {
                for (int x = 0; x < width-1; x++)
                {
                    if (isCanceled) 
                    {
                        break;
                    }
                    Color pixCol = img.GetPixel(x,y);
                    currentProgress = width * (y + 1)+x;
                    char s = getCharForColor(pixCol);
                    asciiImg += PreChar + s.ToString();

                }
                asciiImg += "|\n|";
                if (isCanceled) 
                {
                    break;
                }


            }
            workdone = true;
        }

        public String getASCII_IMG() 
        {
            if (workdone)
            {
                return asciiImg;
            }
            else 
            {
                return asciiImg+"\nwork in progress...";
            }
        }


        public Int16 getProgress()
        {
            Int16 p =  Convert.ToInt16(((float)currentProgress / (float)AbsProgress) * 1000);
            return p;
        }

        public char[] ch ;

        public char getCharForColor(Color c)
        {
            float b = c.GetBrightness();
            int selectedChar = Convert.ToInt16(b / tmp);
            if (Convert.ToInt16(b) == 0) { return ch[selectedChar]; }
            else { return ch[selectedChar - 1]; }
        }
    }

    public class ImageResizer 
    {
        public Bitmap resize(Bitmap bmp , int newHeight , int newWidth) 
        {
            int height = bmp.Height;
            int width = bmp.Width;

            float pxPerHeightf = (float)height / (float)newHeight;
            float pxPerWidthf = (float)width / (float)newWidth;
            int pxPerHeight = Convert.ToInt16(pxPerHeightf);
            int pxPerWidth = Convert.ToInt16(pxPerWidthf);
            pxPerHeightf = pxPerHeightf - pxPerHeight;
            pxPerWidthf = pxPerWidthf - pxPerWidth; 

            if (pxPerHeight > 1 && pxPerWidth > 1)
            {
                
                int newY = -1;
                Bitmap newImg = new Bitmap(newWidth, newHeight);
                for (int y = 0; y < height-pxPerHeight ; y += pxPerHeight+1) 
                {
                    int newX = -1;
                    newY++;
                    for (int x = 0; x < width-pxPerWidth ; x += pxPerWidth+1)
                    {
                        newX++;
                        int newR=0;
                        int newG=0;
                        int newB=0;
                        for (int j = 0; j < pxPerHeight; j++)
                        {
                            for (int i = 0; i < pxPerWidth; i++)
                            {
                                if (pxPerWidth != i || pxPerHeight != j)
                                {
                                    newR += bmp.GetPixel(x + i, y + j).R;
                                    newG += bmp.GetPixel(x + i, y + j).G;      
                                    newB += bmp.GetPixel(x + i, y + j).B;
                                }

                                if(pxPerWidth==i) 
                                {
                                    newR += Convert.ToInt16( (float)bmp.GetPixel(x + i, y + j).R * pxPerWidthf);
                                    newG += Convert.ToInt16((float)bmp.GetPixel(x + i, y + j).G * pxPerWidthf);
                                    newB += Convert.ToInt16((float)bmp.GetPixel(x + i, y + j).B * pxPerWidthf);
                                }

                                if(pxPerHeight==j)
                                {
                                    newR += Convert.ToInt16((float)bmp.GetPixel(x + i, y + j).R * pxPerHeightf);
                                    newG += Convert.ToInt16((float)bmp.GetPixel(x + i, y + j).G * pxPerHeightf);
                                    newB += Convert.ToInt16((float)bmp.GetPixel(x + i, y + j).B * pxPerHeightf);
                                }
                            }
                        }
                        newR /= (pxPerHeight + 1) * (pxPerWidth + 1);
                        newG /= (pxPerHeight + 1) * (pxPerWidth + 1);
                        newB /= (pxPerHeight + 1) * (pxPerWidth + 1);
                        if (newR > 255) { newR = 255; }
                        if (newG > 255) { newG = 255; }
                        if (newB > 255) { newB = 255; }
                        newImg.SetPixel(newX, newY, Color.FromArgb(newR, newG, newB));
                    }
                }
                return newImg;
            }
            else 
            {
                MessageBox.Show("can't resize");
                return bmp;
            }
        }
    }
}