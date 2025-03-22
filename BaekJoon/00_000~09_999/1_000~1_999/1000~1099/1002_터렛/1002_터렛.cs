using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : 터렛
    문제번호 : 1002번

    두 원의 만나는 경우를 찾는 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0094
    {

        static void Main94(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int x1 = ReadInt(sr);
                int y1 = ReadInt(sr);
                int r1 = ReadInt(sr);

                int x2 = ReadInt(sr);
                int y2 = ReadInt(sr);
                int r2 = ReadInt(sr);

                
                // 두원이 일치하는 경우 무한대
                if (x1 == x2 && y1 == y2 && r1 == r2)
                {

                    sw.WriteLine(-1);
                    continue;
                }

                int diffX = x1 - x2;
                diffX *= diffX;

                int diffY = y1 - y2;
                diffY *= diffY;

                int calc1 = r1 + r2;
                calc1 *= calc1;

                int calc2 = r1 - r2;
                calc2 *= calc2;

                // 두 원의 거리가 반지름의 합보다 큰 경우
                if (calc1 < diffX + diffY) sw.WriteLine(0);
                // 반지름의 합과 차가 두원의 거리가 같은 경우 1
                else if (calc1 == diffX + diffY || calc2 == diffX + diffY) sw.WriteLine(1);
                // 내부에 포함되는 경우 0
                else if (diffX + diffY < calc2) sw.WriteLine(0);
                // 이외는 2
                else sw.WriteLine(2);
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            bool plus = true;
            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                else if (c == '-') 
                { 
                    
                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
