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
        public MainFrm()
        {
            InitializeComponent();
            btns = new Buttons(40, 20);
           // snake = new Snake(5);
            ra = new Random();
            //button = new Button(30, 30, 0, 5);
            this.Width = 430;
            this.Height = 500;            //this.button1.Click+=new EventHandler        
        }
        Random ra;
        Buttons btns;
        //Snake snake;
        //Button button;
        int second;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            btns.Darws(g);
            //snake.Draw(g);
            //button.draw(g);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                if (btns.list[i].isIn(e.X, e.Y))
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
                                for (int j = 0; j < 100; j++)
                                {
                                    btns.list[j].Flag = 3;
                                    this.Refresh();
                                }
                                if (DialogResult.OK == MessageBox.Show("failed", "tip", MessageBoxButtons.OK))
                                    btns = new Buttons(40, 20);
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
            if (btns._count == 90)
                MessageBox.Show("success", "tip", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            second = 0;
            this.timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.Refresh(); snake.Move();
            //if (snake.row == button.row && snake.col == button.col)
            //{
            //    button = new Button(ra.Next(50), ra.Next(50), 0, 5);
            //}
            this.textBox1.Text = (second / 60).ToString() + ":" + (second % 60).ToString("00");
            second += 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
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
        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "0:00";
        }
    }
}