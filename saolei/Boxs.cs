using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace saolei
{
    class Boxs
    {
        public List<Box> list = new List<Box>();
        public int _count = 0;
        public int cols;
        int[] landmine ;
        //索引器
        public Box this[int i]
        {
            get
            {
                return list[i];
            }
        }
        //构造函数
        public Boxs(int cols,int mineCount,int width)
        {
            this.cols = cols;
            int num = cols*cols;
            landmine = new int[mineCount];
            for (int i = 0; i < num; i++)
            {
                Box btn;
                int row = i / cols;
                int col = (i % cols);
                btn = new Box(col, row, 0, width);
                btn.Flag = 0;
                btn.flagChange = new FlagChangeHandle(CountHandle);
                list.Add(btn);
            }
            Random(landmine, list);
            CountMine(landmine, list);
        }
        //随机生成10个雷
        public void Random(int[] lei, List<Box> list)
        {
            System.Random random = new Random();
            for (int i = 0; i < lei.Length; i++)
            {
                int index;
                do
                {
                    index = random.Next(cols*cols);
                }
                while (lei.Contains<int>(index));
                lei[i] = index;
            }
            foreach(int ind in  lei)
            {
                list[ind].Statu = 1;
            }
        }
        //数雷的个数
        public void CountMine(int[] lei, List<Box> list)
        {
            for (int i = 0; i < lei.Length; i++)
            {
                int index = lei[i];
                //右
                bool right = index % cols != cols-1;
                if (right)
                { list[index + 1].lCount += 1; }
                //左
                bool left = index % cols != 0;
                if (left)
                { list[index - 1].lCount += 1; }
                //上
                bool up = index / cols < cols-1;
                if (up)
                { list[index + cols].lCount += 1; }
                //下
                bool down = index / cols > 0;
                if (down)
                { list[index - cols].lCount += 1; }

                if (right && up)
                { list[index + cols+1].lCount += 1; }
                if (right && down)
                { list[index - cols+1].lCount += 1; }
                if (left && up)
                { list[index + cols-1].lCount += 1; }
                if (left && down)
                { list[index - cols-1].lCount += 1; }
            }
        }
        //寻找周围雷数为零的
        public void Find(int i)
        {
            int index = i;
            //右
            bool right = index % cols != cols-1;
            if (right)
            {
                if (list[index + 1].lCount == 0 && list[index + 1].Flag != 3)
                {
                    list[index + 1].Flag = 3;
                    Find(index + 1);
                }
                else
                    list[index + 1].Flag = 3;
            }
            //左
            bool left = index % cols != 0;
            if (left)
            {
                if (list[index - 1].lCount == 0 && list[index - 1].Flag != 3)
                {
                    list[index - 1].Flag = 3;
                    Find(index - 1);
                }
                else
                    list[index - 1].Flag = 3;
            }
            //上
            bool up = index / cols < cols-1;
            if (up)
            {
                if (list[index + cols].lCount == 0 && list[index + cols].Flag != 3)
                {
                    list[index + cols].Flag = 3;
                    Find(index + cols);
                }
                else
                    list[index + cols].Flag = 3;
            }
            //下
            bool down = index / cols > 0;
            if (down)
            {
                if (list[index - cols].lCount == 0 && list[index - cols].Flag != 3)
                {
                    list[index - cols].Flag = 3;
                    Find(index - cols);
                }
                else
                    list[index - cols].Flag = 3;
            }

            if (right && up)
            {
                if (list[index + cols+1].lCount == 0 && list[index + cols + 1].Flag != 3)
                {
                    list[index + cols + 1].Flag = 3;
                    Find(index + cols + 1);
                }
                else
                    list[index + cols + 1].Flag = 3;
            }
            if (right && down)
            {
                if (list[index - cols+1].lCount == 0 && list[index - cols + 1].Flag != 3)
                {
                    list[index - cols + 1].Flag = 3;
                    Find(index - cols + 1);
                }
                else
                    list[index - cols + 1].Flag = 3;
            }
            if (left && up)
            {
                if (list[index + cols-1].lCount == 0 && list[index + cols - 1].Flag != 3)
                {
                    list[index + cols - 1].Flag = 3;
                    Find(index + cols - 1);
                }
                else
                    list[index + cols - 1].Flag = 3;
            }
            if (left && down)
            {
                if (list[index - cols-1].lCount == 0 && list[index - cols - 1].Flag != 3)
                {
                    list[index - cols - 1].Flag = 3;
                    Find(index - cols - 1);
                }
                else
                    list[index - cols - 1].Flag = 3;
            }
        }
        public void Darws(Graphics g)
        {
            foreach (Box btn in list)
            {
                btn.draw(g);
            }
        }
        public void CountHandle()
        {lock(this) _count++; }
    }
}