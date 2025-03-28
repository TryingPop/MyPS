using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 28
이름 : 배성훈
내용 : PLATFORME
    문제번호 : 1276번

    구현, 정렬 문제다.
    선의 갯수가 100개이고, 범위가 1만이므로
    일일히 기록하는 식으로 해도 100만번 연산을하기에
    충분하다 판단했고 해당 방법으로 풀었다.

    만약 선의 갯수가 많고, 범위가 10만 가까이 되면
    범위의 최댓값을 기록해야 하기에 세그먼트 트리를 이용해 풀 것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1487
    {

        static void Main1487(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();

            (int y, int s, int e)[] line = new (int y, int s, int e)[n];
            for (int i = 0; i < n; i++)
            {

                line[i] = (ReadInt(), ReadInt(), ReadInt() - 1);
            }

            Array.Sort(line, (x, y) => x.y.CompareTo(y.y));

            int[] height = new int[10_000];
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                int curY = line[i].y;
                ret += curY - height[line[i].s];
                ret += curY - height[line[i].e];
                for (int j = line[i].s; j <= line[i].e; j++)
                {

                    height[j] = curY;
                }
            }

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
