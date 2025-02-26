using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 19
이름 : 배성훈
내용 : 점 모으기
    문제번호 : 7571번

    수학, 정렬 문제다.
    절댓값 함수들의 합을 찾아야 한다.
    최솟값은 중앙값으로 잘 알려져 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1348
    {

        static void Main1348(string[] args)
        {

            int n, m;
            int[] x, y;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(x);
                Array.Sort(y);

                int mX = x[m / 2];
                int mY = y[m / 2];

                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    ret += Math.Abs(x[i] - mX) + Math.Abs(y[i] - mY);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                x = new int[m];
                y = new int[m];

                for (int i = 0; i < m; i++)
                {

                    x[i] = ReadInt();
                    y[i] = ReadInt();
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
