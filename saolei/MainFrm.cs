using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace saolei
{
    public partial class MainFrm : Form
    {
        Random ra;
        Boxs btns;
        int cols;
        int mineCount;
        int btnWidth;

        public MainFrm()
        {
            InitializeComponent();
            cols = 10;
            mineCount = 10;
            btnWidth = 40;
            ra = new Random();
            Init(cols, mineCount, btnWidth);           
        }

        int second=0;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            btns.Darws(g);
        }
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (second == 0)
            {
                this.timer1.Start();
            }
            string s="";
            for (int i = 0; i < cols*cols; i++)
            {
                if (btns.list[i].IsIn(e.X, e.Y))
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (btns[i].Flag == 0)
                        {
                            btns.list[i].Flag = 3;
                            if (btns.list[i].lCount == 0)
                                btns.Find(i);
                            if (btns[i].Statu == 1)
                            {
                                for (int j = 0; j < cols*cols; j++)
                                {
                                    btns.list[j].Flag = 3;
                                    s += " " + j + ":" + btns[j].Statu+",";
                                    this.Refresh();
                                }
                                if (DialogResult.OK == MessageBox.Show("failed", "tip", MessageBoxButtons.OK))
                                {
                                    timer1.Stop();
                                }
                            }
                        }
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        if (btns[i].Flag != 3 && btns[i].Flag != 4)
                            btns[i].Flag = (btns[i].Flag + 1) % 3;
                    }
                    break;
                }
            }
            this.Refresh();
            if (btns._count == cols*cols-mineCount)
            {
                timer1.Stop();
                MessageBox.Show("success", "tip", MessageBoxButtons.OK);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = (second / 60).ToString() + ":" + (second % 60).ToString("00");
            second += 1;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.W:
            //        snake.Direction = 0;
            //        break;
            //    case Keys.S:
            //        snake.Direction = 2;
            //        break;
            //    case Keys.A:
            //        snake.Direction = 3;
            //        break;
            //    case Keys.D:
            //        snake.Direction = 1;
            //        break;
            //}
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            ReStart();  
        }
        private void ReStart()
        {
            second = 0;
            this.textBox1.Text = "0:00";
            btns = new Boxs(cols,mineCount,btnWidth);
            this.timer1.Stop();
            this.Refresh();
        }
        private void Init(int cols,int mineCount,int btnWidth)
        {
            this.cols = cols;
            this.mineCount = mineCount;
            this.btnWidth = btnWidth;
            btns = new Boxs(cols, mineCount, btnWidth);
            this.Width = cols * btnWidth + 17;
            this.Height = cols * btnWidth + 100;
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "0:00";
            this.cmbLevel.SelectedIndex = 0;
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int selectIndex = comboBox.SelectedIndex;
            switch (selectIndex)
            {
                case 0:
                    Init(10,10,40);
                    break;
                case 1:
                    Init(10, 20, 40);
                    break;
                case 2:
                    Init(15, 20, 30);
                    break;
            }
            this.Refresh();
        }


    }
}