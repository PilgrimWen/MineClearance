using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineClearance
{
    public delegate void FlagChangeHandle();
    class Box
    {
        int flag;                   //标志显示状态
        public FlagChangeHandle flagChange;
        int width;               //宽度
        int statu = 0;            //标志是否有雷
        int x, y;
        public int row;         //行号
        public int col;          //列号
        Rectangle rec;
        public int Statu
        {
            get { return this.statu; }
            set { this.statu = value; }
        }
        public int Flag
        {
            get { return this.flag; }
            set
            {
                if (flag == 3)
                {
                    return;
                }
                if (value == 3)
                {
                    flagChange();
                }
                this.flag = value;
            }
        }
        public int lCount { get; set; }//

        public Box(int row, int col, int statu, int width)
        {
            this.statu = statu;
            this.x = row * width;
            this.y = col * width;
            this.col = col;
            this.row = row;
            this.width = width;
            rec = new System.Drawing.Rectangle(x, y, width, width);
            lCount = 0;
        }


        public bool IsIn(int x, int y)
        {
            if ((x - this.x) > 0 && this.x + width - x > 0 && y - this.y > 0 && this.y + width - y > 0)
            {
                return true;
            }
            return false;
        }
        public void draw(Graphics g)
        {
            switch (flag)
            {
                case 0:
                    g.FillRectangle(Brushes.CadetBlue, rec);
                    g.DrawRectangle(Pens.Black, rec);
                    break;
                case 1:
                    Image image = Resource.red;
                    g.DrawImage(image, rec);
                    //g.DrawString("!", new Font("宋体", 15), Brushes.Purple, new Point(rec.X + 2, rec.Y + 2));
                    g.DrawRectangle(Pens.Black, rec);
                    break;
                case 2:
                    {
                        var fontSize = g.MeasureString("?", new Font("宋体", 15));
                        g.DrawString("?", new Font("宋体", 15), Brushes.SeaGreen, new PointF(rec.X + (rec.Width - fontSize.Width) / 2, rec.Y + (rec.Height - fontSize.Height) / 2));
                        g.DrawRectangle(Pens.Black, rec);
                        break;
                    }
                case 3:
                    if (statu == 0)
                    {
                        var fontSize=g.MeasureString(lCount.ToString(), new Font("宋体", 15));
                        if (lCount != 0)
                        {
                            g.DrawString(lCount.ToString(), new Font("宋体", 15), Brushes.SeaGreen, new PointF(rec.X + (rec.Width - fontSize.Width) / 2, rec.Y + (rec.Height - fontSize.Height) / 2));
                        }
                        g.DrawRectangle(Pens.Black, rec);
                    }
                    else
                    {
                        var fontSize = g.MeasureString("*", new Font("宋体", 15));
                        g.DrawString("*", new Font("宋体", 15), Brushes.SeaGreen, new PointF(rec.X + (rec.Width - fontSize.Width) / 2, rec.Y + (rec.Height - fontSize.Height) / 2));
                        g.DrawRectangle(Pens.Black, rec);
                    }
                    break;
            }
        }
    }
}
