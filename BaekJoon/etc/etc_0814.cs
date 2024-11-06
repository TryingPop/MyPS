using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 14
이름 : 배성훈
내용 : 미적분학 입문하기 2
    문제번호 : 24726번

    수학, 기하학, 미적분학 문제다
    문제를 제대로 안읽어 1사분면이 아닌 다른 사분면에도 존재하는 줄 알았다;
    아이디어는 다음과 같다

    직선을 축에 따라 회전시키는데
    x축에 평행하게 이동해도 이상이 없다

    그래서 평행하게 이동시켜 적분했다
    이 경우 처음 x가 0이 되게 이동할 수 있으므로,
    x의 길이와 fy, ty 값만 알면 된다
    그래서 이동시키면, 
*/

namespace BaekJoon.etc
{
    internal class etc_0814
    {

        static void Main814(string[] args)
        {

            (int x, int y)[] pos;
            decimal PI = (decimal)Math.PI;
            decimal ret1, ret2;

            Solve();
            void Solve()
            {

                Read();

                GetRet();

                Console.Write($"{ret1}\n{ret2}");
            }

            void GetRet()
            {

                decimal dx, dy;
                Array.Sort(pos, (x, y) => x.x.CompareTo(y.x));

                ret1 = GetVolume(pos[1].x - pos[0].x, pos[0].y, pos[1].y);
                ret1 += GetVolume(pos[2].x - pos[1].x, pos[1].y, pos[2].y);
                ret1 -= GetVolume(pos[2].x - pos[0].x, pos[0].y, pos[2].y);

                ret1 = ret1 < 0 ? -ret1 : ret1;

                Array.Sort(pos, (x, y) => x.y.CompareTo(y.y));

                ret2 = GetVolume(pos[1].y - pos[0].y, pos[0].x, pos[1].x)
                    + GetVolume(pos[2].y - pos[1].y, pos[1].x, pos[2].x)
                    - GetVolume(pos[2].y - pos[0].y, pos[0].x, pos[2].x);

                ret2 = ret2 < 0 ? -ret2 : ret2;
            }

            decimal GetVolume(decimal _x, decimal _fy, decimal _ty)
            {

                if (_x == 0) return 0;
                decimal m = (_ty - _fy) / _x;
                m = m < 0 ? -m : m;
                if (m == 0) return _fy * _fy * _x * PI;

                decimal down = m * 3;
                decimal ret = PI * (_ty * _ty * _ty - _fy * _fy * _fy) / down;
                ret = ret < 0 ? -ret : ret;
                return ret;
            }

            void Read()
            {

                string[] temp = Console.ReadLine().Split();

                pos = new (int x, int y)[3];

                for (int i = 0; i < 3; i++)
                {

                    pos[i] = (int.Parse(temp[2 * i]), int.Parse(temp[2 * i + 1]));
                }
            }
        }
    }

#if other
using System;

namespace Baekjoon_24726
{
    class Program
    {
        class Point
        {
            double[] xy;
            public Point(double x, double y)
            {
                xy = new double[] { x, y };
            }
            public double Get(int index)
            {
                return xy[index];
            }
        }

        static Point[] points = new Point[3];

        static void Main()
        {
            double[] xys = Array.ConvertAll(Console.ReadLine().Split(' '), double.Parse);
            for (int i = 0; i < 3; i++)
                points[i] = new Point(xys[i * 2], xys[i * 2 + 1]);
            Console.Write(GetVolume(0));
            Console.Write(' ');
            Console.Write(GetVolume(1));
        }

        static double GetVolume(int i)
        {
            double volume = 0;
            Point[] ps = new Point[3];
            Array.Copy(points, ps, 3);
            Array.Sort(ps, (a, b) => a.Get(i) - b.Get(i) < 0 ? -1 : 1);
            int o = 1 - i;
            double i0 = ps[0].Get(i), i1 = ps[1].Get(i), i2 = ps[2].Get(i);
            double da = i1 - i0, db = i2 - i1;
            double o0 = ps[0].Get(o), o1 = ps[1].Get(o), o2 = ps[2].Get(o);
            double om = o0 + (o2 - o0) * da / (da + db);
            if (da != 0)
            {
                double t1 = (om - o0) / da, t2 = (o1 - o0) / da;
                volume += Math.Abs(Integral(da, t1 * t1, 2 * t1 * o0, o0 * o0) - Integral(da, t2 * t2, 2 * t2 * o0, o0 * o0));
            }
            if (db != 0)
            {
                double t1 = (o2 - om) / db, t2 = (o2 - o1) / db;
                volume += Math.Abs(Integral(db, t1 * t1, 2 * t1 * om, om * om) - Integral(db, t2 * t2, 2 * t2 * o1, o1 * o1));
            }
            volume *= Math.PI;
            return volume;
        }

        static double Integral(double e, double a, double b, double c)
        {
            double value = 0;
            value += c * e;
            value += b * e * e / 2;
            value += a * e * e * e / 3;
            return value;
        }
    }
}
#elif other2
// #include<stdio.h>
// #define pi 3.1415926535897932
// #define y1 y_1
double x1,y1,x2,y2,x3,y3,x,y,s;
int main(){
    scanf("%lf%lf%lf%lf%lf%lf",&x1,&y1,&x2,&y2,&x3,&y3);
    x=(x1+x2+x3)/3.0;y=(y1+y2+y3)/3.0;
    s=x1*(y2-y3)+x2*(y3-y1)+x3*(y1-y2);
    s*=0.5;s=s>0?s:-s;
    printf("%.9f %.9f",2.0*pi*y*s,2.0*pi*x*s);
    return 0;
}
#endif
}
