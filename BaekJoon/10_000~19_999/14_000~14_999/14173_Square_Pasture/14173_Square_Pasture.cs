using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 30
이름 : 배성훈
내용 : Square Pasture
    문제번호 : 14173번

    수학, 사칙연산 문제다
    x2와 y1을 잘못 표현해 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1087
    {

        static void Main1087(string[] args)
        {

            (int x1, int y1, int x2, int y2) pos1, pos2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int minX = Math.Min(pos1.x1, pos2.x1);
                int maxX = Math.Max(pos1.x2, pos2.x2);
                int minY = Math.Min(pos1.y1, pos2.y1);
                int maxY = Math.Max(pos1.y2, pos2.y2);

                int size = Math.Max(maxX - minX, maxY - minY);
                Console.WriteLine(size * size);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                pos1 = (ReadInt(), ReadInt(), ReadInt(), ReadInt());
                pos2 = (ReadInt(), ReadInt(), ReadInt(), ReadInt());

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }

        }
    }
}
