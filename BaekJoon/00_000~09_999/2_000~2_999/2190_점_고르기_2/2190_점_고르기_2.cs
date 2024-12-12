using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 12
이름 : 배성훈
내용 : 점 고르기 2
    문제번호 : 2190번

    브루트포스 문제다
    점의 갯수가 100 이하이므로 
    브루트포스인 N^3 방법도 유효하다고 판단했다.
    그래서 브루트포스로 정답을 찾았다.
    다만 y를 잡는데 j가 아닌 i로 해서 틀리고, 음수를 고려 안해 총 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1181
    {

        static void Main1181(string[] args)
        {
            StreamReader sr;

            long n, a, b;
            (int x, int y)[] vertex;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0;
                        int w = vertex[i].x;
                        int h = vertex[j].y;

                        for (int k = 0; k < n; k++)
                        {

                            if (Contains(w, h, k)) cnt1++;
                            if (Contains(w - a, h, k)) cnt2++;
                            if (Contains(w, h - b, k)) cnt3++;
                            if (Contains(w - a, h - b, k)) cnt4++;
                        }

                        int max = Math.Max(Math.Max(cnt1, cnt2), Math.Max(cnt3, cnt4));
                        ret = Math.Max(max, ret);
                    }
                }

                Console.Write(ret);

                bool Contains(long _l, long _b, int _idx)
                {

                    long r = _l + a;
                    long t = _b + b;

                    return _l <= vertex[_idx].x && vertex[_idx].x <= r
                        && _b <= vertex[_idx].y && vertex[_idx].y <= t;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();

                vertex = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    vertex[i] = (ReadInt(), ReadInt());
                }

                sr.Close();

                int ReadInt()
                {

                    int c = sr.Read();
                    bool positive = c != '-';
                    int ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return positive ? ret : -ret;
                }
            }
        }
    }
}
