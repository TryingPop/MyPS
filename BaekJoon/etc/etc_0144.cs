using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 산책 경로
    문제번호 : 3097번

    간단한 거리 계산 문제다
    문제를 잘못읽어 2번 틀렸다;

    브루트 포스로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0144
    {

        static void Main144(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            (int x, int y)[] move = new (int x, int y)[n];

            (int x, int y) last = (0, 0);
            for (int i = 0; i < n; i++)
            {

                int x = ReadInt(sr);
                int y = ReadInt(sr);

                move[i].x = x;
                move[i].y = y;

                last.x += move[i].x;
                last.y += move[i].y;
            }

            sr.Close();


            double ret = GetDis(last.x - move[0].x, last.y - move[0].y);

            for (int i = 0; i < n; i++)
            {

                // 선분 하나뻈을 때 최단 거리 구하기
                double calc = GetDis(last.x - move[i].x, last.y - move[i].y);
                if (calc < ret) ret = calc;
            }

            Console.WriteLine($"{last.x} {last.y}\n{ret:0.00}");
        }

        static double GetDis(double _x, double _y)
        {

            double ret = Math.Sqrt(_x * _x + _y * _y);
            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
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
