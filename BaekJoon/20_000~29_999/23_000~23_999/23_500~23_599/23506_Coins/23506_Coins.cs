using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 15
이름 : 배성훈
내용 : Coins
    문제번호 : 23506번

    그리디 문제다.
    우선 행과 열로 이동하는건 독립이다.
    이는 행 방향으로 이동하는 것은 열의 변화가 없다는 뜻이다.

    그래서 먼저 행을 맞추는 것을 생각하자.
    이제 행을 맞추는데 최소횟수는 다음과 같이 찾을 수 있다.
    이동하지 않은 코인 중 가장 낮은 것부터 1로 옮긴다.
    이렇게 채우는 것이 행을 채우는 최솟값임을 그리디로 알 수 있다.(Exchange Argument)
    
    그리고 열은 행과 독립이므로 똑같은 방법으로 찾고 누적하면 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1630
    {

        static void Main1630(string[] args)
        {

            int n;
            int[] x, y;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 0;
                int ptrX = 1, ptrY = 1;

                for (int i = 1; i <= n; i++)
                {

                    while (x[i] > 0)
                    {

                        ret += Math.Abs(ptrX - i);
                        ptrX++;
                        x[i]--;
                    }

                    while (y[i] > 0)
                    {

                        ret += Math.Abs(ptrY - i);
                        ptrY++;
                        y[i]--;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                x = new int[n + 1];
                y = new int[n + 1];

                for (int i = 0; i < n; i++)
                {

                    int curX = ReadInt();
                    int curY = ReadInt();

                    x[curX]++;
                    y[curY]++;
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
