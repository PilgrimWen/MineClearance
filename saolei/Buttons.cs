using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace saolei
{
    class Buttons
    {
        public List<Button> list = new List<Button>();
        public int _count = 0;
        int[] landmine = new int[10];
        //索引器
        public Button this[int i]
        {
            get
            {
                return list[i];
            }
        }
        //构造函数
        public Buttons(int width, int offest)
        {
            int num = 100;
            for (int i = 0; i < num; i++)
            {
                Button btn;
                int row = i / 10;
                int col = (i % 10);
                btn = new Button(col, row, 0, width);
                btn.row = row;
                btn.col = col;
                btn.Flag = 0;
                btn.flagChange = new FlagChangeHandle(countHandle);
                list.Add(btn);
            }
            Random(landmine, list);
            CountMine(landmine, list);
        }
        //随机生成10个雷
        public void Random(int[] lei, List<Button> list)
        {
            System.Random random = new Random();
            for (int i = 0; i < lei.Length; i++)
            {
                int index;
                do
                {
                    index = random.Next(100);
                }
                while (lei.Contains<int>(index));
                lei[i] = index;
            }
            for (int i = 0; i < lei.Length; i++)
            {
                int index = lei[i];
                list[index].Statu = 1;
            }
        }
        //数雷的个数
        public void CountMine(int[] lei, List<Button> list)
        {
            for (int i = 0; i < lei.Length; i++)
            {
                int index = lei[i];
                //右
                bool right = index % 10 != 9;
                if (right)
                { list[index + 1].lCount += 1; }
                //左
                bool left = index % 10 != 0;
                if (left)
                { list[index - 1].lCount += 1; }
                //上
                bool up = index / 10 < 9;
                if (up)
                { list[index + 10].lCount += 1; }
                //下
                bool down = index / 10 > 0;
                if (down)
                { list[index - 10].lCount += 1; }

                if (right && up)
                { list[index + 11].lCount += 1; }
                if (right && down)
                { list[index - 9].lCount += 1; }
                if (left && up)
                { list[index + 9].lCount += 1; }
                if (left && down)
                { list[index - 11].lCount += 1; }
            }
        }
        //寻找周围雷数为零的
        public void Find(int i)
        {
            int index = i;
            //右
            bool right = index % 10 != 9;
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
            bool left = index % 10 != 0;
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
            bool up = index / 10 < 9;
            if (up)
            {
                if (list[index + 10].lCount == 0 && list[index + 10].Flag != 3)
                {
                    list[index + 10].Flag = 3;
                    Find(index + 10);
                }
                else
                    list[index + 10].Flag = 3;
            }
            //下
            bool down = index / 10 > 0;
            if (down)
            {
                if (list[index - 10].lCount == 0 && list[index - 10].Flag != 3)
                {
                    list[index - 10].Flag = 3;
                    Find(index - 10);
                }
                else
                    list[index - 10].Flag = 3;
            }

            if (right && up)
            {
                if (list[index + 11].lCount == 0 && list[index + 11].Flag != 3)
                {
                    list[index + 11].Flag = 3;
                    Find(index + 11);
                }
                else
                    list[index + 11].Flag = 3;
            }
            if (right && down)
            {
                if (list[index - 9].lCount == 0 && list[index - 9].Flag != 3)
                {
                    list[index - 9].Flag = 3;
                    Find(index - 9);
                }
                else
                    list[index - 9].Flag = 3;
            }
            if (left && up)
            {
                if (list[index + 9].lCount == 0 && list[index + 9].Flag != 3)
                {
                    list[index + 9].Flag = 3;
                    Find(index + 9);
                }
                else
                    list[index + 9].Flag = 3;
            }
            if (left && down)
            {
                if (list[index - 11].lCount == 0 && list[index - 11].Flag != 3)
                {
                    list[index - 11].Flag = 3;
                    Find(index - 11);
                }
                else
                    list[index - 11].Flag = 3;
            }
        }
        public void Darws(Graphics g)
        {
            foreach (Button btn in list)
            {
                btn.draw(g);
            }
        }
        public void countHandle()
        { _count++; }
    }
}